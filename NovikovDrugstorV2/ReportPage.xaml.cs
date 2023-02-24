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

    public partial class ReportPage : Window
    {
        public List<Drug> drugs = new List<Drug>();
        private SqlConnection _connection;
        public ReportPage()
        {
            InitializeComponent();
            _connection = new SqlConnection(@"Data Source=ES371\MSSQLSERVER01;Initial Catalog=NovikovDrugstore;Integrated Security=True");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drugs.Clear();      
            switch (ReportsCBX.SelectedIndex)
            {
                case 0:
                    using (_connection) 
                    {
                        _connection.Open();

                        SqlCommand command = new SqlCommand("GetDrugsForOrdersInProduction", _connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Drug drug = new Drug();
                            drug.ID = (int)reader["ID"];
                            drug.Name = (string)reader["Name"];
                            drug.Quantity = (int)reader["TotalQuantity"];
                            drugs.Add(drug);
                        }
                        reader.Close();

                        StringBuilder sb = new StringBuilder();
                        sb.Append("Название Лекарства\tКоличество\n");

                        foreach (var drug in drugs)
                        {
                            sb.Append($"{drug.Name}\t{drug.Quantity}\n");
                        }
                        int totalQuantity = drugs.Sum(d => d.Quantity);
                        ResultsWindow resultsWindow = new ResultsWindow(drugs, ReportType.DrugsForOrdersInProduction, totalQuantity) ;
                        resultsWindow.Show();


                    }
                    break;
                case 1:
                    DrugsWindow drugsWindow = new DrugsWindow();
                    string drugType = null;
                    string drugName = null;
                    bool allMedicine;

                    if (drugsWindow.ShowDialog() == true)
                    {
                        drugType = drugsWindow.MedicineType;
                        drugName = drugsWindow.MedicineName;
                        allMedicine = drugsWindow.AllMedicine;
                        using (SqlCommand command = new SqlCommand("GetTechnicalManualForDrugs", _connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            if (drugType != "" && drugName != "")
                            {
                                command.Parameters.AddWithValue("@drug", drugName);
                                command.Parameters.AddWithValue("@class", drugType);
                            }
                            if(allMedicine == true)
                            {
                                command.Parameters.AddWithValue("@all", allMedicine);
                            }
                            _connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            StringBuilder sb = new StringBuilder();
                            sb.Append("Drug Name\tDescription\n");

                            while (reader.Read())
                            {
                                Drug drug = new Drug();
                                string name = (string)reader["Лекарство"];
                                string description = (string)reader["MethodOfPreparation"];
                                drug.Name = name;
                                drug.Description = description;
                                sb.Append($"{name}\t{description}\n");
                                drugs.Add(drug);
                            }

                            // Close reader and connection
                            reader.Close();
                            _connection.Close();

                            ResultsWindow resultsWindow = new ResultsWindow(drugs, ReportType.GetTechnicalManualForDrugs);
                            resultsWindow.Show();
                        }
                    }

                    break;



            }

        }
    }
    public enum ReportType
    {
        DrugsForOrdersInProduction,
        GetTechnicalManualForDrugs
    }



    public class Drug
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
