using OA.Data.Entities;
using OA.Data.Interface;

namespace OA.Repo.Repo
{
    public class ContactRepository : EfRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public ApplicationContext TapAMealContext => Context as ApplicationContext;
    }
}


