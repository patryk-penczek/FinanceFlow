using FinanceFlow.Persistence;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using FinanceFlow.Service;
using FinanceFlow.Model;

namespace FinanceFlow.ViewModels
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        private string _categoryName;

        private MainWindowViewModel _mainWindowViewModel;

        private CategoryService _categoryService;

        public string CategoryName
        {
            get => _categoryName;
            set => this.RaiseAndSetIfChanged(ref _categoryName, value);
        }

        public ReactiveCommand<Unit, Unit> CreateCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _categoryService = new CategoryService(appDbContext);

            CreateCategoryCommand = ReactiveCommand.Create(HandleCreateCommand);
            BackCommand = ReactiveCommand.Create(NavigateBack);
        }

        private async void HandleCreateCommand()
        {
            await _categoryService.CreateCategory(_categoryName);
            _mainWindowViewModel.ShowCategory();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowCategory();
        }
    }
}
