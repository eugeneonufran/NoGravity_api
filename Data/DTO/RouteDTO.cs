namespace NoGravity.Data.DTO
{
    public class RouteDTO
    {
        public List<RouteSegmentDTO> RouteSegments { get; set; }
        public decimal TotalPrice { get; set; }
        public TimeSpan TotalTravelTime { get; set; }
    }

}
