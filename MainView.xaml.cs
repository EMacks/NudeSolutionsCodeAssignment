using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSApp
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } set { DataContext = value; } }
        public MainView()
        {
            InitializeComponent();
            cboCategories.ItemsSource = new string[] { "Electronics", "Clothing", "Kitchen" };
            ViewModel = new MainViewModel();
            Refresh();
        }
        public  void Refresh()
        {
             ViewModel.Refresh();
        }

        private void CreateNewProduct(object sender, RoutedEventArgs e)
        {
            ViewModel.NewProduct.Save();
            Refresh();
            ViewModel.NewProduct.Clear();
        }

        private void ClearNewProduct(object sender, RoutedEventArgs e)
        {
            ViewModel.NewProduct.Clear();
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Product p = (Product)b.DataContext;
            p.Delete();
            Refresh();
        }
    }
}
