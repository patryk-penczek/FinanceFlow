using DynamicData;
using FinanceFlow.Model;
using FinanceFlow.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceFlow.Service
{
    public class ExpenseService
    {
        private readonly IAppDbContext _appDbContext;

        public ExpenseService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _appDbContext.Expenses;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public async Task<Expense> CreateExpense(string date, string name, int categoryId, Category category, decimal amount)
        {
            var expense = new Expense()
            {
                Name = name,
                Date = date,
                CategoryId = categoryId,
                Category = category,
                Amount = amount
            };

            var result = _appDbContext.Expenses.Add(expense);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var expenseToRemove = _appDbContext.Expenses.FirstOrDefault(n => n.Id == id);
            if (expenseToRemove != null)
            {
                _appDbContext.Expenses.Remove(expenseToRemove);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Expense> UpdateExpense(int id, string date, string name, int categoryId, Category category, decimal amount)
        {
            var existingExpense = _appDbContext.Expenses.FirstOrDefault(e => e.Id == id);
            if (existingExpense != null)
            {
                existingExpense.Date = date;
                existingExpense.Name = name;
                existingExpense.CategoryId = categoryId;
                existingExpense.Category = category;
                existingExpense.Amount = amount;

                await _appDbContext.SaveChangesAsync();
                return existingExpense;
            }
            return null;
        }
    }
}
