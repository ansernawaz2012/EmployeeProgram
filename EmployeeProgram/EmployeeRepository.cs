using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // OPTION 1
        /// <summary>
        /// Display list of employees taken from data source
        /// </summary>
        /// <param name="employeeList"></param>
        public List<Employee> ShowEmployees(List<Employee> employeeList)
        {
            //Re-load data from updated csv file
           // employeeList = LoadDataViaCsv(employeeList);

            Console.WriteLine("List of employees:");



            // List employees using Linq
            var listOfEmployees = employeeList
                .OrderBy(e => e.EmployeeId)
                .Select(e => e);



            foreach (var employee in listOfEmployees)
            {
                Console.WriteLine($"Name: {employee.FirstName} {employee.LastName} ");
                Console.WriteLine($"ID: {employee.EmployeeId}");
                Console.WriteLine($"DOB: {employee.DOB}");
                Console.WriteLine($"Start Date: {employee.StartDate}");
                Console.WriteLine($"Home Town: {employee.HomeTown}");
                Console.WriteLine($"Dept: {employee.Department}");
                Console.WriteLine("-------------------------------------");
            }


            return employeeList;

        }

        // OPTION 2
        /// <summary>
        /// Add new employee manually
        /// </summary>
        /// <param name="employeeList"></param>
        /// <returns></returns>
        public  List<Employee> AddEmployeeManually(List<Employee> employeeList)
        {
            Console.Write("Enter employee ID: ");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter date of birth: ");
            string stringDOB = Console.ReadLine();

            DateTime DOB = DateConvertor.StringToDateObject(stringDOB);

            Console.Write("Enter start date: ");
            string stringStartDate = Console.ReadLine();

            DateTime startDate = DateConvertor.StringToDateObject(stringStartDate);

            Console.Write("Enter home town: ");
            string homeTown = Console.ReadLine();
            Console.Write("Enter department: ");
            string department = Console.ReadLine();

            Employee newEmployee = new Employee(employeeId, firstName, lastName, DOB, startDate, homeTown, department);
            employeeList.Add(newEmployee);

            //string newEmployeeDetails = $"{employeeId},{firstName},{lastName},{stringDOB},{stringStartDate},{homeTown},{department}\n";

            //string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
            //StreamWriter sw = new StreamWriter(databasePath, false);
            //sw.WriteLine(newEmployeeDetails);
            //sw.Close(); 

            WriteToCsv(employeeList);

            Console.WriteLine("New employee added");
            Console.WriteLine("------------------");

            return employeeList;

        }

        // OPTION 3
        /// <summary>
        /// Edit DOB field of employee using employeeID
        /// </summary>
        /// <param name="employeeList"></param>
        /// <returns></returns>
        public  List<Employee> EditEmployee(List<Employee> employeeList)
        {

            Console.Write("Enter the ID of the employee you wish to edit:");
            int id = Convert.ToInt32(Console.ReadLine());


            var editItem = employeeList.FirstOrDefault(e => e.EmployeeId == id);
            if (editItem != null)
            {
                Console.WriteLine($"Record found - Name: {editItem.FirstName} {editItem.LastName} ID: {editItem.EmployeeId} DOB: {editItem.DOB} StartDate: {editItem.StartDate} ");
                Console.WriteLine($"Enter new date of birth for {editItem.FirstName}");
                var stringDOB = Console.ReadLine();
                var DOB = DateConvertor.StringToDateObject(stringDOB);

                editItem.DOB = DOB;
                WriteToCsv(employeeList);
                Console.WriteLine("Record updated!");
            }
            else
            {
                Console.WriteLine("Record not found");
            }

            return employeeList;
        }

        // OPTION 4
        /// <summary>
        /// Remove an employee from current list using employeeID
        /// </summary>
        /// <param name="employeeList"></param>
        public  List<Employee> RemoveEmployee(List<Employee> employeeList)
        {
            Console.Write("Enter the ID of the employee to be removed:");
            int id = Convert.ToInt32(Console.ReadLine());


            var removeItem = employeeList.FirstOrDefault(e => e.EmployeeId == id);
            if (removeItem != null)
            {
                Console.WriteLine($"{removeItem.FirstName} with ID {removeItem.EmployeeId} will be removed!");
                employeeList.Remove(removeItem);
                WriteToCsv(employeeList);

            }
            else
            {
                Console.WriteLine("Record not found");
            }

            return employeeList;

        }

        
        public  void WriteToCsv(List<Employee> employeeList)
        {
            string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
            StreamWriter sw = new StreamWriter(databasePath, false);
            StringBuilder sb = new StringBuilder();
            foreach (var employee in employeeList)
            {
                string stringDOB = DateConvertor.DateObjectToString(employee.DOB);
                string stringStartDate = DateConvertor.DateObjectToString(employee.StartDate);
                string employeeDetails = $"{employee.EmployeeId},{employee.FirstName},{employee.LastName},{stringDOB},{stringStartDate},{employee.HomeTown},{employee.Department}\n";
                sb.Append(employeeDetails);

            }
            sw.WriteLine(sb);
            sw.Close();
            return;
        }

        public  List<Employee> LoadDataViaCsv(List<Employee> employeeList)
        {
            //Clear list and load content from csv file
            employeeList.Clear();
            // retrieve path of data from config file
            string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];

            var line = File.ReadAllLines(databasePath);
            foreach (var x in line)
            {
                if (string.IsNullOrEmpty(x))
                    break;
                var values = x.Split(',');



                int employeeId = Convert.ToInt32(values[0]);
                string firstName = values[1];
                string lastName = values[2];
                string stringDOB = values[3];

                // convert DOB string to a DateTime object
                DateTime DOB = DateConvertor.StringToDateObject(stringDOB);

                string stringStartDate = values[4];
                DateTime startDate = DateConvertor.StringToDateObject(stringStartDate);

                string homeTown = values[5];
                string department = values[6];

                Employee newEmployee = new Employee(employeeId, firstName, lastName, DOB, startDate, homeTown, department);
                employeeList.Add(newEmployee);

            }

            
            return employeeList;
            //ShowMenu(employeeList);
        }

        public List<Employee> GetData(List<Employee> employeeList)
        {
            //Load data from updated csv file
            employeeList = LoadDataViaCsv(employeeList);

            return employeeList;
        }
    }
}
