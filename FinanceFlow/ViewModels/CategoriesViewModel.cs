using FinanceFlow.Model;
using FinanceFlow.Persistence;
using FinanceFlow.Service;
using FinanceFlow.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace FinanceFlow.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _categories;
        private Category _selectedCategory;

        private MainWindowViewModel _mainWindowViewModel;
        private CategoryService _categoryService;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public ReactiveCommand<Unit, Unit> ShowExpenseCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCategoryCommand { get; }

        public CategoriesViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _categoryService = new CategoryService(appDbContext);

            ShowExpenseCommand = ReactiveCommand.Create(ShowExpense);
            ShowCategoryCommand = ReactiveCommand.Create(ShowCategories);
            RemoveCategoryCommand = ReactiveCommand.Create(RemoveCategory);
            CreateCategoryCommand = ReactiveCommand.Create(CreateCategory);
            UpdateCategoryCommand = ReactiveCommand.Create(UpdateCategory);
            Categories = new ObservableCollection<Category>();
            RefreshCategories();
        }

        private void ShowExpense()
        {
            _mainWindowViewModel.ShowExpense();
        }
        private void ShowCategories()
        {
            _mainWindowViewModel.ShowCategory();
        }

        private void CreateCategory()
        {
            _mainWindowViewModel.ShowCreateCategory();
        }

        private void UpdateCategory()
        {
            if (SelectedCategory != null)
            {
                _mainWindowViewModel.ShowUpdateCategory(SelectedCategory);
            }
        }

        private async void RemoveCategory()
        {
            if (SelectedCategory != null)
            {
                await _categoryService.DeleteCategory(_selectedCategory.Id);
                RefreshCategories();
            }
        }

        private void RefreshCategories()
        {
            Categories.Clear();
            var categories = _categoryService.GetCategories();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
    }
}
