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
    internal class FinancialRecordService : IFinancialRecordService
    {

        private readonly IFinancialRecordRepository _financialRecordRepository;

        //public FinancialRecordService(IFinancialRecordRepository financialRecordRepository)
        //{
        //    _financialRecordRepository = financialRecordRepository;
        //}
        public FinancialRecordService()
        {
            _financialRecordRepository = new FinancialRecordRepository();


        }

        
        public void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            if (employeeId <= 0)
            {
                Console.WriteLine("Error: Invalid employee ID.");
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Error: Description cannot be empty.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Error: Amount must be greater than zero.");
                return;
            }

            if (string.IsNullOrEmpty(recordType))
            {
                Console.WriteLine("Error: Record type cannot be empty.");
                return;
            }

            
            _financialRecordRepository.AddFinancialRecord(employeeId, description, amount, recordType);
            Console.WriteLine("Financial record added successfully.");
        }

        public void GetFinancialRecordById(int recordId)
        {

            try
            {
                
                FinancialRecord financialRecord = _financialRecordRepository.GetFinancialRecordById(recordId);

                
                if (financialRecord == null)
                {
                    throw new FinancialRecordException($"Financial record with ID {recordId} not found.");

                    // Console.WriteLine($"Financial record with ID {recordId} not found.");
                }
                else
                {
                
                    Console.WriteLine($"Financial Record Found: \n" +
                                      $"Record ID: {financialRecord.RecordID}\n" +
                                      $"Employee ID: {financialRecord.EmployeeID}\n" +
                                      $"Date: {financialRecord.RecordDate:yyyy-MM-dd}\n" +
                                      $"Description: {financialRecord.Description}\n" +
                                      $"Amount: {financialRecord.Amount}\n" +
                                      $"Record Type: {financialRecord.RecordType}");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void GetFinancialRecordsForEmployee(int employeeId)
        {
            try
            {
                List<FinancialRecord> financialRecords = _financialRecordRepository.GetFinancialRecordsForEmployee(employeeId);

                if (financialRecords == null || financialRecords.Count == 0)

                {
                    throw new FinancialRecordException($"Financial record with ID {employeeId} not found.");

                    // Console.WriteLine($"No financial records found for Employee ID: {employeeId}");
                }
                else
                {
                    Console.WriteLine($"Financial Records for Employee ID: {employeeId}");

                    foreach (var record in financialRecords)
                    {
                        Console.WriteLine($"Record ID: {record.RecordID}, Description: {record.Description}, Amount: {record.Amount}, Type: {record.RecordType}");
                    }
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message); 
            }

        }

        public void GetFinancialRecordsForDate(DateTime recordDate)
        {
            try
            {
                if (recordDate > DateTime.Now)
                {
                    throw new FinancialRecordException($"Error: The record date {recordDate:yyyy-MM-dd} cannot be in the future.");
                }
                List<FinancialRecord> financialRecords = _financialRecordRepository.GetFinancialRecordsForDate(recordDate);

                if (financialRecords == null || financialRecords.Count == 0)

                {

                    throw new FinancialRecordException($"No financial records found for Date: {recordDate:yyyy-MM-dd}");

                }
                else
                {
                    Console.WriteLine($"Financial Records for Date: {recordDate:yyyy-MM-dd}");

                    foreach (var record in financialRecords)
                    {
                        Console.WriteLine($"Record ID: {record.RecordID}, Employee ID: {record.EmployeeID}, " +
                                          $"Description: {record.Description}, Amount: {record.Amount}, Type: {record.RecordType}");
                    }
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

    }
}

