using OA.Data.Entities;
using OA.Data.Interface;

namespace OA.Repo.Repo
{
    public class ClientContactRepository : EfRepository<ClientContact>, IClientContactRepository
    {
        public ClientContactRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public ApplicationContext TapAMealContext => Context as ApplicationContext;
    }
}
