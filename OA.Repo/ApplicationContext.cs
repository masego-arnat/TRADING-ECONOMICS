using Microsoft.EntityFrameworkCore;
using OA.Data.Entities;
namespace OA.Repo
{
    public class ApplicationContext : DbContext
    {


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContact { get; set; }
    }
}
