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

namespace NovikovDrugstorV2
{
    /// <summary>
    /// Interaction logic for DrugsWindow.xaml
    /// </summary>
    public partial class DrugsWindow : Window
    {
        string medicineType;
        string medicineName;
        bool allMedicine = false;
        public DrugsWindow()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(@"Data Source=ES371\MSSQLSERVER01;Initial Catalog=NovikovDrugstore;Integrated Security=True");
            string query = "SELECT NameOfClass FROM Classes";
            string query2 = "SELECT Name FROM Drugs";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, connection);
            DataTable dataTable = new DataTable();
            DataTable dataTable2 = new DataTable();
            adapter.Fill(dataTable);
            adapter2.Fill(dataTable2);
            cmbMedicineType.ItemsSource = dataTable.DefaultView;
            cmbMedicineType.DisplayMemberPath = "NameOfClass";
            cmbMedicineName.ItemsSource = dataTable2.DefaultView;
            cmbMedicineName.DisplayMemberPath = "Name";
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            medicineType = cmbMedicineType.Text;
            medicineName = cmbMedicineName.Text;
            if(chBxAllMedicine.IsChecked == true) { allMedicine = true;}
            this.DialogResult = true;
        }
        public string MedicineType
        {
            get { return medicineType ; }
        }
        public string MedicineName
        {
            get { return medicineName; }
        }
        public bool AllMedicine
        {
            get { return allMedicine; }
        }
    }
}
