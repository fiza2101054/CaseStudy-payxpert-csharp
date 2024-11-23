using Payxpert.Model;
using Payxpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Repository
{
    internal class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly string _connectionString;
        private SqlCommand _cmd;

        public FinancialRecordRepository()
        {
            // Initialize the connection string using a utility class
            _connectionString = DBConnUtil.GetConnectionString();
            _cmd = new SqlCommand();
        }

        // Add Financial Record with void return type
        public void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO FinancialRecord 
                                     (EmployeeId, Description, Amount, RecordType, RecordDate) 
                                     VALUES 
                                     (@EmployeeId, @Description, @Amount, @RecordType, @RecordDate); SELECT SCOPE_IDENTITY();";

                    _cmd.Connection = conn;
                    _cmd.CommandText = query;

                    
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    _cmd.Parameters.AddWithValue("@Description", description);
                    _cmd.Parameters.AddWithValue("@Amount", amount);
                    _cmd.Parameters.AddWithValue("@RecordType", recordType);
                    _cmd.Parameters.AddWithValue("@RecordDate", DateTime.Now);

                    conn.Open();
                    var result = _cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int newRecordId = Convert.ToInt32(result);
                        Console.WriteLine($"Financial record added successfully with RecordID: {newRecordId}");
                    }
                    else
                    {
                        Console.WriteLine("No ID was returned; the record may not have been added.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                  
                    throw;
                }
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            FinancialRecord financialRecord = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM FinancialRecord WHERE RecordID = @RecordID";
                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@RecordID", recordId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        financialRecord = ExtractFinancialRecord(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }

            return financialRecord;
        }
        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> financialRecords = new List<FinancialRecord>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM FinancialRecord WHERE EmployeeID = @EmployeeID";
                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var financialRecord = ExtractFinancialRecord(reader);
                        financialRecords.Add(financialRecord);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }

            return financialRecords;
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> financialRecords = new List<FinancialRecord>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM FinancialRecord WHERE CAST(RecordDate AS DATE) = @RecordDate";
                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@RecordDate", recordDate.ToString("yyyy-MM-dd"));
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var financialRecord = ExtractFinancialRecord(reader);
                        financialRecords.Add(financialRecord);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }

            return financialRecords;
        }

    
        private FinancialRecord ExtractFinancialRecord(SqlDataReader reader)
        {
            return new FinancialRecord
            {
                RecordID = (int)reader["RecordID"],
                EmployeeID = (int)reader["EmployeeID"],
                RecordDate = (DateTime)reader["RecordDate"],
                Description = (string)reader["Description"],
                Amount = (decimal)reader["Amount"],
                RecordType = (string)reader["RecordType"]
            };
        }
    }
}
