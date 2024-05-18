using AddressBook.CommonLibrary;

class Program
{
    /// <summary>
    /// Search emp
    /// </summary>
    /// <param name="input">Input file</param>
    /// <param name="name">Employee name</param>
    /// <param name="position">Employee position</param>
    /// <param name="mainWorkplace">Employee main workplace</param>
    /// <param name="output">Output file</param>
    static void Main(string input, string? name = null, string? position = null, string? mainWorkplace = null, string? output = null)
    {
        try
        {
            EmployeeList? employees = EmployeeList.LoadFromJson(new FileInfo(input));

            if (employees == null)
            {
                Console.Error.WriteLine("No employees found.");
                return;
            }

            SearchResult searchResult = employees.Search(mainWorkplace, position, name);

            foreach (Employee employee in searchResult.Employees)
            {
                Console.WriteLine($"[{searchResult.Employees.ToList().IndexOf(employee) + 1}] {employee.Name}");
                Console.WriteLine($"Pracovisko: {employee.Workplace}");
                Console.WriteLine($"Miestnosť: {employee.Room}");
                Console.WriteLine($"Telefón: {employee.Phone}");
                Console.WriteLine($"E-mail: {employee.Email}");
                Console.WriteLine($"Funkcia: {employee.Position}");
                Console.WriteLine();
            }

            if (output != null)
            {
                searchResult.SaveToCsv(new FileInfo(output), ";");
            }
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine("File not found");
            return;
        }
    }
}
