using OA.Data.Entities;

namespace OA.Service.Interface
{
    public interface IClientContactService
    {
        Task AddClientContactAsync(int clientId, int contactId);
        Task RemoveClientContact(int clientId, int contactId);

        IEnumerable<ClientContact> ListClientContact();
        //Task EditClientContactAsync(ClientContact clientContact);
    }
}
