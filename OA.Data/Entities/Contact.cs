using System.ComponentModel.DataAnnotations;

namespace OA.Data.Entities
{
    public class Contact : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int NumbersLinked { get; set; }
        public ICollection<ClientContact> ClientContacts { get; set; }


    }
}
