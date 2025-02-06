using OA.Data.Entities;
namespace OA.Data.Interface
{
    public interface IClientRepository : IRepository<Client>, IAsyncRepository<Client>
    {
    }
}

