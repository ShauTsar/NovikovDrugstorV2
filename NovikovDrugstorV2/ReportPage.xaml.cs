using Azure.Core;
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

namespace NovikovDrugstorV2
{

    public partial class ReportPage : Window
    {
        public DrugsWindow drugsWindow;
        private SqlConnection _connection;
        public ResultsWindow resultsWindow;
        public ReportPage()
        {
            InitializeComponent();
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (ReportsCBX.SelectedIndex)
            {
                case 0:
                    ShowResult(ReportType.DrugsForOrdersInProduction);
                    
                    break;
                case 1:
                    ShowResult(ReportType.GetTechnicalManualForDrugs);                               
                    break;
                case 2:
                    ShowResult(ReportType.DrugsPrices);
                        break;
                case 3:
                    ShowResult(ReportType.BestDrugs);
                    break;
                case 4:
                    ShowResult(ReportType.PatientsNotRelease);
                    break;
                case 5:
                    ShowResult(ReportType.PatientsWait);
                    break;
                case 6:
                    ShowResult(ReportType.TopDrugs);
                    break;
                case 7:
                    ShowResult(ReportType.ComponentsForDate);
                    break;
                case 8:
                    ShowResult(ReportType.DrugsAndPatients);
                    break;
                case 9:
                ShowResult(ReportType.ZeroDrugs);
                    break;
                case 10:
                ShowResult(ReportType.CriticalDrugs);
                    break;  
                case 11:
                ShowResult(ReportType.AllOrders);
                    break;






            }

        }
        private void ShowResult(ReportType reportType)
        {
            resultsWindow = new ResultsWindow(reportType: reportType, _connection: _connection);
            resultsWindow.Show();
        }
    }
    public enum ReportType
    {
        DrugsForOrdersInProduction,
        GetTechnicalManualForDrugs,
        DrugsPrices,
        BestDrugs,
        PatientsNotRelease,
        PatientsWait,
        TopDrugs,
        ZeroDrugs,
        CriticalDrugs,
        AllOrders,
        ComponentsForDate, 
        DrugsAndPatients
    }



   
}
