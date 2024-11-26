using DAL.Concrete;
using DTO;
using System.Windows;

namespace WpfItemReceiver
{
    public partial class EditCategoryWindow : Window
    {
        private readonly CategoryDal _categoryDal;
        private readonly CategoryDto _category;

        public EditCategoryWindow(CategoryDal categoryDal, CategoryDto category)
        {
            InitializeComponent();
            _categoryDal = categoryDal;
            _category = category;

            // Ініціалізуємо поля
            CategoryNameBox.Text = _category.Name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _category.Name = CategoryNameBox.Text;

            _categoryDal.Update(_category);
            MessageBox.Show("Category updated successfully!");
            Close();
        }
    }
}
