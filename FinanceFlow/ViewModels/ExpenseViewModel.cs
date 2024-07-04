using FinanceFlow.Model;
using FinanceFlow.Persistence;
using FinanceFlow.Service;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Linq;

namespace FinanceFlow.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ExpenseService _expenseService;
        private readonly CategoryService _categoryService;

        public ObservableCollection<Expense> Expenses { get; }
        public ObservableCollection<Category> Categories { get; }

        private Expense _selectedExpense;
        public Expense SelectedExpense
        {
            get => _selectedExpense;
            set => this.RaiseAndSetIfChanged(ref _selectedExpense, value);
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        private decimal _totalExpenses;
        public decimal TotalExpenses
        {
            get => _totalExpenses;
            set => this.RaiseAndSetIfChanged(ref _totalExpenses, value);
        }

        public ReactiveCommand<Unit, Unit> CreateExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenUpdateExpenseWindowCommand { get; }

        public ExpenseViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _expenseService = new ExpenseService(appDbContext);
            _categoryService = new CategoryService(appDbContext);

            CreateExpenseCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowCreateExpense());
            RemoveExpenseCommand = ReactiveCommand.CreateFromTask(RemoveExpenseAsync);
            ShowExpenseCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowExpense());
            ShowCategoryCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowCategory());
            OpenUpdateExpenseWindowCommand = ReactiveCommand.Create(UpdateExpense);

            Expenses = new ObservableCollection<Expense>();
            Categories = new ObservableCollection<Category>();

            LoadData();
            CalculateTotalExpenses();
        }

        private async Task RemoveExpenseAsync()
        {
            try
            {
                if (_selectedExpense != null)
                {
                    await _expenseService.DeleteExpense(_selectedExpense.Id);
                    Expenses.Remove(_selectedExpense);
                    CalculateTotalExpenses();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing expense: {ex.Message}");
            }
        }

        private void UpdateExpense()
        {
            try
            {
                if (_selectedExpense != null)
                {
                    _mainWindowViewModel.ShowUpdateExpense(_selectedExpense);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating expense: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                LoadExpenses();
                LoadCategories();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void LoadExpenses()
        {
            try
            {
                Expenses.Clear();
                var expenses = _expenseService.GetExpenses().ToList();

                foreach (var expense in expenses)
                {
                    Expenses.Add(expense);
                }

                CalculateTotalExpenses();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading expenses: {ex.Message}");
            }
        }

        private void LoadCategories()
        {
            try
            {
                Categories.Clear();
                var categories = _categoryService.GetCategories();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        private void CalculateTotalExpenses()
        {
            TotalExpenses = Expenses.Sum(e => e.Amount);
        }
    }
}
