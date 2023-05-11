using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoGravity.Data.Tables
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Journey")]
        public int JourneyId { get; set; }
        public virtual Journey Journey { get; set; }

        [ForeignKey("StartStarport")]
        public int StartStarportId { get; set; }
        public virtual Starport StartStarport { get; set; }

        [ForeignKey("EndStarport")]
        public int EndStarportId { get; set; }
        public virtual Starport EndStarport { get; set; }

        [Required]
        public string PassengerFirstName { get; set; }

        [Required]
        public string PassengerSecondName { get; set; }

        [Required]
        public string CIF { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [ForeignKey("SeatNumber")]
        public int SeatId { get; set; }
        public virtual SeatAllocation SeatNumber { get; set; }

       

        [Required]
        public DateTime BookingDateTime { get; set; }
    }
}
