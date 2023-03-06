using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace NovikovDrugstorV2.TableWindows
{
  
    public partial class ReleaseDialog : Window
    {
        public int SelectedOrderId { get; private set; }
        public int SelectedReceipt { get;private set; }

        public ReleaseDialog(bool onlyOrders = false, bool onlyRecipes=false)
        {
            InitializeComponent();
            if(onlyOrders)
            {
                LoadOrders();
            }
            else if(onlyRecipes)
            {
                LoadReceipt();
            }
            else
            {
                LoadOrders();
                LoadReceipt();
            }
            
            
        }

        private void LoadOrders()
        {
            OrdersList.Visibility = Visibility.Visible;
            OrdersTB.Visibility = Visibility.Visible;
               
            string query = "SELECT ID FROM dbo.OrdersForIssue ORDER BY ID";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            OrdersList.ItemsSource = table.DefaultView;
            OrdersList.DisplayMemberPath = "ID";
            OrdersList.SelectedValuePath = "ID"; // Add this line to specify the ValueMemberPath
        }
        private void LoadReceipt()
        {
            RecipesList.Visibility = Visibility.Visible;
            RecipesTB.Visibility = Visibility.Visible;
            string query = "SELECT ID FROM dbo.Recipes ORDER BY ID";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            RecipesList.ItemsSource = table.DefaultView;
            RecipesList.DisplayMemberPath = "ID";
            RecipesList.SelectedValuePath = "ID";
        }



        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(OrdersList.IsVisible)
            {
                SelectedOrderId = (int)OrdersList.SelectedValue;
            }
            if(RecipesList.IsVisible)
            {
                SelectedReceipt = (int)RecipesList.SelectedValue;
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

}
