using FinanceFlow.Model;
using FinanceFlow.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceFlow.Service
{
    public class CategoryService
    {
        private readonly IAppDbContext _appDbContext;

        public CategoryService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _appDbContext.Categories;
        }

        public async Task<Category> CreateCategory(string name)
        {
            var category = new Category()
            {
                Name = name,
            };


            var result = _appDbContext.Categories.Add(category);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task UpdateCategory(Category category)
        {
            var categoryToUpdate = await _appDbContext.Categories.FindAsync(category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToRemove = await _appDbContext.Categories.FirstOrDefaultAsync(n => n.Id == id);
            if (categoryToRemove != null)
            {
                _appDbContext.Categories.Remove(categoryToRemove);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
