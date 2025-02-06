using OA.Data.Entities;

namespace OA.Service.Interface
{
    public interface IContactService
    {
        Task AddContact(Contact client);
        IEnumerable<Contact> ListContact();
        Task<Contact> GetContactById(int id);

        Task EditContact(Contact  contact);

        Task DeleteContact(Contact  contact);
    }
}
