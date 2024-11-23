using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Model
{
    public class Employee
    {
        private int _employeeID;
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _gender;
        private string _email;
        private string _phoneNumber;
        private string _address;
        private string _position;
        private DateTime _joiningDate;
        private DateTime? _terminationDate;


        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public DateTime JoiningDate 
        {
            get { return _joiningDate; }
            set { _joiningDate = value; }
        }

        public DateTime? TerminationDate
        {
            get { return _terminationDate; }
            set { _terminationDate = value; }
        }

        public int CalculateAge()
        {
            int age = DateTime.Now.Year - DateOfBirth.Year;
            Console.WriteLine($"The age of this employee is {age} years.");
            return age;
        }

        public override string ToString()
        {
            return $"Employee ID: {EmployeeID}\t" +
                   $"Name: {FirstName} {LastName}\t" +
                   $"Position: {Position}\t" +
                   $"Email: {Email}\t" +
                   $"Age: {CalculateAge()}";
        }
    }
}
