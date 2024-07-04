using FinanceFlow.Model;
using FinanceFlow.Service;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using FinanceFlow.Persistence;

namespace FinanceFlow.ViewModels
{
    public class UpdateExpenseViewModel : ViewModelBase
    {
        private readonly ExpenseService _expenseService;
        private string _date;
        private string _name;
        private Category _selectedCategory;
        private readonly Expense _selectedExpense;
        private MainWindowViewModel _mainWindowViewModel;
        private decimal _amount;

        public ReactiveCommand<Unit, Unit> NavigateBackCommand { get; }

        public UpdateExpenseViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Expense expense)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _selectedExpense = expense;
            _expenseService = new ExpenseService(appDbContext);

            Date = expense.Date;
            Name = expense.Name;
            SelectedCategory = expense.Category;
            Amount = expense.Amount;

            Categories = new ObservableCollection<Category>(appDbContext.Categories);

            UpdateExpenseCommand = ReactiveCommand.Create(UpdateExpense);
            NavigateBackCommand = ReactiveCommand.Create(NavigateBack);
        }

        public string Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public decimal Amount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        public ObservableCollection<Category> Categories { get; }
        public ReactiveCommand<Unit, Unit> UpdateExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        private async void UpdateExpense()
        {
            var updatedExpense = new Expense
            {
                Id = _selectedExpense.Id,
                Date = Date,
                Name = Name,
                CategoryId = SelectedCategory.Id,
                Category = SelectedCategory,
                Amount = Amount
            };

            await _expenseService.UpdateExpense(updatedExpense.Id, updatedExpense.Date, updatedExpense.Name, updatedExpense.CategoryId, updatedExpense.Category, updatedExpense.Amount);
            _mainWindowViewModel.ShowExpense();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowExpense();
        }
    }
}
