using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoGravity.Data.Tables
{
    public class Journey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Starcraft")]
        public int StarcraftId { get; set; }
        public virtual Starcraft Starcraft { get; set; }
    }
}
