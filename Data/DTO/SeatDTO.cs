namespace NoGravity.Data.DTO
{
    public class SeatDTO
    {
        public int SeatNumber { get; set; }
        public SeatColor Color { get; set; }

        public SeatDTO(int seatNumber, SeatColor color) { 
            SeatNumber = seatNumber;
            Color = color;
        }
    }

    public enum SeatColor
    {
        Grey,
        Green
        
    }

    public class SegmentSeatMapDTO
    {
        public SegmentSeatMapDTO(int segmentId, IEnumerable<SeatDTO> seatMap)
        {
            SegmentId = segmentId;
            SeatMap = seatMap;
        }

        public int SegmentId { get; set; }
        public IEnumerable<SeatDTO> SeatMap { get; set; }
        public bool HasSeatChange { get; set; }
    }

    public class RouteSeatMapDTO
    {
        public RouteSeatMapDTO(List<SegmentSeatMapDTO> segmentSeatMaps)
        {
            SegmentSeatMaps = segmentSeatMaps;
        }

        public List<SegmentSeatMapDTO> SegmentSeatMaps { get; set; }


    }
}
