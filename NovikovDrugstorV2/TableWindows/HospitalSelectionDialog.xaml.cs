using System;
using System.Collections.Generic;
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
    /// Interaction logic for HospitalSelectionDialog.xaml
    /// </summary>
    public partial class HospitalSelectionDialog : Window
    {
        public int SelectedHospitalId { get; private set; }
        public string SelectedHospitalName { get; set; }

        public HospitalSelectionDialog()
        {
            InitializeComponent();
            LoadHospitals();
        }

        private void LoadHospitals()
        {
            string query = "SELECT ID, Name FROM dbo.Hospitals ORDER BY Name";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(@"Data Source=ES371\MSSQLSERVER01;Initial Catalog=NovikovDrugstore;Integrated Security=True"))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            HospitalsList.ItemsSource = table.DefaultView;
            HospitalsList.DisplayMemberPath = "Name";
            HospitalsList.SelectedValuePath = "ID"; // Add this line to specify the ValueMemberPath
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)HospitalsList.SelectedItem;
            if (selectedRow != null)
            {
                SelectedHospitalId = (int)HospitalsList.SelectedValue;
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

