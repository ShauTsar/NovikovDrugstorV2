using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace NovikovDrugstorV2.TableWindows
{
    /// <summary>
    /// Interaction logic for ComponentsDialog.xaml
    /// </summary>
    public partial class ComponentsDialog : Window
    {
        public int SelectedComponentId { get; set; }
        public string ComponentName { get; set; }
        public int SelectedTechnicalId { get; set; }

        public ComponentsDialog(bool components = false, bool tech = false)
        {
            InitializeComponent();
            LoadData(components, tech);
        }

        private void LoadData(bool components = false, bool tech = false)
        {
            try
            {
                
               
                if(components)
                {
                    string query = "SELECT ID, NameOfComponent FROM dbo.Components";
                    DataTable table = new DataTable();
                    TechnicalList.Visibility = Visibility.Hidden;
                    TechTB.Visibility = Visibility.Hidden;

                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    using (var command = new SqlCommand(query, connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);

                    }
                    ComponentsList.ItemsSource = table.DefaultView;
                    ComponentsList.DisplayMemberPath = "NameOfComponent";
                    ComponentsList.SelectedValuePath = "ID";
                }          
                else if (tech)
                {
                    string query2 = "SELECT ID FROM dbo.TechnicalManual";

                    DataTable table2 = new DataTable();
                    ComponentsList.Visibility = Visibility.Hidden;
                    ComponentsTB.Visibility = Visibility.Hidden;
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    using (var command2 = new SqlCommand(query2, connection))
                    using (var adapter2 = new SqlDataAdapter(command2))
                    {
                        adapter2.Fill(table2);

                    }
                    TechnicalList.ItemsSource = table2.DefaultView;
                    TechnicalList.DisplayMemberPath = "ID";
                    TechnicalList.SelectedValuePath = "ID";
                }
                else
                {
                    string query = "SELECT ID, NameOfComponent FROM dbo.Components";
                    string query2 = "SELECT ID FROM dbo.TechnicalManual";
                    DataTable table = new DataTable();
                    DataTable table2 = new DataTable();
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    using (var command = new SqlCommand(query, connection))
                    using (var command2 = new SqlCommand(query2, connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);

                    }

                    ComponentsList.ItemsSource = table.DefaultView;
                    ComponentsList.DisplayMemberPath = "NameOfComponent";
                    ComponentsList.SelectedValuePath = "ID";
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    using (var command2 = new SqlCommand(query2, connection))
                    using (var adapter2 = new SqlDataAdapter(command2))
                    {
                        adapter2.Fill(table2);

                    }
                    TechnicalList.ItemsSource = table.DefaultView;
                    TechnicalList.DisplayMemberPath = "ID";
                    TechnicalList.SelectedValuePath = "ID";
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(ComponentsList.IsVisible)
            {
                SelectedComponentId = (int)ComponentsList.SelectedValue;
                
            }
           if(TechnicalList.IsVisible)
            {               
                SelectedTechnicalId = (int)TechnicalList.SelectedValue;

            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
