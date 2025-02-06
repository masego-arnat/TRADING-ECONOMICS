using OA.Data.Entities;
using OA.Data.Interface;

namespace OA.Repo.Repo
{
    public class ClientRepository : EfRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public ApplicationContext TapAMealContext => Context as ApplicationContext;
    }
}

