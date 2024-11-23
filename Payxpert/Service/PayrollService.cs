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
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;

       
        //public PayrollService(IPayrollRepository payrollRepository)
        //{
        //    _payrollRepository = payrollRepository;
        //}
        public PayrollService()
        {
            _payrollRepository = new PayrollRepository();
        }


        public void GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)

        {
            try
            {
                Payroll payroll = _payrollRepository.GeneratePayroll(employeeId, startDate, endDate);

                if (payroll != null)
                {
                    Console.WriteLine("Payroll Generated:");
                    Console.WriteLine($"Payroll ID: {payroll.PayrollID}");
                    Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                    Console.WriteLine($"Start Date: {payroll.PayPeriodStartDate}");
                    Console.WriteLine($"End Date: {payroll.PayPeriodEndDate}");
                    Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                    Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                    Console.WriteLine($"Deductions: {payroll.Deductions}");
                    Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                }
                else
                {
                    Console.WriteLine("Payroll not found for the specified period.");
                    throw new PayrollGenerationException($"Failed to generate payroll for Employee ID: {employeeId} during the period {startDate.ToShortDateString()} - {endDate.ToShortDateString()}.");

                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void GetPayrollById(int payrollId)
        {
            try
            {
                Payroll payroll = _payrollRepository.GetPayrollById(payrollId);

                if (payroll == null)
                {
                   // Console.WriteLine($"Payroll not found with ID: {payrollId}");
                    throw new PayrollGenerationException($"Payroll not found with ID: {payrollId}.");//

                }
                else
                {
                    Console.WriteLine($"Payroll Found: Employee ID: {payroll.EmployeeID}, Pay Period: {payroll.PayPeriodStartDate.ToShortDateString()} - {payroll.PayPeriodEndDate.ToShortDateString()}, Net Salary: {payroll.NetSalary:C}");
                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void GetPayrollsForEmployee(int employeeId)
        {
            try
            {
                List<Payroll> payrolls = _payrollRepository.GetPayrollsForEmployee(employeeId);

                if (payrolls != null && payrolls.Count > 0)
                {
                    Console.WriteLine($"\nPayroll records for Employee ID: {employeeId}");
                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine(payroll.ToString());
                    }
                }
                else
                {
                   // Console.WriteLine($"No payroll records found for Employee ID: {employeeId}");
                    throw new PayrollGenerationException($"No payroll records found for Employee ID: {employeeId}.");

                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the custom exception
            }
        }


        public void GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<Payroll> payrolls = _payrollRepository.GetPayrollsForPeriod(startDate, endDate);

                if (payrolls.Count > 0)
                {
                    Console.WriteLine("\nPayrolls for the Period ");
                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine(payroll.ToString());
                    }
                }
                else
                {
                   // Console.WriteLine($"No payrolls found for the period {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.");
                    throw new PayrollGenerationException($"No payrolls found for the period {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.");

                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CalculateGrossSalaryForEmployee(int employeeId)
        {
            try
            {
                List<Payroll> payrolls = _payrollRepository.GetPayrollsForEmployee(employeeId);

                if (payrolls != null && payrolls.Count > 0)
                {
                    foreach (var payroll in payrolls)
                    {
                        decimal grossSalary = payroll.BasicSalary + payroll.OvertimePay;

                        Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                        Console.WriteLine($"Pay Period: {payroll.PayPeriodStartDate.ToShortDateString()} - {payroll.PayPeriodEndDate.ToShortDateString()}");
                        Console.WriteLine($"Basic Salary: {payroll.BasicSalary:C}");
                        Console.WriteLine($"Overtime Pay: {payroll.OvertimePay:C}");
                        Console.WriteLine($"Gross Salary: {grossSalary}");
                      
                    }
                }
                else
                {
                    Console.WriteLine($"No payroll records found for Employee ID: {employeeId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while calculating gross salary: {ex.Message}");
            }
        }




    }
}
