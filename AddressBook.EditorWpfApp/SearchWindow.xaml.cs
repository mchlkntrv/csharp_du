using AddressBook.CommonLibrary;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly EmployeeList? _employeeList;
        public SearchWindow(EmployeeList employees)
        {
            _employeeList = employees;
            InitializeComponent();
            PositionComboBox.ItemsSource = _employeeList.GetPositions();
            WorkplaceComboBox.ItemsSource = _employeeList.GetMainWorkplaces();
        }
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            if (_employeeList != null)
            {
                var results = _employeeList.Search(
                    mainWorkplace: WorkplaceComboBox.SelectedItem?.ToString(),
                    position: PositionComboBox.SelectedItem?.ToString(),
                    name: NameTextBox.Text);

                EmployeesListBox.ItemsSource = results.Employees;
                EmployeeCountLabel.Content = results.Employees.Length.ToString();
            }
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            PositionComboBox.SelectedIndex = -1;
            WorkplaceComboBox.SelectedIndex = -1;
            NameTextBox.Clear();
            EmployeesListBox.ItemsSource = null;
            EmployeeCountLabel.Content = "0";
        }

        private void ExportClick(object sender, RoutedEventArgs e)
        {
            if (_employeeList != null)
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    var results = _employeeList.Search(
                        mainWorkplace: WorkplaceComboBox.SelectedItem?.ToString(),
                        position: PositionComboBox.SelectedItem?.ToString(),
                        name: NameTextBox.Text);

                    var fileInfo = new FileInfo(saveFileDialog.FileName);
                    results.SaveToCsv(fileInfo, ";");
                }
            }
        }
    }
}
