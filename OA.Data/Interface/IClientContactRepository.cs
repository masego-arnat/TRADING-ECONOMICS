using OA.Data.Entities;

namespace OA.Data.Interface
{
    public interface IClientContactRepository : IRepository<ClientContact>, IAsyncRepository<ClientContact>
    {
    }
}
