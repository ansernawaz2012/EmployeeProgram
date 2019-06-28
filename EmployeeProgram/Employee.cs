using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime StartDate { get; set; }
        public string HomeTown { get; set; }
        public string Department { get; set; }
       
        public int Age {
            get {

                int age = DateTime.Now.Year - DOB.Year;

                if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                {
                    age = age - 1;
                }

                return age;
            }
        }



        public Employee(int employeeId, string firstName, string lastName, DateTime DOB, DateTime startDate, string homeTown, string department)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DOB = DOB;
            this.StartDate = startDate;  
            this.HomeTown = homeTown;
            this.Department = department;
        
    }
        

        
    }

    
}
