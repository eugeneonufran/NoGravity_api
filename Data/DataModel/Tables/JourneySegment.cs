namespace NoGravity.Data.Tables
{
    public class JourneySegment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int JourneyId { get; set; }
        public virtual Journey Journey { get; set; }

        [ForeignKey("DepartureStarport")]
        public int DepartureStarportId { get; set; }
        public virtual Starport DepartureStarport { get; set; }

        [ForeignKey("ArrivalStarport")]
        public int ArrivalStarportId { get; set; }
        public virtual Starport ArrivalStarport { get; set; }


        [Required]
        public int Order { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }
    }
}
