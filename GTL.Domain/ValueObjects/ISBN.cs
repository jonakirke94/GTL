using GTL.Domain.Exceptions;
using GTL.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GTL.Domain.ValueObjects
{
    public class ISBN : ValueObject
    {
        public string Number { get; set; }

        public static ISBN For(string ISBN)
        {
            var ISBNObject = new ISBN();

            try
            {
                // ISBN numbers are either 10 or 13 digits. The format can be validated based on the last digit which is a "check-digit":  https://en.wikipedia.org/wiki/International_Standard_Book_Number
                var isValid = false;

                if (string.IsNullOrEmpty(ISBN))
                    throw new ArgumentNullException();

                var formatted = ISBN.Replace("-", "").Replace(" ", "");

                switch (ISBN.Length)
                {
                    case 10:
                        isValid = IsValid10(formatted);
                        break;
                    case 13:
                        isValid = IsValid13(formatted);
                        break;                    
                }

                if (!isValid)
                    throw  new ArgumentException();

                ISBNObject.Number = ISBN;
            }
            catch (Exception ex)
            {
                throw new ISBNInvalidException(ISBN, ex);
            }

            return ISBNObject;
        }

        private static bool IsValid10(string ISBN)
        {
            var reg = new Regex(@"^\d{9}[\d, X]{1}$");
            var sum = 0;

            if (reg.IsMatch(ISBN))
            {
                

                for (var i = 0; i < 9; i++)
                {
                    sum += ToInt(ISBN[i]) * (i + 1);
                }

                sum += (ISBN[9] == 'X' ? 10 : ToInt(ISBN[9])) * 10;
            }


            return sum % 11 == 0;
        }


        private static bool IsValid13(string ISBN)
        {
            var reg = new Regex(@"^\d{13}$");
            var sum = 0;

            if (reg.IsMatch(ISBN))
            {            
                var index = 0;
                sum = ISBN.Sum(c => ToInt(c) * (IsOdd(index++) ? 3 : 1));
            }


            return sum % 10 == 0;
        }

        private static int ToInt(char c)
        {
            return (int)(c - '0');
        }

        private static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
        }
    }
}

