using System.ComponentModel.DataAnnotations;

namespace Teatastic.Models
{
    // The function show the purpose of the tea (relaxing, energizing, etc.)
    public class Function
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        //Relationship
        public List<Tea_Function> Teas_Functions { get; set; }
    }
}
