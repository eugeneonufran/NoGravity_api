namespace NoGravity.Data.DTO.Booking
{
    public class JourneySeatMapDTO
    {
        public int JourneyId { get; set; }
        public List<SeatAllocationDTO> SeatsAvailable { get; set; }
    }
}
