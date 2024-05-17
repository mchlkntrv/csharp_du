using AddressBook.CommonLibrary;
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

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public Employee Employee { get; private set; }

        public EmployeeWindow()
        {
            InitializeComponent();
            Employee = new Employee
            {
                _name = string.Empty,
                _position = string.Empty,
                _email = string.Empty
            };
            DataContext = Employee;
        }

        public EmployeeWindow(Employee employee) : this()
        {
            NameTextBox.Text = employee.Name;
            PositionTextBox.Text = employee.Position;
            PhoneTextBox.Text = employee.Phone;
            EmailTextBox.Text = employee.Email;
            RoomTextBox.Text = employee.Room;
            MainWorkplaceTextBox.Text = employee.MainWorkplace;
            WorkplaceTextBox.Text = employee.Workplace;

            Employee = employee;
            DataContext = Employee;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            Employee.Name = NameTextBox.Text;
            Employee.Position = PositionTextBox.Text;
            Employee.Phone = PhoneTextBox.Text;
            Employee.Email = EmailTextBox.Text;
            Employee.Room = RoomTextBox.Text;
            Employee.MainWorkplace = MainWorkplaceTextBox.Text;
            Employee.Workplace = WorkplaceTextBox.Text;

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
