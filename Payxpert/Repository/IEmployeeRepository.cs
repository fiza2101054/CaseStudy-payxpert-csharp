using Payxpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        bool RemoveEmployee(int employeeId);
        //bool UpdateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
    }
}
