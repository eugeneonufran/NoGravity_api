using NoGravity.Data.DTO;
using NoGravity.Data.Tables;


public interface ITicketsDataService
{
    Task<List<RouteDTO>> GetPossibleRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal);
    Task<IEnumerable<SeatAllocation>> GetAvailableSeatsInSegment(int segmentId);
    Task<IEnumerable<SeatAllocation>> GetAllSeatsInSegment(int segmentId);

    public (string FolderAndFileName, string FilePath) GeneratePdfwithAppSettings(Ticket ticket);
    byte[] CombineAndReturnPdf(List<string> pdfFilePaths);


}
