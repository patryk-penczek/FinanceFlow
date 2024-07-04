using System.Data.Entity;
using System.Threading.Tasks;
using FinanceFlow.Model;

namespace FinanceFlow.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Expense> Expenses { get; set; }
        DbSet<CheckOut> CheckOuts { get; set; }
        Task<int> SaveChangesAsync();
    }
}
