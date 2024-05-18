using AddressBook.CommonLibrary;
using System.Windows;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public Employee Employee { get; private set; }

        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            Employee = employee;
            this.DataContext = Employee;
        }
        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
