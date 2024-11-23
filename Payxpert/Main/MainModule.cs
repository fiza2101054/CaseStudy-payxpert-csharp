using Payxpert.Exceptions;
using Payxpert.Model;
using Payxpert.Repository;
using Payxpert.Service;
using System.Text.RegularExpressions;


namespace Payxpert
{
   public class MainModule
    {
       static void Main(string[] args)

        {


           
            IEmployeeService employeeService = new EmployeeService();
            
            IPayrollService payrollService = new PayrollService();

            ITaxService taxService = new TaxService();

            IFinancialRecordService financialRecordService = new FinancialRecordService();

            IReportGeneratorService reportGeneratorService = new ReportGeneratorService();  

            ValidationService validationService = new ValidationService();

            bool continueRunning = true;
            while (continueRunning)
            {
              
                Console.WriteLine("===================================");
                Console.WriteLine("            PAYXPERT MENU          ");
                Console.WriteLine("===================================");
                Console.WriteLine(" 1.  Get All Employees");
                Console.WriteLine(" 2.  Get Employee By ID");
                Console.WriteLine(" 3.  Add Employee");
                Console.WriteLine(" 4.  Remove Employee");
                Console.WriteLine(" 5.  Update Employee");
                Console.WriteLine(" 6.  Get Payroll");
                Console.WriteLine(" 7.  Get Payroll By ID");
                Console.WriteLine(" 8.  Get Payroll By Employee ID");
                Console.WriteLine(" 9.  Get Payroll For Period");
                Console.WriteLine("10.  Get Tax By ID");
                Console.WriteLine("11.  Get Tax By Employee");
                Console.WriteLine("12.  Get Tax By Year");
                Console.WriteLine("13.  Calculate Tax For Employee");
                Console.WriteLine("14.  Add Financial Record");
                Console.WriteLine("15.  Get Financial Record By ID");
                Console.WriteLine("16.  Get Financial Records By Employee");
                Console.WriteLine("17.  Get Financial Records By Date");
                Console.WriteLine("18.  Calculate Gross Salary For Employee");
                Console.WriteLine("19.  Generate Report For Employee");
                Console.WriteLine("20.  Exit");
                Console.WriteLine("===================================");
                Console.WriteLine("Please select an option (1-20):");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            employeeService.GetAllEmployees();
                            break;

                        case "2":
                            Console.Write("Enter Employee ID: ");
                            if (int.TryParse(Console.ReadLine(), out int employeeId))
                            {
                                employeeService.GetEmployeeById(employeeId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Enter Employee Details:");
                            Employee newEmployee = new Employee();

                            Console.Write("First Name: ");
                            string firstName = Console.ReadLine();
                            validationService.ValidateNotEmpty(firstName, "First Name");
                            newEmployee.FirstName = firstName;

                            Console.Write("Last Name: ");
                            string lastName = Console.ReadLine();
                            validationService.ValidateNotEmpty(lastName, "Last Name");
                            newEmployee.LastName = lastName;

                            Console.Write("Date of Birth (YYYY-MM-DD): ");
                            string dob = Console.ReadLine();
                            validationService.ValidateDate(dob, "Date of Birth");
                            newEmployee.DateOfBirth = DateTime.Parse(dob);

                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            validationService.ValidateEmail(email);
                            newEmployee.Email = email;

                            Console.Write("Phone Number: ");
                            string phoneNumber = Console.ReadLine();
                            validationService.ValidatePhoneNumber(phoneNumber);
                            newEmployee.PhoneNumber = phoneNumber;

                            Console.Write("Position: ");
                            string position = Console.ReadLine();
                           // validationService.ValidatePosition(position);
                            newEmployee.Position = position;

                            employeeService.AddEmployee(newEmployee);
                            break;

                        case "4":
                            Console.Write("Enter Employee ID to remove: ");
                            if (int.TryParse(Console.ReadLine(), out int removeEmployeeId))
                            {
                                employeeService.RemoveEmployee(removeEmployeeId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;

                        case "5":
                            Console.Write("Enter Employee ID to update: ");
                            int updateId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter new First Name (leave blank to skip): ");
                            string updateFirstName = Console.ReadLine();
                            if (string.IsNullOrEmpty(updateFirstName)) updateFirstName = null;

                            Console.Write("Enter new Last Name (leave blank to skip): ");
                            string updateLastName = Console.ReadLine();
                            if (string.IsNullOrEmpty(updateLastName)) updateLastName = null;

                            Console.Write("Enter Position (leave blank to skip): ");
                            string updatePosition = Console.ReadLine();
                            if (string.IsNullOrEmpty(updatePosition)) updatePosition = null;

                            Employee updateEmployee = new Employee
                            {
                                EmployeeID = updateId,
                                FirstName = updateFirstName,
                                LastName = updateLastName,
                                Position = updatePosition,

                            };

                            employeeService.UpdateEmployee(updateEmployee);
                            break;



                        case "6":

                            Console.Write("Enter Employee ID for Payroll: ");
                            if (int.TryParse(Console.ReadLine(), out employeeId))
                            {
                                Console.Write("Enter Start Date (YYYY-MM-DD): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime inputStartDate))
                                {
                                    Console.Write("Enter End Date (YYYY-MM-DD): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime inputEndDate))
                                    {

                                        payrollService.GeneratePayroll(employeeId, inputStartDate, inputEndDate);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid End Date.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Start Date.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID.");
                            }
                            break;

                        case "7":
                            Console.Write("Enter Payroll ID: ");
                            if (int.TryParse(Console.ReadLine(), out int payrollId))
                            {
                                payrollService.GetPayrollById(payrollId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Payroll ID. Please enter a valid integer.");
                            }
                            break;


                        case "8":
                            Console.WriteLine("Enter the Employee ID:");
                            if (int.TryParse(Console.ReadLine(), out int empId))
                            {
                                payrollService.GetPayrollsForEmployee(empId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid number.");
                            }
                            break;


                        
                            
                        case "9":
                            Console.WriteLine("Enter the start date (yyyy-mm-dd):");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                            {
                                Console.WriteLine("Enter the end date (yyyy-mm-dd):");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                                {
                                    payrollService.GetPayrollsForPeriod(startDate, endDate);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid end date. Please enter a valid date.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid start date. Please enter a valid date.");
                            }
                            break;


                        case "10":
                            Console.Write("Enter Tax ID: ");
                            if (int.TryParse(Console.ReadLine(), out int taxId))
                            {
                                taxService.GetTaxById(taxId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Tax ID. Please enter a valid integer.");
                            }
                            break;


                        case "11":

                            Console.Write("Enter Employee ID: ");
                            if (int.TryParse(Console.ReadLine(), out int EmployeeId))
                            {
                                taxService.GetTaxesForEmployee(EmployeeId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;


                        case "12":
                            Console.Write("Enter Tax Year: ");
                            if (int.TryParse(Console.ReadLine(), out int taxYear))
                            {
                                taxService.GetTaxesForYear(taxYear);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Tax Year. Please enter a valid integer.");
                            }
                            break;

                        case "13":
                            Console.Write("Enter Employee ID: ");
                            if (int.TryParse(Console.ReadLine(), out int employeeIdForTax))
                            {
                                Console.Write("Enter Tax Year: ");
                                if (int.TryParse(Console.ReadLine(), out int TaxYear))
                                {
                                    taxService.CalculateTax(employeeIdForTax, TaxYear);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Tax Year. Please enter a valid year.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;


                        case "14": 
                            Console.WriteLine("Enter Financial Record Details:");

                            Console.Write("Enter Employee ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int employeeID))
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                                break;
                            }

                           
                            Console.Write("Enter Description: ");
                            string description = Console.ReadLine();
                            validationService.ValidateNotEmpty(description, "Description");

                           
                            Console.Write("Enter Amount: ");
                            string amountInput = Console.ReadLine();
                            validationService.ValidateNotEmpty(amountInput, "Amount");
                            validationService.ValidateDecimal(amountInput, "Amount");
                            decimal amount = decimal.Parse(amountInput);

                            
                            Console.Write("Enter Record Type (e.g., 'Income', 'Expense'): ");
                            string recordType = Console.ReadLine();
                            validationService.ValidateNotEmpty(recordType, "Record Type");

                            
                            financialRecordService.AddFinancialRecord(employeeID, description, amount, recordType);
                            Console.WriteLine("Financial record added successfully.");
                            break;

                        case "15":
                            Console.Write("Enter Financial Record ID: ");
                            if (int.TryParse(Console.ReadLine(), out int financialRecordId))
                            {
                                financialRecordService.GetFinancialRecordById(financialRecordId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Financial Record ID. Please enter a valid integer.");
                            }
                            break;


                        case "16": 
                            Console.Write("Enter Employee ID: ");
                            if (int.TryParse(Console.ReadLine(), out int EmployeeID))
                            {
                                financialRecordService.GetFinancialRecordsForEmployee(EmployeeID);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;


                        case "17":

                            Console.Write("Enter Record Date (YYYY-MM-DD): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime recordDate))
                            {
                                financialRecordService.GetFinancialRecordsForDate(recordDate);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Date. Please enter a valid date in the format YYYY-MM-DD.");
                            }
                            break;

                        case "18":
                            Console.Write("Enter Employee ID to calculate Gross Salary: ");
                            if (int.TryParse(Console.ReadLine(), out int grossSalaryEmployeeId))
                            {
                                payrollService.CalculateGrossSalaryForEmployee(grossSalaryEmployeeId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;


                        case "19": 
                            Console.Write("Enter Employee ID to generate report: ");
                            if (int.TryParse(Console.ReadLine(), out int reportEmployeeId))
                            {
                                reportGeneratorService.GetReportForEmployee(reportEmployeeId);  
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
                            }
                            break;

                        case "20":
                            Console.WriteLine("Exiting the application...");
                            continueRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option from the menu.");
                            break;
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }


        }
    }

}


