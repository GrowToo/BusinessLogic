using DAL.Concrete;
using DTO;
using System.Windows;

namespace WpfItemReceiver
{
    public partial class EditProductWindow : Window
    {
        private readonly ProductDal _productDal;
        private readonly ProductDto _product;

        public EditProductWindow(ProductDal productDal, ProductDto product)
        {
            InitializeComponent();
            _productDal = productDal;
            _product = product;

            // Ініціалізуємо поля
            ProductNameBox.Text = _product.Name;
            ProductPriceBox.Text = _product.Price.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _product.Name = ProductNameBox.Text;
            _product.Price = decimal.Parse(ProductPriceBox.Text);

            _productDal.Update(_product);
            MessageBox.Show("Product updated successfully!");
            Close();
        }
    }
}
