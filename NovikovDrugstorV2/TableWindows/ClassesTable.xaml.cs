using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class ClassesTable : Window
    {
        private SqlConnection _connection;
        private SqlDataAdapter _dataAdapter;
        private DataTable _table;
        private string tableName;
        public ClassesTable(string table)
        {
            InitializeComponent();
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string query = "";

            switch (table)
            {
                case "Doctors":
                    query = "SELECT dbo.Doctors.ID, dbo.Doctors.Surname, dbo.Doctors.Name, dbo.Doctors.MiddleName, dbo.Hospitals.Name AS hospitalid FROM dbo.Doctors JOIN dbo.Hospitals ON dbo.Doctors.HospitalID = dbo.Hospitals.ID";
                    break;
                case "TechnicalManual":
                    query = "SELECT dbo.TechnicalManual.ID, dbo.Drugs.Name as Лекарство, MethodOfPreparation, TimeOfPreparation FROM dbo.TechnicalManual JOIN dbo.Drugs ON dbo.TechnicalManual.DrugID = dbo.Drugs.ID";
                    break;
                case "Drugs":
                    query = "SELECT dbo.Drugs.ID, dbo.Classes.NameOfClass as Вид, dbo.Drugs.Name, dbo.Drugs.Quantity, dbo.Drugs.Price FROM dbo.Drugs JOIN dbo.Classes ON dbo.Classes.ID = dbo.Drugs.ClassID";
                    break;
                case "Storage":
                    query = "SELECT dbo.Storage.ID, dbo.Drugs.Name as Лекарство, Quanity, CriticalRate, LessThenNormal FROM dbo.Storage JOIN dbo.Drugs ON dbo.Drugs.ID = dbo.Storage.DrugID";
                    break;
                case "Recipes":
                    query = "SELECT dbo.Recipes.ID, dbo.Doctors.Surname as Доктор, dbo.Patients.Surname as Пациент, dbo.Pharmacists.Surname as Фармацевт, Diagnosis, dbo.Drugs.Name as Лекарство FROM dbo.Recipes JOIN dbo.Doctors ON dbo.Doctors.ID = dbo.Recipes.DoctorID JOIN dbo.Patients ON " +
                        "dbo.Patients.ID = dbo.Recipes.PatientID " +
                        "JOIN dbo.Pharmacists ON dbo.Pharmacists.ID = dbo.Recipes.PharmacistID " +
                        "JOIN dbo.Drugs ON dbo.Drugs.ID = dbo.Recipes.DrugID";
                    break;
                case "ComponentToCreate":
                    query = "SELECT dbo.ComponentToCreate.ID, dbo.Components.NameOfComponent as Компонент, dbo.ComponentToCreate.TechnicalID, DateToUse As [Дата изготовления] FROM dbo.ComponentToCreate JOIN dbo.Components ON dbo.Components.ID = dbo.ComponentToCreate.ComponentID";
                    break;
                default:
                    query = $"SELECT * FROM dbo.{table}";
                    break;
            }

            _dataAdapter = new SqlDataAdapter(query, _connection);

            LoadTable(table);
            tableName = table;
            
        }
        private void LoadTable(string tableName)
        {
            _table = new DataTable();
            _dataAdapter.Fill(_table);
            DataGrid.ItemsSource = _table.DefaultView;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            switch (tableName)
            {
                case "Classes":
                    try
                    {
                        string query = $"INSERT INTO dbo.{tableName} (NameOfClass, MethodOfUse) VALUES ('New Class', 'New Method of Use')";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Doctors":
                    try
                    {
                        var dialog = new HospitalSelectionDialog();
                        int selectedHospitalId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            selectedHospitalId = dialog.SelectedHospitalId;
                        }
                        else
                        {
                            return;
                        }


                        string query = $"INSERT INTO dbo.{tableName} (Surname, Name, MiddleName, HospitalID) VALUES ('Фамилия', 'Имя', 'Отчество', {selectedHospitalId})";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Drugs":
                    try
                    {
                        var dialog = new DrugDialog();
                        int selectedClassId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            selectedClassId = dialog.SelectedClassId;
                        }
                        else
                        {
                            return;
                        }
                        string query = $"INSERT INTO dbo.{tableName} (Name, Quantity, ClassID, Price) VALUES ('Введите Имя', 0, {selectedClassId}, 0)";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Hospitals":
                    try
                    {
                        string query = $"INSERT INTO dbo.{tableName} (Name, Address) VALUES ('Название больницы', 'Адресс')";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "OrdersForIssue":
                    try
                    {
                        var dialog = new ReleaseDialog();
                        int receiptId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            receiptId = dialog.SelectedReceipt;
                        }
                        else
                        {
                            return;
                        }
                        string query = $"INSERT INTO dbo.{tableName} (RecipeID, DateToIssue) VALUES ({receiptId}, {date})";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Patients":
                    try
                    {
                        string query = $"INSERT INTO dbo.{tableName} (Surname , Name, MiddleName ,Age,PhoneNumber, Address) VALUES ('Фамилия', 'Имя', 'Отчество', 0, 'Номер Телефона', 'Адрес')";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Pharmacists":
                    try
                    {
                        string query = $"INSERT INTO dbo.{tableName} (Surname, Name, MiddleName) VALUES ('Фамилия', 'Имя', 'Отчество')";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "Recipes":
                    try
                    {
                        var dialog = new RecipeDialog();
                        int doctorId = 0;
                        int patientId = 0;
                        int pharmacistId = 0;
                        int drugId = 0;
                        string diagnosis = "";

                        if (dialog.ShowDialog() == true)
                        {
                            doctorId = dialog.SelectedDoctorId;
                            patientId = dialog.SelectedPatientId;
                            pharmacistId = dialog.SelectedPharmacistId;
                            drugId = dialog.SelectedDrugId;
                            diagnosis = dialog.Diagnosis;
                        }
                        else
                        {
                            return;
                        }

                        string query = $"INSERT INTO dbo.{tableName} (DoctorID, PatientID, PharmacistID, Diagnosis, DrugID) VALUES ({doctorId}, {patientId}, {pharmacistId}, '', {drugId})";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;

                case "Release":
                    try
                    {
                        var dialog = new ReleaseDialog();
                        int receiptId = 0;
                        
                        int orderId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            receiptId = dialog.SelectedReceipt;
                            orderId = dialog.SelectedOrderId;
                        }
                        else
                        {
                            return;
                        }

                        string query = $"INSERT INTO dbo.{tableName} (ReciptID, Date, OrderID) VALUES ({receiptId}, '{date}', {orderId})";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;

                case "Storage":
                    try
                    {
                        var dialog = new TechnicalManualDialog();
                        int drugId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            drugId = dialog.SelectedDrugId;

                        }
                        else
                        {
                            return;
                        }

                        string query = $"INSERT INTO dbo.{tableName} (DrugID, Quanity,CriticalRate, LessThenNormal) VALUES ({drugId},2,0, CONVERT(bit, 'false'))";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "TechnicalManual":
                    try
                    {
                        var dialog = new TechnicalManualDialog();
                        int drugId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            drugId = dialog.SelectedDrugId;

                        }
                        else
                        {
                            return;
                        }

                        string query = $"INSERT INTO dbo.{tableName} (DrugID, MethodOfPreparation, TimeOfPreparation) VALUES ({drugId}, '', 6)";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;
                case "ComponentToCreate":
                    try
                    {
                        var dialog = new ComponentsDialog();
                        int componentId = 0;

                        if (dialog.ShowDialog() == true)
                        {
                            componentId = dialog.SelectedComponentId;

                        }
                        else
                        {
                            return;
                        }

                        string query = $"INSERT INTO dbo.{tableName} (ComponentID, TechnicalId, DateToUse) VALUES ({componentId}, 1, null)";
                        SqlCommand cmd = new SqlCommand(query, _connection);
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        LoadTable(tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _connection.Close();
                    }
                    break;

            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedRow = (DataRowView)DataGrid.SelectedItem;

                if (selectedRow != null)
                {
                    int id = (int)selectedRow.Row["ID"];
                    string query = $"DELETE FROM dbo.{tableName} WHERE ID = {id}";
                    SqlCommand cmd = new SqlCommand(query, _connection);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                    LoadTable(tableName);
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataRowView selectedRow = e.Row.Item as DataRowView;
            if (e.Column.Header.ToString() == "hospitalid")
            {
                var dialog = new HospitalSelectionDialog();
                int selectedHospitalId = 0;

                if (dialog.ShowDialog() == true)
                {
                    selectedHospitalId = dialog.SelectedHospitalId;
                    selectedRow.Row[e.Column.Header.ToString()] = selectedHospitalId;
                }
                else
                {
                    e.Cancel = true;
                }
            }

            else if (e.Column.Header.ToString() == "Лекарство")
            {
                var dialog = new TechnicalManualDialog();
                int selectedDrugID = 0;
                if (dialog.ShowDialog() == true)
                {
                    selectedDrugID = dialog.SelectedDrugId;
                    selectedRow[e.Column.Header.ToString()] = selectedDrugID;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "Вид")
            {
                var dialog = new DrugDialog();
                int selectedClassId = 0;

                if (dialog.ShowDialog() == true)
                {
                    selectedClassId = dialog.SelectedClassId;
                    selectedRow[e.Column.Header.ToString()] = selectedClassId;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "Компонент")
            {
                var dialog = new ComponentsDialog(components:true);
                int selectedClassId = 0;

                if (dialog.ShowDialog() == true)
                {
                    selectedClassId = dialog.SelectedComponentId;
                    selectedRow[e.Column.Header.ToString()] = selectedClassId;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "TechnicalID")
            {
                var dialog = new ComponentsDialog(tech:true);
                int selectedClassId = 0;

                if (dialog.ShowDialog() == true)
                {
                    selectedClassId = dialog.SelectedTechnicalId;
                    selectedRow[e.Column.Header.ToString()] = selectedClassId;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "RecipeId" || e.Column.Header.ToString() == "ReciptId")
            {
                var dialog = new ReleaseDialog(onlyRecipes:true);
                int receiptId = 0;

                if (dialog.ShowDialog() == true)
                {
                    receiptId = dialog.SelectedReceipt;
                    selectedRow[e.Column.Header.ToString()] = receiptId;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "OrderId")
            {
                var dialog = new ReleaseDialog(onlyOrders:true);
                int orderId = 0;

                if (dialog.ShowDialog() == true)
                {
                    orderId = dialog.SelectedOrderId;
                    selectedRow[e.Column.Header.ToString()] = orderId;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.Column.Header.ToString() == "Фармацевт")
            {
                var dialog = new RecipeDialog(onlyPharma: true);
                int pharmacistId = 0;

                if (dialog.ShowDialog() == true)
                {
                    pharmacistId = dialog.SelectedPharmacistId;
                    selectedRow[e.Column.Header.ToString()] = pharmacistId;
                }
                else
                {
                    return;
                }
            }
            else if (e.Column.Header.ToString() == "Доктор")
            {
                var dialog = new RecipeDialog(onlyDoctors: true);
                int doctorId = 0;

                if (dialog.ShowDialog() == true)
                {
                    doctorId = dialog.SelectedDoctorId;
                    selectedRow[e.Column.Header.ToString()] = doctorId;
                }
                else
                {
                    return;
                }
            }
            else if (e.Column.Header.ToString() == "Пациент")
            {
                var dialog = new RecipeDialog(onlyPatients: true);
                int pharmacistId = 0;

                if (dialog.ShowDialog() == true)
                {
                    pharmacistId = dialog.SelectedPatientId;
                    selectedRow[e.Column.Header.ToString()] = pharmacistId;
                }
                else
                {
                    return;
                }
            }


        }



        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                DataRowView selectedRow = e.Row.Item as DataRowView;
                if (selectedRow != null)
                {
                    int id = (int)selectedRow.Row["ID"];
                    string columnName = e.Column.SortMemberPath; 
                    string newValue = (e.EditingElement as TextBox).Text; 
                    switch (columnName)
                    {
                        case "Вид":
                            columnName = "ClassId";
                            break;
                        case "Лекарство":
                            columnName = "DrugId";
                            break;
                        case "Компонент":
                            columnName = "ComponentID";
                            break;
                        case "Фармацевт":
                            columnName = "PharmacistID";
                            break;
                        case "Доктор":
                            columnName = "DoctorID";
                            break;
                        case "Пациент":
                            columnName = "PatientID";
                            break;
                    }

                    string query = $"UPDATE dbo.{tableName} SET {columnName} = '{newValue}' WHERE ID = {id}";
                    using (SqlCommand cmd = new SqlCommand(query, _connection))
                    {
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch 
            {
                
            }
            finally
            {
                _connection.Close();
                LoadTable(tableName);
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTable(tableName);
        }

    }
}

