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

namespace NovikovDrugstorV2
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        private List<Drug> _drugs;
        public ReportType ReportType { get; set; }

        public ResultsWindow(List<Drug> drugs, ReportType reportType, int quantity = 0)
        {
            InitializeComponent();

            // Set the report type
            ReportType = reportType;

            // Set the data grid columns based on the report type
            if (ReportType == ReportType.DrugsForOrdersInProduction)
            {
                DrugGrid.Columns.Add(new DataGridTextColumn() { Header = "Название Лекарства", Binding = new Binding("Name") });
                DrugGrid.Columns.Add(new DataGridTextColumn() { Header = "Количество", Binding = new Binding("Quantity") });

            }
            else if (ReportType == ReportType.GetTechnicalManualForDrugs)
            {
                DrugGrid.Columns.Add(new DataGridTextColumn() { Header = "Название Лекарства", Binding = new Binding("Name") });
                DrugGrid.Columns.Add(new DataGridTextColumn() { Header = "Описание", Binding = new Binding("Description") });
            }
            if (quantity > 0)
            {
                lbQuantity.Visibility= Visibility.Visible;
                lbQuantity.Content += quantity.ToString();
            }

            DrugGrid.ItemsSource = drugs;
        }

    }

}
