using OA.Data.Entities;

namespace OA.Service.Interface
{
    public interface IClientService
    {
        Task AddClient(Client client);
        IEnumerable<Client> ListClients();
        Task<Client> GetClientById(int id);

        Task EditClient(Client  client); 
        Task DeleteClient(Client  client);
    }
}
