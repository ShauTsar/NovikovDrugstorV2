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
    /// <summary>
    /// Interaction logic for DrugDialog.xaml
    /// </summary>
    public partial class DrugDialog : Window
    {
        public int SelectedClassId { get; private set; }
        public string SelectedClassName { get; set; }

        public DrugDialog()
        {
            InitializeComponent();
            LoadClasses();
        }

        private void LoadClasses()
        {
            string query = "SELECT ID, NameOfClass FROM dbo.Classes ORDER BY NameOfClass";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            ClassesList.ItemsSource = table.DefaultView;
            ClassesList.DisplayMemberPath = "NameOfClass";
            ClassesList.SelectedValuePath = "ID"; // Add this line to specify the ValueMemberPath
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)ClassesList.SelectedItem;
            if (selectedRow != null)
            {
                SelectedClassId = (int)ClassesList.SelectedValue;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a class.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
