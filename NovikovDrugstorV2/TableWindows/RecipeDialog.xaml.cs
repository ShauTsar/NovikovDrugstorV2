using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
    /// Interaction logic for RecipeDialog.xaml
    /// </summary>
    public partial class RecipeDialog : Window
    {
        public int SelectedDoctorId { get; private set; }
        public int SelectedPatientId { get; private set; }
        public int SelectedPharmacistId { get; private set; }
        public int SelectedDrugId { get; private set; }
        public string Diagnosis { get; private set; }

        public RecipeDialog(bool onlyDoctors = false, bool onlyPatients = false, bool onlyPharma = false)
        {
            InitializeComponent();
            if(onlyDoctors == true)
            {
                LoadDoctors();
            }
            else if(onlyPatients == true)
            {
                LoadPatients();
            }
            else if(onlyPharma == true)
            {
                LoadPharmacists();
            }
            else
            {
                LoadDoctors();
                
                LoadPatients();
               
                LoadPharmacists();
                
                LoadDrugs();
            }
        }

        private void LoadDoctors()
        {
            DoctorsList.Visibility = Visibility.Visible;
            DoctorsTB.Visibility = Visibility.Visible;
            string query = "SELECT ID, Surname FROM dbo.Doctors ORDER BY Surname";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            DoctorsList.ItemsSource = table.DefaultView;
            DoctorsList.DisplayMemberPath = "Surname";
            DoctorsList.SelectedValuePath = "ID";
        }

        private void LoadPatients()
        {
            PatientsList.Visibility = Visibility.Visible;
            PatientsTB.Visibility = Visibility.Visible;
            string query = "SELECT ID, Surname FROM dbo.Patients ORDER BY Surname";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            PatientsList.ItemsSource = table.DefaultView;
            PatientsList.DisplayMemberPath = "Surname";
            PatientsList.SelectedValuePath = "ID";
        }

        private void LoadPharmacists()
        {
            PharmacistsList.Visibility = Visibility.Visible;
            PharmaTB.Visibility = Visibility.Visible;
            string query = "SELECT ID, Surname FROM dbo.Pharmacists ORDER BY Surname";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            PharmacistsList.ItemsSource = table.DefaultView;
            PharmacistsList.DisplayMemberPath = "Surname";
            PharmacistsList.SelectedValuePath = "ID";
        }

        private void LoadDrugs()
        {
            DrugsList.Visibility = Visibility.Visible;
            DrugsTB.Visibility = Visibility.Visible;
            string query = "SELECT ID, Name FROM dbo.Drugs ORDER BY Name";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            DrugsList.ItemsSource = table.DefaultView;
            DrugsList.DisplayMemberPath = "Name";
            DrugsList.SelectedValuePath = "ID";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedDoctorRow = (DataRowView)DoctorsList.SelectedItem;
            DataRowView selectedPatientRow = (DataRowView)PatientsList.SelectedItem;
            DataRowView selectedPharmacistRow = (DataRowView)PharmacistsList.SelectedItem;
            DataRowView selectedDrugRow = (DataRowView)DrugsList.SelectedItem;
            if (DoctorsList.IsVisible == true)
            {
                SelectedDoctorId = (int)DoctorsList.SelectedValue;
            }
            if (PatientsList.IsVisible == true)
            {
                SelectedPatientId = (int)PatientsList.SelectedValue;
            }
            if (PharmacistsList.IsVisible == true)
            {
                SelectedPharmacistId = (int)PharmacistsList.SelectedValue;
            }
            if (DrugsList.IsVisible == true)
            {
                SelectedDrugId = (int)DrugsList.SelectedValue;
            }
                     
            DialogResult = true;
            Close();

            //if (selectedDoctorRow == null && selectedPatientRow == null && selectedPharmacistRow == null && selectedDrugRow == null)
            //{
            //    MessageBox.Show("Please select a doctor, patient, pharmacist, and drug.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

          
}
