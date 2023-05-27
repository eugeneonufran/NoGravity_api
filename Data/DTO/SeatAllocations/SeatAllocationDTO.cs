namespace NoGravity.Data.DTO.SeatAllocations
{
    public class SeatAllocationDTO
    {
        public int Id { get; set; }

        public int SegmentId { get; set; }
        public int SeatNumber { get; set; }
        public bool IsVacant { get; set; }

    }

}
