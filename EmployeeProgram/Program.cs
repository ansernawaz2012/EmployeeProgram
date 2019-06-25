using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace EmployeeProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize empty list containing employee objects
            List<Employee> employeeList = new List<Employee>();
            ShowMenu(employeeList);
            
        }

        private static void ShowMenu(List<Employee> employeeList)
        {
            Console.WriteLine("Welcome to the Employee program \nSelect an option:");
            Console.WriteLine("1 - Show all employees");
            Console.WriteLine("2 - Add a new employee");
            Console.WriteLine("3 - Edit an existing employee");
            Console.WriteLine("4 - Remove existing employee");
            Console.WriteLine("5 - List employees with a work anniversary within the next month");
            Console.WriteLine("6 - List average age of employees in each department");
            Console.WriteLine("7 - List number of employees in each town");
            Console.WriteLine("8 - Add employee via csv");

            Console.WriteLine("0 - Exit");

            //Get input from user
            string input = Console.ReadLine();
            int userOption;
            
            //Force user to enter number
            while (!Int32.TryParse(input, out userOption))
            {
                Console.Write
                    ("Invalid input. Please enter a number: ");
                input = Console.ReadLine();
            }

            // Allow users to only select options 0 to 8
            switch (userOption)
            {
                case 1:
                    ShowEmployees(employeeList);
                    break;
                case 2:
                    AddEmployeeManually(employeeList);
                    break;
                case 3:
                    EditEmployee(employeeList);
                    break;
                case 4:
                    RemoveEmployee(employeeList);
                    break;
                case 5:
                    ShowEmployeeWithAnniversary(employeeList);
                    break;
                case 6:
                    AverageEmployeeAgeInDept(employeeList);
                    break;
                case 7:
                    EmployeesPerTown(employeeList);
                    break;
                case 8:
                    AddEmployeeViaCsv(employeeList);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option from 0 to 7");
                    break;
            }

            
            ShowMenu(employeeList);
           // Console.ReadLine();
        }

        /// <summary>
        ///  Method to show number of employees for each town
        /// </summary>
        /// <param name="employeeList"></param>
        private static void EmployeesPerTown(List<Employee> employeeList)
        {
            var results = employeeList
                          .GroupBy(e => e.homeTown)
                          .Select(e => new { Hometown = e.Key, NumberOfEmployees = e.Count() });

            //var groupedEmploeesByDepartment = empdep.GroupBy(x => x.Department).Select(x => new { Department = x.Key, EmployeesCount = x.Count() });
            Console.WriteLine("Number of employees per town.");

            foreach (var x in results)
            {
                Console.WriteLine(x);
            }

           }

        /// <summary>
        /// Method to calculate average employee age for each department
        /// </summary>
        /// <param name="employeeList"></param>        
        private static void AverageEmployeeAgeInDept(List<Employee> employeeList)
        {
            //Populate age field using date of birth 
            foreach (var employee in employeeList)
            {
                employee.age = DateConvertor.GetEmployeeAge(employee.DOB);
            }

            var results = employeeList
                          .GroupBy(e => e.department)
                          .Select(e => new { Department = e.Key, NumberOfEmployees = e.Count(), TotalAge = e.Sum(x=>x.age) });

            Console.WriteLine("Average age of employees per department.");


            foreach (var x in results)
            {
                Console.WriteLine($"The average age for {x.Department} is {x.TotalAge / x.NumberOfEmployees}");
            }
                        
        }
        /// <summary>
        /// List of employees 
        /// </summary>
        /// <param name="employeeList"></param>
        private static void ShowEmployeeWithAnniversary(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                var currentDayOfYear = DateTime.Now.DayOfYear;
                var empStartDate = employee.startDate.DayOfYear;
                if (empStartDate - currentDayOfYear <= 30 && empStartDate > currentDayOfYear )
                {
                    Console.WriteLine($"{employee.firstName} with a start date of {employee.startDate} has an anniversary within 30 days");
                }
            }
        }

        /// <summary>
        /// Remove an employee from current list
        /// </summary>
        /// <param name="employeeList"></param>
        private static void RemoveEmployee(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        //edit employee method
        private static void EditEmployee(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Display list of employees 
        /// </summary>
        /// <param name="employeeList"></param>
        private static void ShowEmployees(List<Employee> employeeList)
        {
            //employeeList = employeeList.Select(n=>n).ToList();

            Console.WriteLine("List of employees:");
            //for (int i = 0; i < employeeList.Count; i++)
            //{
            //    // Print out details of each employee
            //    employeeList[i].ShowDetails();
            //}


            // List employees using Linq
            var listOfEmployees = employeeList
                .OrderByDescending(e =>e.startDate)
                .Select(e => e);



            foreach (var employee in listOfEmployees)
            {
                Console.WriteLine($"Name: {employee.firstName} {employee.lastName} ");
                Console.WriteLine($"DOB: {employee.DOB}");
                Console.WriteLine($"Start Date: {employee.startDate}");
                Console.WriteLine($"Home Town: {employee.homeTown}");
                Console.WriteLine($"Dept: {employee.department}");
                Console.WriteLine("-------------------------------------");
            }


            // Console.ReadLine();
            // showMenu(employeeList);

        }

        private static void AddEmployeeViaCsv(List<Employee> employeeList)
        {
            // retrieve path of data from config file
            string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
            using (var reader = new StreamReader(databasePath))

            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                   
                    // assign each value from values array to corresponding field
                    string firstName = values[0];
                    string lastName = values[1];
                    string stringDOB = values[2];
                   
                    // convert DOB string to a DateTime object
                    DateTime DOB = DateConvertor.StringToDateObject(stringDOB);

                    string stringStartDate = values[3];
                    DateTime startDate = DateConvertor.StringToDateObject(stringStartDate);

                    string homeTown = values[4];
                    string department = values[5];

                    Employee newEmployee = new Employee(firstName, lastName, DOB, startDate, homeTown, department);
                    employeeList.Add(newEmployee);

                }
                Console.WriteLine("Employees added from csv file!");
                //Console.ReadLine();
            }
            ShowMenu(employeeList);
        }
        /// <summary>
        /// Add new employee manually
        /// </summary>
        /// <param name="employeeList"></param>
        /// <returns></returns>
        private static List<Employee>  AddEmployeeManually(List<Employee> employeeList)
        {

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

            Employee newEmployee = new Employee(firstName, lastName, DOB, startDate, homeTown, department);
            employeeList.Add(newEmployee);

            string newEmployeeDetails = $"\n{firstName},{lastName},{stringDOB},{stringStartDate},{homeTown},{department}";

            string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
            StreamWriter sw = new StreamWriter(databasePath, true);
            sw.WriteLine(newEmployeeDetails);
            sw.Close(); 
            
            Console.WriteLine("New employee added");
            Console.ReadLine();

            return employeeList;

        }
    }
}
