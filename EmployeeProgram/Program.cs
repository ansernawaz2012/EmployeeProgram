using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmployeeProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Employee> employeeList = new List<Employee>();
            showMenu(employeeList);

            
        }

        private static void showMenu(List<Employee> employeeList)
        {
            Console.WriteLine("Welcome to the Employee program \nSelect an option:");
            Console.WriteLine("1 - Add employee manually: \n2 - Add employee via csv file:");
            Console.WriteLine("3 - List all employees:");
            Console.WriteLine("4 - Exit");
            int userOption = Convert.ToInt32(Console.ReadLine());

            if (userOption == 1)
            {
                addEmployeeManually(employeeList);
                showMenu(employeeList);
            }
            else if (userOption == 2)
            {
                addEmployeeViaCsv(employeeList);
            }
            else if (userOption == 3)
            {
                showEmployees(employeeList);
            }
            else if (userOption == 4)
            {
                Environment.Exit(0);
            }

            Console.ReadLine();
        }

        private static void showEmployees(List<Employee> employeeList)
        {
            //employeeList = employeeList.Select(n=>n).ToList();

            Console.WriteLine("List of employees:");
            for (int i = 0; i < employeeList.Count; i++)
            {
                // Console.WriteLine(employeeList[i].firstName);
                employeeList[i].showDetails();
            }

            

            Console.ReadLine();
            showMenu(employeeList);
            
                }

        private static void addEmployeeViaCsv(List<Employee> employeeList)
        {
            using (var reader = new StreamReader(@"..\..\MOCK_DATA.csv"))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                   // Console.WriteLine("Output from the CSV file");
                   // Console.WriteLine(line);
                   // Console.WriteLine(values[0]);
                    //Console.WriteLine(employeeList.Count);

                    string firstName = values[0];
                    string lastName = values[1];
                    string DOB = values[2];
                    string startDate = values[3];
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
            string DOB = Console.ReadLine();
            Console.Write("Enter start date: ");
            string startDate = Console.ReadLine();
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
