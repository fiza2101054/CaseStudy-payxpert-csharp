using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    public interface IEmployeeService
    {
        /*List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);*/
        void GetAllEmployees();
        void GetEmployeeById(int employeeId);
        void AddEmployee(Employee employee);
        void RemoveEmployee(int employeeId);
        //void UpdateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

    }
}
