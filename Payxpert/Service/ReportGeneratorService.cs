using Payxpert.Model;
using Payxpert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    internal class ReportGeneratorService : IReportGeneratorService
    {

        private readonly ReportGeneratorRepository _reportGeneratorRepository;

      
        public ReportGeneratorService()
        {
            _reportGeneratorRepository = new ReportGeneratorRepository(); 
        }

        
        public ReportGenerator GetReportForEmployee(int employeeId)
        {
            try
            {
               
                ReportGenerator report = _reportGeneratorRepository.GetReportForEmployee(employeeId);

                if (report != null)
                {
                   
                    Console.WriteLine("Employee Report:");
                    Console.WriteLine($"Employee ID: {report.EmployeeDetails.EmployeeID}");
                    Console.WriteLine($"Name: {report.EmployeeDetails.FirstName} {report.EmployeeDetails.LastName}");
                    Console.WriteLine($"Position: {report.EmployeeDetails.Position}");
                    Console.WriteLine($"Email: {report.EmployeeDetails.Email}");
                    Console.WriteLine($"Phone: {report.EmployeeDetails.PhoneNumber}");
                    Console.WriteLine($"Address: {report.EmployeeDetails.Address}");
                    Console.WriteLine($"Date of Birth: {report.EmployeeDetails.DateOfBirth.ToShortDateString()}");
                    Console.WriteLine($"Joining Date: {report.EmployeeDetails.JoiningDate.ToShortDateString()}");
                    if (report.EmployeeDetails.TerminationDate.HasValue)
                    {
                        Console.WriteLine($"Termination Date: {report.EmployeeDetails.TerminationDate.Value.ToShortDateString()}");
                    }

                    Console.WriteLine("\nPayroll History:");
                    foreach (var payroll in report.PayrollHistory)
                    {
                        Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Pay Period: {payroll.PayPeriodStartDate.ToShortDateString()} to {payroll.PayPeriodEndDate.ToShortDateString()}");
                        Console.WriteLine($"Basic Salary: {payroll.BasicSalary:C}, Overtime Pay: {payroll.OvertimePay:C}, Deductions: {payroll.Deductions:C}");
                    }

                    Console.WriteLine("\nTax History:");
                    foreach (var tax in report.TaxHistory)
                    {
                        Console.WriteLine($"Tax Year: {tax.TaxYear}, Taxable Income: {tax.TaxableIncome:C}, Tax Amount: {tax.TaxAmount:C}");
                    }

                    Console.WriteLine("\nFinancial Records:");
                    foreach (var financialRecord in report.FinancialRecords)
                    {
                        Console.WriteLine($"Record Date: {financialRecord.RecordDate.ToShortDateString()}, Description: {financialRecord.Description}");
                        Console.WriteLine($"Amount: {financialRecord.Amount:C}, Record Type: {financialRecord.RecordType}");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to generate the report.");
                }

                return report; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
                return null; 
            }
        }
    }
}
