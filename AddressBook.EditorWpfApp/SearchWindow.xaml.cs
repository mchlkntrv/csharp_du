using AddressBook.CommonLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
