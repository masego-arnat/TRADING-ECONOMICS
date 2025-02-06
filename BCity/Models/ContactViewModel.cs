using System.ComponentModel.DataAnnotations;

namespace BCity.Models
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public int NumbersLinked { get; set; }
    }
}
