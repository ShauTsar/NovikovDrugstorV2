using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
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
using System.Data.SqlClient;
using System.Configuration;

namespace NovikovDrugstorV2.TableWindows
{
    /// <summary>
    /// Interaction logic for TechnicalManualDialog.xaml
    /// </summary>
    public partial class TechnicalManualDialog : Window
    {
        public int SelectedDrugId { get; set; }
        public string DrugName { get; set; }

        public TechnicalManualDialog()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT ID, Name FROM dbo.Drugs";;
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                using (var command = new SqlCommand(query, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }

                DrugComboBox.ItemsSource = table.DefaultView;
                DrugComboBox.DisplayMemberPath = "Name";
                DrugComboBox.SelectedValuePath = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)DrugComboBox.SelectedItem;
            if (selectedRow != null)
            {
                SelectedDrugId = (int)DrugComboBox.SelectedValue;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a hospital.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }

}
