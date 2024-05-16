//using AddressBook.CommonLibrary;
//class ProgramTest
//{
//    static void Main()
//    {
//        EmployeeList? loadedEmployees = EmployeeList.LoadFromJson(new FileInfo("employees.json"));

//        if (loadedEmployees != null)
//        {
//            foreach (string position in loadedEmployees.GetPositions())
//            {
//                Console.WriteLine(position);
//            }

//            foreach (string workplace in loadedEmployees.GetMainWorkplaces())
//            {
//                Console.WriteLine(workplace);
//            }

//            SearchResult searchResult = loadedEmployees.Search(mainWorkplace: "Fakulta riadenia a informatiky", name: "Michal");
//            foreach (Employee foundEmployee in searchResult.Employees)
//            {
//                Console.WriteLine(foundEmployee.Name);
//            }

//            searchResult.SaveToCsv(new FileInfo("searchResult.csv"));
//            loadedEmployees.SaveToJson(new FileInfo("employeesSaved.json"));
//        }
//    }
//}
