using System.ComponentModel.DataAnnotations;

namespace BCity.Models
{
    public class ClientViewModel
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }

        public int NumbersLinked { get; set; }
    }
}
