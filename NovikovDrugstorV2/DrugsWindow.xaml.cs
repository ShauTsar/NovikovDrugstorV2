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

namespace NovikovDrugstorV2
{
    /// <summary>
    /// Interaction logic for DrugsWindow.xaml
    /// </summary>
    public partial class DrugsWindow : Window
    {
        string medicineType;
        string medicineName;
        string dateFrom;
        string dateTo;
        public DrugsWindow(bool date = false, bool dateAndCategory = false, bool onlyClass=false, bool onlyDrug = false)
        {

            InitializeComponent();
            if (date == true)
            {
                dateFromDp.Visibility = Visibility.Visible;
                dateToDp.Visibility = Visibility.Visible;
            }
            else if(dateAndCategory== true)
            {
                dateFromDp.Visibility = Visibility.Visible;
                dateToDp.Visibility = Visibility.Visible;
                Lb1.Visibility = Visibility.Visible;
                Lb2.Visibility = Visibility.Visible;
                cmbMedicineName.Visibility = Visibility.Visible;
                cmbMedicineType.Visibility = Visibility.Visible;
            }
            else if(onlyClass)
            {
                Lb1.Visibility = Visibility.Visible;
                cmbMedicineType.Visibility = Visibility.Visible;
            }
            else if(onlyDrug)
            {
                Lb2.Visibility = Visibility.Visible;
                cmbMedicineName.Visibility = Visibility.Visible;
            }
            else 
            {
                Lb1.Visibility = Visibility.Visible;
                Lb2.Visibility = Visibility.Visible;
                cmbMedicineName.Visibility = Visibility.Visible;
                cmbMedicineType.Visibility = Visibility.Visible;
            }
           
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
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
            if (dateFromDp.SelectedDate.HasValue) dateFrom = dateFromDp.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"); 
            else if (dateToDp.SelectedDate.HasValue) dateTo = dateToDp.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"); 
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
        public string DateFrom
        {
            get { return dateFrom; }
        }
        public string DateTo
        {
            get { return dateTo; }
        }
    }
}
