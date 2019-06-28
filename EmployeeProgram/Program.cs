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

       // static readonly IEmployeeRepository repository = new EmployeeRepository();

        static void Main(string[] args)
        {
             IEmployeeRepository repository = new EmployeeRepository();


            EmployeeMenu employeeMenu = new EmployeeMenu(repository);

            
            
        }
                
    }
}
