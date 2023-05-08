using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoGravity.Data.Tables
{
    public class Starcraft
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Carrier")]
        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
