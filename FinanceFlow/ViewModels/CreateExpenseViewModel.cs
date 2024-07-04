using System;
using System.Collections.ObjectModel;
using System.Reactive;
using FinanceFlow.Model;
using FinanceFlow.Persistence;
using FinanceFlow.Service;
using ReactiveUI;

namespace FinanceFlow.ViewModels
{
    public class CreateExpenseViewModel : ViewModelBase
    {
        private string _expenseDate;
        private string _expenseName;
        private Category _selectedCategory;
        private decimal _amount;
        private ObservableCollection<Category> _categories;
        private MainWindowViewModel _mainWindowViewModel;
        private ExpenseService _expenseService;

        public string ExpenseDate
        {
            get => _expenseDate;
            set => this.RaiseAndSetIfChanged(ref _expenseDate, value);
        }

        public string ExpenseName
        {
            get => _expenseName;
            set => this.RaiseAndSetIfChanged(ref _expenseName, value);
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

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public ReactiveCommand<Unit, Unit> CreateExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateExpenseViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _expenseService = new ExpenseService(appDbContext);

            Categories = new ObservableCollection<Category>(appDbContext.Categories);

            CreateExpenseCommand = ReactiveCommand.Create(HandleCreateCommand);
            BackCommand = ReactiveCommand.Create(NavigateBack);
        }

        private async void HandleCreateCommand()
        {
            if (SelectedCategory == null)
            {
                return;
            }

            await _expenseService.CreateExpense(ExpenseDate, ExpenseName, SelectedCategory.Id, SelectedCategory, Amount);
            _mainWindowViewModel.ShowExpense();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowExpense();
        }
    }
}
