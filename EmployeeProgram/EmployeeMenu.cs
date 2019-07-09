using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;



namespace EmployeeProgram
{
    public class EmployeeMenu
    {
        private static  IEmployeeRepository _repository;
        static RestClient client = new RestClient("http://localhost:55026");

        public EmployeeMenu(IEmployeeRepository repository)
        {
            _repository = repository;

            //Initialize empty list containing employee objects
            List<Employee> employeeList = new List<Employee>();
            _repository.GetData(employeeList);

            //employeeList = GetEmployeeListFromAPI();
            Console.WriteLine("Welcome to the Employee program");

            ShowMenu(employeeList);
        }

        /// <summary>
        /// Method to retrieve list of employees from Web API
        /// </summary>
        /// <returns></returns>
        //private static List<Employee> GetEmployeeListFromAPI()
        //{
        //   // var client = new RestClient("http://localhost:55026");
        //    var request = new RestRequest("api/employee", Method.GET);
        //    // request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

        //    var queryResult = client.Execute<List<Employee>>(request);

        //    //var queryResult = client.Execute(request);
        //    //List<Employee> results = JsonConvert.DeserializeObject<Employee>(queryResult.Content);

        //    //Console.WriteLine(queryResult.Content);
        //    return queryResult.Data;
        //}
        private static void ShowMenu(List<Employee> employeeList)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine("------------------");
            Console.WriteLine(" Select an option ");
            Console.WriteLine("------------------");
            Console.WriteLine("1 - Show all employees");
            Console.WriteLine("2 - Add a new employee");
            Console.WriteLine("3 - Edit an existing employee");
            Console.WriteLine("4 - Remove existing employee");
            Console.WriteLine("5 - List employees with a work anniversary within the next month");
            Console.WriteLine("6 - List average age of employees in each department");
            Console.WriteLine("7 - List number of employees in each town");


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
                    _repository.ShowEmployees(employeeList);
                    break;
                case 2:
                    _repository.AddEmployeeManually(employeeList);
                    break;
                case 3:
                    _repository.EditEmployee(employeeList);
                    break;
                case 4:
                    _repository.RemoveEmployee(employeeList);
                    break;
                case 5:
                    APIShowEmployeeWithAnniversary(employeeList);
                    break;
                case 6:
                    APIAverageEmployeeAgeInDept(employeeList);
                    break;
                case 7:
                    APIEmployeesPerTown(employeeList);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option from 0 to 7");
                    break;
            }


            ShowMenu(employeeList);

        }

        // OPTION 5
        /// <summary>
        /// List of employees that have a work anniversary within 30 days 
        /// </summary>
        /// <param name="employeeList"></param>
        private static void ShowEmployeeWithAnniversary(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                if (employee.StartDate.CompareStartDate())
                {
                    Console.WriteLine($"{employee.FirstName} with a start date of {employee.StartDate} has an anniversary within 30 days");
                };

            }
        }

        private static void APIShowEmployeeWithAnniversary(List<Employee> employeeList)
        {
            var request = new RestRequest("api/employee/ShowEmployeeWithAnniversary", Method.GET);


            var queryResult = client.Execute<List<string>>(request);

            foreach (var x in queryResult.Data)
            {
                Console.WriteLine(x);
            }
        }

        // OPTION 6
        /// <summary>
        /// Method to calculate average employee age for each department
        /// </summary>
        /// <param name="employeeList"></param>        
        private static void AverageEmployeeAgeInDept(List<Employee> employeeList)
        {

            var results = employeeList
                          .GroupBy(e => e.Department)
                          .Select(e => new { Department = e.Key, NumberOfEmployees = e.Count(), TotalAge = e.Sum(x => x.Age) });

            Console.WriteLine("Average age of employees per department.");


            foreach (var x in results)
            {
                Console.WriteLine($"The average age for {x.Department} is {Math.Round((float)x.TotalAge / (float)x.NumberOfEmployees, 1, MidpointRounding.AwayFromZero)}");
            }

        }

        private static void APIAverageEmployeeAgeInDept(List<Employee> employeeList)
        {
            var request = new RestRequest("api/employee/GetAverageEmployeeAgeInDept", Method.GET);


            var queryResult = client.Execute<List<string>>(request);

            foreach (var x in queryResult.Data)
            {
                Console.WriteLine(x);
            }
        }


        // OPTION 7
        /// <summary>
        ///  Method to show number of employees for each town
        /// </summary>
        /// <param name="employeeList"></param>
        private static void EmployeesPerTown(List<Employee> employeeList)
        {
            var results = employeeList
                          .GroupBy(e => e.HomeTown)
                          .Select(e => new { Hometown = e.Key, NumberOfEmployees = e.Count() });

            Console.WriteLine("Number of employees per town.");

            foreach (var x in results)
            {
                Console.WriteLine($"The number of employees from {x.Hometown} is {x.NumberOfEmployees}");
            }

        }

        private static void APIEmployeesPerTown(List<Employee> employeeList)
        {
            
            var request = new RestRequest("api/employee/GetEmployeesPerTown", Method.GET);
            

            var queryResult = client.Execute<List<string>>(request);

            foreach (var x in queryResult.Data)
            {
                Console.WriteLine(x);
            }
            //var queryResult = client.Execute(request);
            //List<Employee> results = JsonConvert.DeserializeObject<Employee>(queryResult.Content);

            //Console.WriteLine(queryResult.Content);
            //return queryResult.Data;

            //foreach (var x in results)
            //{
            //    Console.WriteLine($"The number of employees from {x.Hometown} is {x.NumberOfEmployees}");
            //}

        }

    }
}
    

