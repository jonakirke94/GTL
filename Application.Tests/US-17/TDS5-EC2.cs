using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GTL.Domain.Enums;

namespace Application.Tests
{
    /// <summary>
    /// Equivalence class 2 for TDS-5, used for US-17
    /// </summary>
    public class TDS5_EC2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "012345678", "Titel", "Beskrivelse", 1, MaterialType.Book }; // 9 characters in ISBN
            yield return new object[] { "01234567890", "Titel", "Beskrivelse", 1, MaterialType.Book }; // 11 characters in ISBN
            yield return new object[] { "012345678901", "Titel", "Beskrivelse", 1, MaterialType.Book }; // 12 characters in ISBN
            yield return new object[] { "01234567890123", "Titel", "Beskrivelse", 1, MaterialType.Book }; // 14 characters in ISBN
            yield return new object[] { "9780395489321", null, "Beskrivelse", 1, MaterialType.Book }; // Title is null
            yield return new object[] { "9780395489321", string.Empty, "Beskrivelse", 1, MaterialType.Book }; // Title is empty
            yield return new object[] { "9780395489321", string.Concat(Enumerable.Repeat("a", 61)), "Beskrivelse", 1, MaterialType.Book }; // Title is empty
            yield return new object[] { "9780395489321", "Titel", null, 1, MaterialType.Book }; // Description is null
            yield return new object[] { "9780395489321", "Titel", string.Empty, 1, MaterialType.Book }; // Description is empty
            yield return new object[] { "9780395489321", "Titel", string.Empty, 0, MaterialType.Book }; // Edition too low
            yield return new object[] { "9780395489321", "Titel", string.Empty, unchecked(int.MaxValue + 1), MaterialType.Book }; // Edition overflow
            yield return new object[] { "9780395489321", "Titel", string.Empty, 1, null }; // Edition overflow
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
