using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;

namespace AddressBook.CommonLibrary
{
    public class EmployeeList : ObservableCollection<Employee>
    {
        public static EmployeeList? LoadFromJson(FileInfo jsonFile)
        {
            string json = File.ReadAllText(jsonFile.FullName);
            EmployeeList? employees = JsonConvert.DeserializeObject<EmployeeList>(json);
            return employees;
        }

        public void SaveToJson(FileInfo jsonFile)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(jsonFile.FullName, json);
        }

        public IEnumerable<string> GetPositions()
        {
            var positions = from employee in this select employee.Position;
            return positions.Where(position => position != null).Distinct().OrderBy(position => position);
        }

        public IEnumerable<string> GetMainWorkplaces()
        {
            var mainWorkplaces = from employee in this select employee.MainWorkplace;
            return mainWorkplaces.Where(mainWorkplace => mainWorkplace != null).Distinct().OrderBy(mainWorkplace => mainWorkplace);
        }
        public SearchResult Search(string? mainWorkplace = null, string? position = null, string? name = null)
        {
            IEnumerable<Employee> result = this;

            if (mainWorkplace != null)
            {
                result = result.Where(e => e.MainWorkplace == mainWorkplace);
            }

            if (position != null)
            {
                result = result.Where(e => e.Position == position);
            }

            if (name != null)
            {
                result = result.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return new SearchResult(result.ToList());
        }
    }
}
