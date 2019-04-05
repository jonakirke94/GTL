using GTL.Domain.Exceptions;
using GTL.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                // VALIDATE CORRECT ISBN FORMAT
                ISBNObject.Number = ISBN;
            }
            catch (Exception ex)
            {
                throw new ISBNInvalidException(ISBN, ex);
            }

            return ISBNObject;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
        }
    }
}
