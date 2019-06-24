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

            List<Employee> employeeList = new List<Employee>();
            showMenu(employeeList);

            
        }

        //Initialize empty list containing employee objects
        private static void showMenu(List<Employee> employeeList)
        {
            Console.WriteLine("Welcome to the Employee program \nSelect an option:");
            Console.WriteLine("1 - Show all employees");
            Console.WriteLine("2 - Add a new employee");
            Console.WriteLine("3 - Edit an existing employee");
            Console.WriteLine("4 - Remove existing employee");
            Console.WriteLine("5 - List employees with a work anniversary withing the next month");
            Console.WriteLine("6 - List average age of employees in each department");
            Console.WriteLine("7 - List number of employees in each town");
            Console.WriteLine("8 - Add employee via csv");

            Console.WriteLine("0 - Exit");

            //Get input from user
            string input = Console.ReadLine();
            int userOption;

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
                    showEmployees(employeeList);
                    break;
                case 2:
                    addEmployeeManually(employeeList);
                    break;
                case 3:
                    editEmployee(employeeList);
                    break;
                case 4:
                    removeEmployee(employeeList);
                    break;
                case 5:
                    showEmployeeWithAnniversary(employeeList);
                    break;
                case 6:
                    averageEmployeeAgeInDept(employeeList);
                    break;
                case 7:
                    employeesPerTown(employeeList);
                    break;
                case 8:
                    addEmployeeViaCsv(employeeList);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option from 0 to 7");
                    break;
            }

            //if (userOption == 1)
            //{
            //    addEmployeeManually(employeeList);
            //    showMenu(employeeList);
            //}
            //else if (userOption == 2)
            //{
            //    addEmployeeViaCsv(employeeList);
            //}
            //else if (userOption == 3)
            //{
            //    showEmployees(employeeList);
            //}
            //else if (userOption == 4)
            //{
            //    Environment.Exit(0);
            //}

            showMenu(employeeList);
           // Console.ReadLine();
        }

        // Method to show number of employees for a given town
        private static void employeesPerTown(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        // Method to show calculate average age for a given department
        private static void averageEmployeeAgeInDept(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        private static void showEmployeeWithAnniversary(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        private static void removeEmployee(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        //edit employee method
        private static void editEmployee(List<Employee> employeeList)
        {
            throw new NotImplementedException();
        }

        private static void showEmployees(List<Employee> employeeList)
        {
            //employeeList = employeeList.Select(n=>n).ToList();

            Console.WriteLine("List of employees:");
            for (int i = 0; i < employeeList.Count; i++)
            {
                // Print out details of each employee
                employeeList[i].showDetails();
            }


            // List employees using Linq
           // var listOfEmployees = employeeList
           //.Select(e => new { e.Item1, e.Item2, e.Item3, e.Item4, e.Item5, e.Item6 });

            

           // foreach (var employee in listOfEmployees)
           // {
           //     Console.WriteLine($"Name: {employee.Item1} {employee.Item2} DOB: {employee.Item3} Start Date: {employee.Item4} Home Town: {employee.Item5} Dept: {employee.Item6}");            }


            // Console.ReadLine();
            // showMenu(employeeList);

        }

        private static void addEmployeeViaCsv(List<Employee> employeeList)
        {
            // retrieve path of data from config file
            string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
            //using (var reader = new StreamReader(@"..\..\MOCK_DATA.csv"))
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
                    DateTime DOB = DateConvertor.stringToDateObject(stringDOB);

                    string stringStartDate = values[3];
                    DateTime startDate = DateConvertor.stringToDateObject(stringStartDate);

                    string homeTown = values[4];
                    string department = values[5];

                    Employee newEmployee = new Employee(firstName, lastName, DOB, startDate, homeTown, department);
                    employeeList.Add(newEmployee);

                }
                Console.WriteLine("Employees added from csv file!");
                //Console.ReadLine();
            }
            showMenu(employeeList);
        }

        private static List<Employee>  addEmployeeManually(List<Employee> employeeList)
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter date of birth: ");
            string stringDOB = Console.ReadLine();

            DateTime DOB = DateConvertor.stringToDateObject(stringDOB);

            Console.Write("Enter start date: ");
            string stringStartDate = Console.ReadLine();

            DateTime startDate = DateConvertor.stringToDateObject(stringStartDate);

            Console.Write("Enter home town: ");
            string homeTown = Console.ReadLine();
            Console.Write("Enter department: ");
            string department = Console.ReadLine();

            Employee newEmployee = new Employee(firstName, lastName, DOB, startDate, homeTown, department);
            employeeList.Add(newEmployee);

            Console.WriteLine("New employee added");
            Console.ReadLine();

            return employeeList;





        }
    }
}
