namespace NoGravity.Data.DTO
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public List<RouteSegmentDTO> RouteSegments { get; set; }
        public decimal TotalPrice { get; set; }
        public TimeSpan TotalTravelTime { get; set; }

    }

}
