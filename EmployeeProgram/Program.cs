using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                addEmployeeViaCsv();
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

        private static void addEmployeeViaCsv()
        {
            throw new NotImplementedException();
        }

        private static List<Employee>  addEmployeeManually(List<Employee> employeeList)
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter date of birth: ");
            float DOB = Convert.ToSingle(Console.ReadLine());
            Console.Write("Enter start date: ");
            float startDate = Convert.ToSingle(Console.ReadLine());
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
