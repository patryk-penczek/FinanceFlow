using FinanceFlow.Persistence;
using FinanceFlow.Model;
using ReactiveUI;
using FinanceFlow.Service;
using FinanceFlow.Views;

namespace FinanceFlow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        private IAppDbContext _appDbContext;
        private readonly ExpenseService _expenseService;

        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            _appDbContext = new AppDbContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            _expenseService = new ExpenseService(_appDbContext);
            CurrentView = new ExpenseViewModel(this, _appDbContext);
        }

        public void ShowExpense()
        {
            CurrentView = new ExpenseViewModel(this, _appDbContext);
        }

        public void ShowCreateExpense()
        {
            CurrentView = new CreateExpenseViewModel(this, _appDbContext);
        }

        public void ShowCategory()
        {
            CurrentView = new CategoriesViewModel(this, _appDbContext);
        }

        public void ShowCreateCategory()
        {
            CurrentView = new CreateCategoryViewModel(this, _appDbContext);
        }

        public void ShowUpdateExpense(Expense selectedExpense)
        {
            CurrentView = new UpdateExpenseViewModel(this, _appDbContext, selectedExpense);
        }

        public void ShowUpdateCategory(Category category)
        {
            CurrentView = new UpdateCategoryView { DataContext = new UpdateCategoryViewModel(category, new CategoryService(_appDbContext), this) };
        }
    }
}
