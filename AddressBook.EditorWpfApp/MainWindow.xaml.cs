using AddressBook.CommonLibrary;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private EmployeeList _employees = [];
        public Employee? SelectedEmployee { get; set; }

        private bool _wasChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            EmployeeListView.ItemsSource = _employees;

            UpdateEmployeeCount();

            //SetButtons();

            //bool isItemSelected = EmployeeListView.SelectedItem != null;
            //EditButton.IsEnabled = isItemSelected;
            //DeleteButton.IsEnabled = isItemSelected;
            //EditButtonMain.IsEnabled = isItemSelected;
            //DeleteButtonMain.IsEnabled = isItemSelected;
        }

        private void UpdateEmployeeCount()
        {
            EmployeeCountLabel.Content = _employees.Count;
            SearchButton.IsEnabled = (EmployeeListView.Items.Count > 0);
            SetButtons();
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
            var addEmployeeDialog = new AddEmployeeWindow();
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
            SelectedEmployee = EmployeeListView.SelectedItem as Employee;
            EmployeeListView.SelectedItem = null;


            if (SelectedEmployee != null)
            {
                var CopyEmployee = new Employee(SelectedEmployee.Name, SelectedEmployee.Position, SelectedEmployee.Email);
                CopyEmployee.UpdateFrom(SelectedEmployee);

                var editEmployeeDialog = new EditEmployeeWindow(CopyEmployee);

                if (editEmployeeDialog.ShowDialog() == true)
                {
                    SelectedEmployee.UpdateFrom(CopyEmployee);
                    EmployeeListView.Items.Refresh();
                    _wasChanged = true;
                }
            }
            SetButtons();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            SelectedEmployee = EmployeeListView.SelectedItem as Employee;
            EmployeeListView.SelectedItem = null;

            if (SelectedEmployee != null)
            {
                var result = MessageBox.Show($"Chcete odstrániť vybraného zamestnanca?", "Odstrániť zamestnanca", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    _employees.Remove(SelectedEmployee);
                    UpdateEmployeeCount();
                    EmployeeListView.Items.Refresh();
                    _wasChanged = true;
                }
            }
            SetButtons();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow(_employees);
            searchWindow.ShowDialog();
        }

        private void EmployeeListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeListView.SelectedItem != null)
            {
                SetButtons();
            }
        }

        private void SetButtons()
        {
            bool isItemSelected = EmployeeListView.SelectedItem != null;
            EditButton.IsEnabled = isItemSelected;
            DeleteButton.IsEnabled = isItemSelected;
            EditButtonMain.IsEnabled = isItemSelected;
            DeleteButtonMain.IsEnabled = isItemSelected;
            SearchButton.IsEnabled = (EmployeeListView.Items.Count > 0);
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Adresár zamestnancov Ultra Enterprise Pro Ultimate Edition\n\n© 1999\nAutor: Michaela Kantorová", "O programe", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}