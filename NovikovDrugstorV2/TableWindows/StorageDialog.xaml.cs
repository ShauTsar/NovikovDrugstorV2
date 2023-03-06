using System;
using System.Collections.Generic;
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
    /// Interaction logic for StorageDialog.xaml
    /// </summary>
    public partial class StorageDialog : Window
    {
        private readonly SqlConnection _connection;

        public StorageDialog(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM dbo.Drugs";
                SqlCommand cmd = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    DrugComboBox.Items.Add(new ComboBoxItem { Content = name, Tag = id });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public int SelectedDrugId { get; private set; }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
          
            if (DrugComboBox.SelectedItem is ComboBoxItem item && item.Tag is int id)
            {
                SelectedDrugId = id;
                DialogResult = true;

                try
                {
                    string query = $"INSERT INTO dbo.Storage (DrugID) VALUES ({SelectedDrugId})";
                    SqlCommand cmd = new SqlCommand(query, _connection);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    _connection.Close();
                }
            
                
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

}
