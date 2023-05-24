namespace NoGravity.Data.Tables
{
    public class Carrier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

     

        [Required]
        public string Description { get; set; }
    }
}
