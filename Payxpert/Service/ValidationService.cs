using Payxpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Service
{
    public  class ValidationService
    {
        public void ValidateNotEmpty(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new InvalidInputException($"{fieldName} cannot be empty.");
            }
        }

        public void ValidateDate(string input, string fieldName)
        {
            if (!DateTime.TryParse(input, out _))
            {
                throw new InvalidInputException($"Invalid {fieldName}. Please enter a valid date in YYYY-MM-DD format.");
            }
        }

        public void ValidateEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !input.Contains("@") || !input.Contains("."))
            {
                throw new InvalidInputException("Invalid email format. An email must contain '@' and '.'");
            }
        }

        public void ValidatePhoneNumber(string input)
        {
            if (input.Length != 10 || !IsDigitsOnly(input))
            {
                throw new InvalidInputException("Phone number must be exactly 10 digits and contain only numbers.");
            }
        }

        public void ValidateDecimal(string input, string fieldName)
        {
            if (!decimal.TryParse(input, out decimal value) || value <= 0)
            {
                throw new InvalidInputException($"Invalid {fieldName}. Please enter a positive number.");
            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

