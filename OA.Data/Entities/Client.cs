using System.ComponentModel.DataAnnotations;

namespace OA.Data.Entities
{
    public class Client : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int NumbersLinked { get; set; }
        public ICollection<ClientContact> ClientContacts { get; set; }

    }
}
