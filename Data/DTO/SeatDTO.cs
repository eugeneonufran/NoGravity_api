namespace NoGravity.Data.DTO
{
    public class SeatDTO
    {
        public int Id { get; set; }

        public int SegmentId { get; set; }
        public int SeatNumber { get; set; }
        public bool IsVacant { get; set; }

    }

}
