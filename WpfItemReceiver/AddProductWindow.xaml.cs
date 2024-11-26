using DAL.Concrete;
using DTO;
using System.Windows;

namespace WpfItemReceiver
{
    public partial class AddProductWindow : Window
    {
        private readonly ProductDal _productDal;

        public AddProductWindow(ProductDal productDal)
        {
            InitializeComponent();
            _productDal = productDal;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameBox.Text;
            decimal price = decimal.Parse(ProductPriceBox.Text);

            var newProduct = new ProductDto { Name = name, Price = price };
            _productDal.Add(newProduct);

            MessageBox.Show("Product added successfully!");
            Close();
        }
    }
}
