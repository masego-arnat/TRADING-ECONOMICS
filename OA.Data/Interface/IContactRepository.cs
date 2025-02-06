using OA.Data.Entities;

namespace OA.Data.Interface
{

    public interface IContactRepository : IRepository<Contact>, IAsyncRepository<Contact>

    {
    }
}
