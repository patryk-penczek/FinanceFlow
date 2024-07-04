using FinanceFlow.Model;
using FinanceFlow.Service;
using ReactiveUI;
using System;
using System.Reactive;

namespace FinanceFlow.ViewModels
{
    public class UpdateCategoryViewModel : ViewModelBase
    {
        private Category _selectedCategory;
        private CategoryService _categoryService;
        private MainWindowViewModel _mainWindowViewModel;

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public UpdateCategoryViewModel(Category selectedCategory, CategoryService categoryService, MainWindowViewModel mainWindowViewModel)
        {
            _selectedCategory = selectedCategory;
            _categoryService = categoryService;
            _mainWindowViewModel = mainWindowViewModel;

            UpdateCommand = ReactiveCommand.Create(Update);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }

        private async void Update()
        {
            await _categoryService.UpdateCategory(SelectedCategory);
            _mainWindowViewModel.ShowCategory();
        }

        private void Cancel()
        {
            _mainWindowViewModel.ShowCategory();
        }
    }
}
