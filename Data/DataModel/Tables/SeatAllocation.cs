namespace NoGravity.Data.Tables
{
    public class SeatAllocation
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("JourneySegment")]
        public int SegmentId { get; set; }
        public virtual JourneySegment Segment{ get; set; }


        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public bool isVacant { get; set; }
        
        
    }
}
