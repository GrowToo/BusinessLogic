using DAL.Concrete;
using DTO;
using System.Windows;

namespace WpfItemReceiver
{
    public partial class MainWindow : Window
    {
        private ProductDal _productDal;
        private CategoryDal _categoryDal;

        public MainWindow()
        {
            InitializeComponent();

            // Ініціалізація DAL
            string connectionString = DBConfiguration.ConnectionString;
            _productDal = new ProductDal(connectionString);
            _categoryDal = new CategoryDal(connectionString);

            // Завантаження даних
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _productDal.GetAll();
            ProductGrid.ItemsSource = products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_productDal);
            addProductWindow.ShowDialog();
            LoadProducts();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is ProductDto selectedProduct)
            {
                var editProductWindow = new EditProductWindow(_productDal, selectedProduct);
                editProductWindow.ShowDialog();
                LoadProducts();
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryWindow(_categoryDal);
            addCategoryWindow.ShowDialog();
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            var editCategoryWindow = new EditCategoryWindow(_categoryDal);
            editCategoryWindow.ShowDialog();
        }
    }
}
