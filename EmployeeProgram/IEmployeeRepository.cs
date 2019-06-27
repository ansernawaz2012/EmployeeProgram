using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
   public interface IEmployeeRepository
    {

        List<Employee> ShowEmployees(List<Employee> employeeList);
        List<Employee> AddEmployeeManually(List<Employee> employeeList);
        List<Employee> EditEmployee(List<Employee> employeeList);
        List<Employee> RemoveEmployee(List<Employee> employeeList);
        void ShowEmployeeWithAnniversary(List<Employee> employeeList);
        void AverageEmployeeAgeInDept(List<Employee> employeeList);
        void EmployeesPerTown(List<Employee> employeeList);
        void WriteToCsv(List<Employee> employeeList);
        List<Employee> LoadDataViaCsv(List<Employee> employeeList);
    }
}
