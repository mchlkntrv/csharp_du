using System.Text;

namespace AddressBook.CommonLibrary
{
    public class SearchResult(IEnumerable<Employee> employees)
    {
        public Employee[] _employees = employees.ToArray();

        public Employee[] Employees
        {
            get => _employees;
        }
        public void SaveToCsv(FileInfo csvFile, string delimiter = "\t")
        {
            using StreamWriter writer = new(csvFile.FullName, false, Encoding.UTF8);
            writer.WriteLine("Name" + delimiter + "MainWorkplace" + delimiter + "Workplace" + delimiter + "Room" + delimiter + "Phone" + delimiter + "Email" + delimiter + "Position");
            foreach (Employee employee in _employees)
            {
                writer.WriteLine(employee.Name + delimiter + employee.MainWorkplace + delimiter + employee.Workplace + delimiter + employee.Room + delimiter + employee.Phone + delimiter + employee.Email + delimiter + employee.Position);
            }
        }
    }
}
