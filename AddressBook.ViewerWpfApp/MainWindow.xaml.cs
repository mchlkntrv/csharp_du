using AddressBook.CommonLibrary;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AddressBook.ViewerWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeList? _employeeList;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);
                _employeeList = EmployeeList.LoadFromJson(fileInfo);
                if (_employeeList != null)
                {
                    PositionComboBox.ItemsSource = _employeeList.GetPositions();
                    WorkplaceComboBox.ItemsSource = _employeeList.GetMainWorkplaces();
                }
            }
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