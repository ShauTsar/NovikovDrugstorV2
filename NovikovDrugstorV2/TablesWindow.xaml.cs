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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;


namespace NovikovDrugstorV2
{
    /// <summary>
    /// Interaction logic for TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
        
        public TablesWindow()
        {
            InitializeComponent();

        }

        private void TablesWindow_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnTbClasses_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Classes");
            classesTable.Show();
        }

        private void btnTbDoctors_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Doctors");
            classesTable.Show();
        }

        private void btnTbDrugs_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Drugs");
            classesTable.Show();
        }

        private void btnTbHospitals_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Hospitals");
            classesTable.Show();
        }

        private void btnTbOrdersForIssue_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("OrdersForIssue");
            classesTable.Show();
        }

        private void btnTbPatients_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Patients");
            classesTable.Show();
        }

        private void btnTbPharmacists_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Pharmacists");
            classesTable.Show();
        }

        private void btnTbRecipes_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Recipes");
            classesTable.Show();
        }

        private void btnTbRelease_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Release");
            classesTable.Show();
        }

        private void btnTbStorage_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("Storage");
            classesTable.Show();
        }

        private void btnTbTechnicalManual_Click(object sender, RoutedEventArgs e)
        {
            TableWindows.ClassesTable classesTable = new TableWindows.ClassesTable("TechnicalManual");
            classesTable.Show();
        }
    }
}
