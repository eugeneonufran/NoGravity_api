namespace NoGravity.Data.DTO.SeatAllocations
{
    public class SeatAllocationDTO
    {
        public int Id { get; set; }

        public int segmentId { get; set; }
        public int seatNumber { get; set; }
        public bool isVacant { get; set; }

    }

}
