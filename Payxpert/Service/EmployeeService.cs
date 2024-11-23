using Payxpert.Exceptions;
using Payxpert.Model;
using Payxpert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{

    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;

        //public EmployeeService(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}
        public EmployeeService()
        {
            _employeeRepository= new EmployeeRepository();
        }

        public void GetAllEmployees()
        {
            List<Employee> employees = _employeeRepository.GetAllEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found in the database.");
            }
            else
            {
                Console.WriteLine("Employees List:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
                }
            }
        }
        public void  GetEmployeeById(int employeeId)
        {
            try
            {
                Employee employee = _employeeRepository.GetEmployeeById(employeeId);

               
                    if (employee == null )
                {

                    throw new EmployeeNotFoundException($"Employee not found with {employeeId}");
                   // throw new EmployeeNotFoundException($"{employeeId}");


                    //Console.WriteLine($"Employee not found with ID:  {employeeId}");
                }
                else
                {
                    Console.WriteLine($"Employee Found: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee); 
            Console.WriteLine("Employee added successfully");
        }

        
        public void RemoveEmployee(int employeeId)
        {

            try
            {
                bool isDeleted = _employeeRepository.RemoveEmployee(employeeId);
                if (isDeleted)
                {
                   // Console.WriteLine($"Employee with ID {employeeId} was successfully removed.");
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found or could not be deleted.");

                }
                else
                {
                    Console.WriteLine($"Employee with ID {employeeId} not found or could not be deleted.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                bool isUpdated = _employeeRepository.UpdateEmployee(employee);

                if (isUpdated)
                {
                    Console.WriteLine("Employee updated successfully.");
                   // throw new EmployeeNotFoundException($"Employee with ID {employee.EmployeeID} not found or could not be updated.");

                }
                else
                {
                    Console.WriteLine("Employee update failed. Please check the provided details.");
                     throw new EmployeeNotFoundException($"Employee with ID {employee.EmployeeID} not found or could not be updated.");


                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }



    }
}
