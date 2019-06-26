﻿using System;
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
            LoadDataViaCsv(employeeList);
            ShowMenu(employeeList);
            
        }

        private static void ShowMenu(List<Employee> employeeList)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            Console.WriteLine("Welcome to the Employee program \nSelect an option:");
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
                //case 8:
                //    AddEmployeeViaCsv(employeeList);
                //    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option from 0 to 7");
                    break;
            }

            
            ShowMenu(employeeList);
          
        }

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

        /// <summary>
        /// Method to calculate average employee age for each department
        /// </summary>
        /// <param name="employeeList"></param>        
        private static void AverageEmployeeAgeInDept(List<Employee> employeeList)
        {
            //Populate age field using date of birth 
            //foreach (var employee in employeeList)
            //{
            //    employee.Age = DateConvertor.GetEmployeeAge(employee.DOB);
            //}

            var results = employeeList
                          .GroupBy(e => e.Department)
                          .Select(e => new { Department = e.Key, NumberOfEmployees = e.Count(), TotalAge = e.Sum(x=>x.Age) });

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
                if(employee.StartDate.CompareStartDate())
                {
                    Console.WriteLine($"{employee.FirstName} with a start date of {employee.StartDate} has an anniversary within 30 days");
                };


                //var currentDayOfYear = DateTime.Now.DayOfYear;
                //var empStartDate = employee.StartDate.DayOfYear;
                //if (empStartDate - currentDayOfYear <= 30 && empStartDate > currentDayOfYear )
                //{
                //    Console.WriteLine($"{employee.FirstName} with a start date of {employee.StartDate} has an anniversary within 30 days");
                //}
            }
        }
        
        


        /// <summary>
        /// Remove an employee from current list
        /// </summary>
        /// <param name="employeeList"></param>
        private static List<Employee> RemoveEmployee(List<Employee> employeeList)
        {
            Console.Write("Enter the ID of the employee to be removed:");
            int id = Convert.ToInt32(Console.ReadLine());


            var removeItem = employeeList.FirstOrDefault(e => e.EmployeeId == id);
            if(removeItem != null)
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

        //edit employee method
        private static List<Employee> EditEmployee(List<Employee> employeeList)
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


        /// <summary>
        /// Display list of employees 
        /// </summary>
        /// <param name="employeeList"></param>
        private static List<Employee> ShowEmployees(List<Employee> employeeList)
        {
            //Re-load data from updated csv file
            employeeList = LoadDataViaCsv(employeeList);

            Console.WriteLine("List of employees:");
            


            // List employees using Linq
            var listOfEmployees = employeeList
                .OrderBy(e =>e.EmployeeId)
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

        //used for testing purposes
        //private static void AddEmployeeViaCsv(List<Employee> employeeList)
        //{
        //    // retrieve path of data from config file
        //    string databasePath = ConfigurationManager.AppSettings["CsvDatabasePath"];
        //    using (var reader = new StreamReader(databasePath))

        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            var values = line.Split(',');


        //            // assign each value from values array to corresponding field
        //            int employeeId = Convert.ToInt32(values[0]);
        //            string firstName = values[1];
        //            string lastName = values[2];
        //            string stringDOB = values[3];
                   
        //            // convert DOB string to a DateTime object
        //            DateTime DOB = DateConvertor.StringToDateObject(stringDOB);

        //            string stringStartDate = values[4];
        //            DateTime startDate = DateConvertor.StringToDateObject(stringStartDate);

        //            string homeTown = values[5];
        //            string department = values[6];

        //            Employee newEmployee = new Employee(employeeId, firstName, lastName, DOB, startDate, homeTown, department);
        //            employeeList.Add(newEmployee);

        //        }
        //        Console.WriteLine("Employees added from csv file!");
        //        //Console.ReadLine();
        //    }
        //    ShowMenu(employeeList);
        //}


        /// <summary>
        /// Add new employee manually
        /// </summary>
        /// <param name="employeeList"></param>
        /// <returns></returns>
        private static List<Employee>  AddEmployeeManually(List<Employee> employeeList)
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

        private static List<Employee> LoadDataViaCsv(List<Employee> employeeList)
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

            //using (var reader = new StreamReader(databasePath))

            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        if (string.IsNullOrEmpty(line))
            //         break;
            //        var values = line.Split(',');


            //        // assign each value from values array to corresponding field
            //        int employeeId = Convert.ToInt32(values[0]);
            //        string firstName = values[1];
            //        string lastName = values[2];
            //        string stringDOB = values[3];

            //        // convert DOB string to a DateTime object
            //        DateTime DOB = DateConvertor.StringToDateObject(stringDOB);

            //        string stringStartDate = values[4];
            //        DateTime startDate = DateConvertor.StringToDateObject(stringStartDate);

            //        string homeTown = values[5];
            //        string department = values[6];

            //        Employee newEmployee = new Employee(employeeId, firstName, lastName, DOB, startDate, homeTown, department);
            //        employeeList.Add(newEmployee);

            //    }
               
            //}
            return employeeList;
            //ShowMenu(employeeList);
        }

        private static void WriteToCsv(List<Employee> employeeList)
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
    }
}
