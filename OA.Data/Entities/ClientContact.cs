using System.ComponentModel.DataAnnotations;

namespace OA.Data.Entities
{
    public class ClientContact
    {
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
