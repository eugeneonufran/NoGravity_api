using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoGravity.Data.Tables
{
    public class Starport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Planet")]
        public int PlanetId { get; set; }
        public virtual Planet Planet { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
