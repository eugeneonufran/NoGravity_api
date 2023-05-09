namespace NoGravity.Data.DTO
{
    public class RouteSegmentDTO
    {
        public int SegmentId { get; set; }
        public int JourneyId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int Order { get; set; }
        public decimal Price { get; set; }
        public TimeSpan TravelTime { get; set; }
        public TimeSpan? IdleTime { get; set; }
    }
}
