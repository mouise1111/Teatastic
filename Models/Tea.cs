using System.ComponentModel.DataAnnotations;

namespace Teatastic.Models
{
    public class Tea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public double Price { get; set; }

        //Relationship
        public List<Tea_Function> Teas_Functions { get; set; }

    }
}
