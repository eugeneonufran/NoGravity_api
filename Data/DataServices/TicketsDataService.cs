using DinkToPdf;
using DinkToPdf.Contracts;
using NoGravity.Data.DTO.SeatAllocations;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;

namespace NoGravity.Data.DataServices
{
    public class TicketsDataService : ITicketsDataService
    {
        private readonly NoGravityDbContext _dbContext;
        private readonly IConverter _converter;
        private readonly IConfiguration _configuration;

        public TicketsDataService(NoGravityDbContext dbContext, IConverter converter, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _converter = converter;
            _configuration = configuration;
        }

        public async Task<List<RouteDTO>> GetPossibleRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal)
        {
            IQueryable<JourneySegment> routes = _dbContext.JourneySegments;

            var allPaths = RouteFinder.FindAllPaths(await routes.ToListAsync(), departureStarportId, arrivalStarportId, specifiedDate);

            var filteredPaths = new List<List<JourneySegment>>();

            foreach (var path in allPaths)
            {
                if (await GenerateSeatMapForJourneys(path) is not null)
                {
                    filteredPaths.Add(path);
                }
            }

            switch (sortType)
            {
                case SortType.Price:
                    filteredPaths = RouteFinder.SortPathsByPrice(filteredPaths);
                    break;
                case SortType.Time:
                    filteredPaths = RouteFinder.SortPathsByTime(filteredPaths);
                    break;
                case SortType.Optimal:
                    filteredPaths = RouteFinder.SortPathsByOptimal(filteredPaths);
                    break;
            }

            var validRouteDTOs = new List<RouteDTO>();

            for (int routeIndex = 0; routeIndex < filteredPaths.Count; routeIndex++)
            {
                var route = filteredPaths[routeIndex];
                var routeSegments = new List<RouteSegmentDTO>();
                DateTime? previousArrivalTime = null;
                bool isRouteValid = true;

                for (int segmentIndex = 0; segmentIndex < route.Count; segmentIndex++)
                {
                    var segment = route[segmentIndex];
                    var seats = await GetAvailableSeatsInSegment(segment.Id);

                    // Check if there are available seats for the segment
                    if (seats.Count() == 0)
                    {
                        isRouteValid = false;
                        break; // Skip the current route if no seats are available
                    }

                    TimeSpan? idleTime = previousArrivalTime.HasValue
                        ? segment.DepartureDateTime - previousArrivalTime.Value
                        : null;

                    var segmentInfo = new RouteSegmentDTO
                    {
                        SegmentId = segment.Id,
                        DepartureId = segment.DepartureStarportId,
                        ArrivalId = segment.ArrivalStarportId,
                        JourneyId = segment.JourneyId,
                        DepartureDateTime = segment.DepartureDateTime,
                        ArrivalDateTime = segment.ArrivalDateTime,
                        Order = segment.Order,
                        Price = segment.Price,
                        TravelTime = segment.ArrivalDateTime - segment.DepartureDateTime,
                        IdleTime = idleTime
                    };

                    routeSegments.Add(segmentInfo);
                    previousArrivalTime = segment.ArrivalDateTime;
                }

                if (!isRouteValid)
                {
                    continue; // Skip the current route if it is not valid (no available seats)
                }

                var totalPrice = route.Sum(segment => segment.Price);
                var totalTime = route.Last().ArrivalDateTime - route.First().DepartureDateTime;

                var routeDTO = new RouteDTO
                {
                    Id = routeIndex,
                    RouteSegments = routeSegments,
                    TotalPrice = totalPrice,
                    TotalTravelTime = totalTime
                };

                var journeySeatMaps = await GenerateSeatMapForJourneys(route); // Generate seat map for the route
                if (journeySeatMaps == null)
                {
                    continue; // Skip the current route if seat map is null (no available seats in some segments)
                }

                routeDTO.JourneySeatMaps = journeySeatMaps;

                validRouteDTOs.Add(routeDTO);
            }

            return validRouteDTOs;
        }


        private async Task<List<JourneySeatMapDTO>?> GenerateSeatMapForJourneys(List<JourneySegment> path)
        {
            var result = new List<JourneySeatMapDTO>();

            List<List<JourneySegment>> groupedSegmentsByJourney = path
                .GroupBy(segment => segment.JourneyId)
                .Select(group => group.ToList())
                .ToList();

            foreach (var groupedSegment in groupedSegmentsByJourney)
            {
                var availableSeatsForJourney = await GetAvailableSeatsInJourney(groupedSegment);
                if (!availableSeatsForJourney.Any())
                {
                    return null;
                }
                else
                {
                    var seatAllocationsDTO = availableSeatsForJourney.Select(seatAllocation => new SeatAllocationDTO
                    {
                        Id = seatAllocation.Id,
                        SegmentId = seatAllocation.SegmentId,
                        SeatNumber = seatAllocation.SeatNumber,
                        IsVacant = seatAllocation.isVacant
                    }).ToList();

                    var newJourneySeatMap = new JourneySeatMapDTO
                    {
                        JourneyId = groupedSegment.First().JourneyId,
                        SeatsAvailable = seatAllocationsDTO
                    };

                    result.Add(newJourneySeatMap);
                }
            }

            return result;
        }


        public async Task<IEnumerable<SeatAllocation>> GetAvailableSeatsInJourney(List<JourneySegment> segments)
        {
            // Get the segment IDs from the given segments
            var segmentIds = segments.Select(segment => segment.Id).ToList();

            // Retrieve the available seats for the first segment
            var availableSeats = await GetAvailableSeatsInSegment(segmentIds.First());

            // Filter the seats to include only those available in all segments
            foreach (var segmentId in segmentIds.Skip(1))
            {
                var seats = await GetAvailableSeatsInSegment(segmentId);
                availableSeats = availableSeats.Where(seat => seats.Any(s => s.SeatNumber == seat.SeatNumber && s.isVacant)).ToList();
            }

            return availableSeats;
        }

        private List<int> GetAvailableSeatsForPath(List<JourneySegment> path)
        {
            var commonSeats = new List<int>();

            foreach (var segment in path)
            {
                var seats = GetAvailableSeatsInSegment(segment.Id).GetAwaiter().GetResult().Select(c=>c.SeatNumber);

                if (!commonSeats.Any())
                {
                    commonSeats.AddRange(seats);
                }
                else
                {
                    commonSeats = commonSeats.Intersect(seats).ToList();
                }

            }

            return commonSeats;
        }


        public async Task<IEnumerable<SeatAllocation>> GetAvailableSeatsInSegment(int segmentId)
        {
            var vacantSeats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId && sa.isVacant)
                .ToListAsync();

            return vacantSeats;
        }

        public async Task<IEnumerable<SeatAllocation>> GetNotAvailableSeatsInSegment(int segmentId)
        {
            var vacantSeats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId && !sa.isVacant)
                .ToListAsync();

            return vacantSeats;
        }

        public async Task<IEnumerable<SeatAllocation>> GetAllSeatsInSegment(int segmentId)
        {
            var seats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId)
                .ToListAsync();

            return seats;
        }


        public (string FolderAndFileName, string FilePath) GeneratePdfwithAppSettings(Ticket ticket)
        {
            // Get the template and generated output directories from appsettings
            var templateDirectory = _configuration["TicketsPaths:TEMPLATES"];
            var generatedDirectory = _configuration["TicketsPaths:GENERATED"];

            // Ensure the output directory exists
            if (!Directory.Exists(generatedDirectory))
            {
                Directory.CreateDirectory(generatedDirectory);
            }
            
            string ticketGuid = "ewe2345428-3838230-274242";
            var templateFilePath = Path.Combine(templateDirectory, "template1.pdf");
            var jFolder = $"JRN-{ticket.JourneyId}";
            var outputFolderAndFileName = $"{jFolder}/ticket-[{ticketGuid}]-[{ticket.BookingDateTime:yyyyMMdd_HHmmss}].pdf";
            var outputFilePath = Path.Combine(generatedDirectory, outputFolderAndFileName);

            // Ensure the journey folder exists
            var journeyFolder = Path.Combine(generatedDirectory, jFolder);
            if (!Directory.Exists(journeyFolder))
            {
                Directory.CreateDirectory(journeyFolder);
            }


            using (var templateStream = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    var reader = new PdfReader(templateStream);
                    var stamper = new PdfStamper(reader, outputStream);
                    var formFields = stamper.AcroFields;

                    // Fill in the passenger data on the template using form fields (if the template has them)
                    formFields.SetField("FirstNameField", ticket.PassengerFirstName);
                    formFields.SetField("LastNameField", ticket.PassengerSecondName);

                    // Optionally, add custom text to the PDF using the PdfContentByte object
                    var canvas = stamper.GetOverContent(1); // Page number 1
                    var font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    var fontSize = 60f;
                    canvas.BeginText();
                    canvas.SetFontAndSize(font, fontSize);
                    canvas.SetRGBColorFill(0, 0, 0); // Black color
                    canvas.SetTextMatrix(100, 100); // X and Y coordinates
                    canvas.ShowText($"{ticket.PassengerFirstName} {ticket.PassengerSecondName}");
                    canvas.EndText();

                    canvas.BeginText();
                    canvas.SetFontAndSize(font, fontSize);
                    canvas.SetRGBColorFill(0, 0, 0); // Black color
                    canvas.SetTextMatrix(50, 50); // X and Y coordinates
                    canvas.ShowText($"Journey: {ticket.JourneyId}");
                    canvas.EndText();

                    stamper.Close();
                    reader.Close();
                }
            }

            return (outputFolderAndFileName, outputFilePath);
        }

        public byte[] CombineAndReturnPdf(List<string> pdfFilePaths)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new combined PDF document
                using (Document document = new Document())
                {
                    PdfCopy copy = new PdfCopy(document, ms);
                    document.Open();

                    // Add each PDF to the combined document
                    foreach (var pdfFilePath in pdfFilePaths)
                    {
                        using (var pdfStream = new FileStream(pdfFilePath, FileMode.Open, FileAccess.Read))
                        {
                            var reader = new PdfReader(pdfStream);
                            copy.AddDocument(reader);
                            reader.Close();
                        }
                    }
                }

                // Return the combined PDF as a byte array
                return ms.ToArray();
            }
        }


    }
}
