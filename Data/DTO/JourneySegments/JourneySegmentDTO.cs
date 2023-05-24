namespace NoGravity.Data.DTO.JourneySegments
{
    public class JourneySegmentDTO
    {
        public int Id { get; set; }
        public int DepartureStarportId { get; set; }
        public int ArrivalStarportId { get; set; }
        public int Order { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public decimal Price { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
