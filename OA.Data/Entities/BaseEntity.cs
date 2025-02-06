using System.ComponentModel.DataAnnotations;

namespace OA.Data.Entities
{
    public class BaseEntity
    {

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        [Required]
        public bool IsActive { get; set; }
    }
}
