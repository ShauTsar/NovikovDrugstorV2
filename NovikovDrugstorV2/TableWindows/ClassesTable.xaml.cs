using System;
using System.Collections.Generic;
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
            _connection = new SqlConnection(@"Data Source=ES371\MSSQLSERVER01;Initial Catalog=NovikovDrugstore;Integrated Security=True");
            _dataAdapter = new SqlDataAdapter($"SELECT * FROM dbo.{table}", _connection);
            if(table == "Doctors")
            {
                _dataAdapter = new SqlDataAdapter($"SELECT dbo.Doctors.ID, dbo.Doctors.Surname, dbo.Doctors.Name, dbo.Doctors.MiddleName, dbo.Hospitals.Name AS hospitalid FROM dbo.Doctors JOIN dbo.Hospitals ON dbo.Doctors.HospitalID = dbo.Hospitals.ID", _connection);
            }
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
                        string query = $"INSERT INTO dbo.{tableName} (Name, Quantity) VALUES ('Введите Имя', 'Введите количество', 'Вид лекарства')";
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
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "OrdersForIssue":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "Patients":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "Pharmacists":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "Recipes":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "Release":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "Storage":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

                    }
                    break;
                case "TechnicalManual":
                    try
                    {
                        DataRow newRow = _table.NewRow();
                        newRow["ID"] = _table.AsEnumerable().Max(row => row.Field<int>("ID")) + 1;
                        newRow["NameOfClass"] = "New Class";
                        newRow["MethodOfUse"] = "New Method of Use";
                        _table.Rows.Add(newRow);
                        using (SqlCommandBuilder builder = new SqlCommandBuilder(_dataAdapter))
                        {
                            _dataAdapter.Update(_table);
                        }
                        DataGrid.Items.Refresh();
                    }
                    catch
                    {

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
                    selectedRow.Row["hospitalid"] = selectedHospitalId;
                }
                else
                {
                    e.Cancel = true;
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

                    string query = $"UPDATE dbo.{tableName} SET {columnName} = '{newValue}' WHERE ID = {id}";
                    using (SqlCommand cmd = new SqlCommand(query, _connection))
                    {
                        _connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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

