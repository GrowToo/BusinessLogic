using DAL.Concrete;
using DTO;
using System.Windows;

namespace WpfItemReceiver
{
    public partial class AddCategoryWindow : Window
    {
        private readonly CategoryDal _categoryDal;

        public AddCategoryWindow(CategoryDal categoryDal)
        {
            InitializeComponent();
            _categoryDal = categoryDal;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = CategoryNameBox.Text;

            var newCategory = new CategoryDto { Name = name };
            _categoryDal.Add(newCategory);

            MessageBox.Show("Category added successfully!");
            Close();
        }
    }
}
