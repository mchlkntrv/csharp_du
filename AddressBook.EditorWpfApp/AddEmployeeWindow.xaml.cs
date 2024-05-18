using AddressBook.CommonLibrary;
using System.Windows;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public Employee Employee { get; private set; }

        public AddEmployeeWindow()
        {
            InitializeComponent();
            Employee = new(string.Empty, string.Empty, string.Empty);
            DataContext = Employee;
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
