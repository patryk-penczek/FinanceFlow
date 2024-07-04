using System.Data.Entity;
using System.Threading.Tasks;
using FinanceFlow.Model;

namespace FinanceFlow.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public AppDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
