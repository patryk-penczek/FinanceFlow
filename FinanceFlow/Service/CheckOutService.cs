using FinanceFlow.Model;
using FinanceFlow.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceFlow.Service
{
    public class CheckOutService
    {
        private readonly IAppDbContext _appDbContext;

        public CheckOutService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CheckOut> GetCheckOuts()
        {
            return _appDbContext.CheckOuts;
        }

        public async Task<CheckOut> CreateCheckOut(string name, string lastname)
        {
            var chechout = new CheckOut()
            {
                Name = name,
                LastName = lastname,
                CheckOutDate = DateTime.Now,
                ReturnDate = DateTime.Now,
            };

            var result = _appDbContext.CheckOuts.Add(chechout);
            await _appDbContext.SaveChangesAsync();
            return result;
        }
    }
}
