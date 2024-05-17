using AddressBook.CommonLibrary;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeList _employees = [];
        private bool _wasChanged;
        public MainWindow()
        {
            InitializeComponent();
            EmployeeListView.ItemsSource = _employees;
            UpdateEmployeeCount();
            bool isItemSelected = EmployeeListView.SelectedItem != null;
            EditButton.IsEnabled = isItemSelected;
            DeleteButton.IsEnabled = isItemSelected;
            EditButtonMain.IsEnabled = isItemSelected;
            DeleteButtonMain.IsEnabled = isItemSelected;
        }

        private void UpdateEmployeeCount()
        {
            EmployeeCountLabel.Content = _employees.Count;
        }

        private void NewFileClick(object sender, RoutedEventArgs e)
        {
            if (_wasChanged)
            {
                var result = MessageBox.Show("Chcete uložiť zmeny?", "Uložiť zmeny", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                if (result == MessageBoxResult.Yes)
                {
                    SaveAsFileClick(sender, e);
                }
            }
            _employees.Clear();
            EmployeeListView.ItemsSource = _employees;
            UpdateEmployeeCount();
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var jsonFile = new FileInfo(openFileDialog.FileName);
                var loadedList = EmployeeList.LoadFromJson(jsonFile);
                if (loadedList != null)
                {
                    _employees = loadedList;
                    EmployeeListView.ItemsSource = _employees;
                    UpdateEmployeeCount();
                }
                else
                {
                    MessageBox.Show("Chyba pri načítaní súboru.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveAsFileClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var jsonFile = new FileInfo(saveFileDialog.FileName);
                _employees.SaveToJson(jsonFile);
                _wasChanged = false;
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var addEmployeeDialog = new EmployeeWindow();
            if (addEmployeeDialog.ShowDialog() == true)
            {
                var newEmployee = addEmployeeDialog.Employee;
                _employees.Add(newEmployee);
                UpdateEmployeeCount();
                _wasChanged = true;
            }
            EmployeeListView.Items.Refresh();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem is Employee selectedEmployee)
            {
                var editEmployeeDialog = new EmployeeWindow(selectedEmployee);
                if (editEmployeeDialog.ShowDialog() == true)
                {
                    selectedEmployee.Name = editEmployeeDialog.Employee.Name;
                    selectedEmployee.Position = editEmployeeDialog.Employee.Position;
                    selectedEmployee.Phone = editEmployeeDialog.Employee.Phone;
                    selectedEmployee.Email = editEmployeeDialog.Employee.Email;
                    selectedEmployee.Room = editEmployeeDialog.Employee.Room;
                    selectedEmployee.MainWorkplace = editEmployeeDialog.Employee.MainWorkplace;
                    selectedEmployee.Workplace = editEmployeeDialog.Employee.Workplace;

                    EmployeeListView.SelectedItem = null;
                    UpdateEmployeeCount();
                    _wasChanged = true;

                }
            }
            EmployeeListView.Items.Refresh();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem is Employee selectedEmployee)
            {
                var result = MessageBox.Show($"Chcete odstrániť vybraného zamestnanca?", "Odstrániť zamestnanca", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _employees.Remove(selectedEmployee);
                    UpdateEmployeeCount();
                    EmployeeListView.Items.Refresh();
                    _wasChanged = true;

                }
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow(_employees);
            searchWindow.ShowDialog();
        }

        private void EmployeeListViewSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool isItemSelected = EmployeeListView.SelectedItem != null;
            EditButton.IsEnabled = isItemSelected;
            DeleteButton.IsEnabled = isItemSelected;
            EditButtonMain.IsEnabled = isItemSelected;
            DeleteButtonMain.IsEnabled = isItemSelected;
        }
    }
}