using System;
using System.Collections.Generic;
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
    public partial class ResultsWindow : Window
    {
        public DrugsWindow drugsWindow;
        public ReportType ReportType { get; set; }

        public ResultsWindow(ReportType reportType, SqlConnection _connection = null)
        {
            InitializeComponent();
            ReportType = reportType;
            switch(ReportType)
            {
                case ReportType.DrugsForOrdersInProduction:
                    ResultShow("GetDrugsForOrdersInProduction", _connection);
                    break;
                case ReportType.GetTechnicalManualForDrugs:
                    ResultShow("GetTechnicalManualForDrugs",_connection, true);
                    break;
                case ReportType.DrugsPrices:
                    ResultShow("DrugsPrices", _connection, true);
                    break;
                case ReportType.BestDrugs:
                    ResultShow("BestDrugs", _connection, true);
                    break;
                case ReportType.PatientsNotRelease:
                    ResultShow("PatientsNotRelease", _connection);
                    break;
                case ReportType.PatientsWait:
                    ResultShow("PatientsWait", _connection, true);
                    break;
                case ReportType.TopDrugs:
                    ResultShow("TopDrugs", _connection, true);
                    break;
                case ReportType.ZeroDrugs:
                    ResultShow("ZeroDrugs", _connection);
                    break;
                case ReportType.CriticalDrugs:
                    ResultShow("CriticalDrugs", _connection, true);
                    break;
                case ReportType.AllOrders:
                    ResultShow("AllOrders", _connection);
                    break;
                case ReportType.ComponentsForDate:
                    ResultShow("ComponentsForDate", _connection,true);
                    break;
                case ReportType.DrugsAndPatients:
                    ResultShow("DrugsAndPatients", _connection, true);
                    break;
            }

            
             
         
        }
        private void ResultShow(string method, SqlConnection _connection, bool Category = false)
        {
            if(Category == true)
            {
                switch (method)
                {
                    case "ComponentsForDate":
                        drugsWindow = new DrugsWindow(date: true);
                        break;
                    case "DrugsAndPatients":
                        drugsWindow = new DrugsWindow(dateAndCategory: true);
                        break;
                    case "DrugsPrices":
                        drugsWindow = new DrugsWindow(onlyDrug:true);
                        break;
                    case "PatientsWait":
                        drugsWindow = new DrugsWindow(onlyClass:true);
                        break;
                    case "TopDrugs":
                        drugsWindow = new DrugsWindow(onlyClass: true);
                        break;
                    case "CriticalDrugs":
                        drugsWindow = new DrugsWindow(onlyClass: true);
                        break;
                    default:
                        drugsWindow = new DrugsWindow();
                        break;
                }
                if (drugsWindow.ShowDialog() == true)
                {
                    using (SqlCommand command = new SqlCommand(method, _connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (drugsWindow.MedicineName != "")
                        {
                            command.Parameters.AddWithValue("@drug", drugsWindow.MedicineName);
                        }
                        else if (drugsWindow.MedicineType != "")
                        {
                            command.Parameters.AddWithValue("@class", drugsWindow.MedicineType);
                        }
                        if (drugsWindow.DateFrom != null && drugsWindow.DateTo != null)
                        {
                            command.Parameters.AddWithValue("@dateFrom", drugsWindow.DateFrom);
                            command.Parameters.AddWithValue("@dateTo", drugsWindow.DateTo);
                        }
                        else if (drugsWindow.DateFrom != null)
                        {
                            command.Parameters.AddWithValue("@dateFrom", drugsWindow.DateFrom);
                            command.Parameters.AddWithValue("@dateTo", DateTime.Now);
                        }
                        else if (drugsWindow.DateFrom != null)
                        {
                            command.Parameters.AddWithValue("@dateFrom", DateTime.Now);
                            command.Parameters.AddWithValue("@dateTo", drugsWindow.DateTo);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DrugGrid.AutoGenerateColumns = true;
                            DrugGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
            }
            else
            {
                using (SqlCommand command = new SqlCommand(method, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DrugGrid.AutoGenerateColumns = true;
                        DrugGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
           
        }

    }

}
