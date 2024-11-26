using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryWindow();
            categoryWindow.Show();
        }
    }
}
