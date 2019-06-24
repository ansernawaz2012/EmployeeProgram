using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
    public class Employee
    {
        public string  firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime startDate { get; set; }
        public string homeTown { get; set; }
        public  string department { get; set; }


        public Employee(string firstName, string lastName, DateTime DOB, DateTime startDate, string homeTown, string department)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.DOB = DOB;
            this.startDate = startDate;  
            this.homeTown = homeTown;
            this.department = department;
    }

        public void showDetails()
        {
            Console.WriteLine($"Name: {firstName} {lastName} DOB: {DOB} Start Date: {startDate} Home Town: {homeTown} Dept: {department}");
        }

    }

    
}
