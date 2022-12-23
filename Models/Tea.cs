using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teatastic.Models
{
    public class Tea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [Required]
        public double Price { get; set; }

        //Relationship
        public List<Function>? Functions { get; set; }

        [NotMapped]
        public List<int> FunctionIds { get; set; }

    }
}
