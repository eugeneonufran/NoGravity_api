namespace NoGravity.Data.DTO.Tickets
{
    public class TicketDTO
    {
        public int JourneyId { get; set; }
        public int StartStarportId { get; set; }
        public int EndStarportId { get; set; }
        public string PassengerFirstName { get; set; }
        public string PassengerSecondName { get; set; }
        public string CIF { get; set; }
        public int UserId { get; set; }
        public int SeatNumber { get; set; }
    }
}
