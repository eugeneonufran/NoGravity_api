namespace NoGravity.Data.DTO
{
    public class OrderRequestDTO
    {
        public RouteDTO Route { get; set; }
        public List<PassengerInputDTO> Passengers { get; set; }
        public bool ActuallyCreateTicket { get; set; }
    }

}
