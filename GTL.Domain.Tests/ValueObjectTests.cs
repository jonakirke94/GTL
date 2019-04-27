using GTL.Domain.Exceptions;
using GTL.Domain.ValueObjects;
using Xunit;

namespace GTL.Domain.Tests
{
    public class ValueObjectTests
    {
        [Theory] // data-driven test
        [InlineData("99921-58-10-7")]
        [InlineData("9971-5-0210-0")]
        [InlineData("960-425-059-0")]
        [InlineData("80-902734-1-6")]
        [InlineData("1-84356-028-3")]
        public void Valid10Digit(string isbn)
        {
            var isbnObj = ISBN.For(isbn);

            Assert.Equal(isbn, isbnObj.Number);
        }

        [Theory]
        [InlineData("9780399501487")]
        [InlineData("9780395489321")]
        [InlineData("9780345538376")]
        [InlineData("9780545935173")]
        public void Valid13Digit(string isbn)
        {
            var isbnObj = ISBN.For(isbn);

            Assert.Equal(isbn, isbnObj.Number);
        }

        [Theory]
        [InlineData("978039950")] //9
        [InlineData("97803954893")] //11
        [InlineData("978034553837")] //12
        [InlineData("97805459351731")] //14
        [InlineData("A05459351731")]
        [InlineData("ABCDEFGHHIJ")]
        public void InvalidInput(string isbn)
        {
            Assert.Throws<ISBNInvalidException>(() => ISBN.For(isbn));
        }
    }
}
