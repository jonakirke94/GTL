using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Application.Tests
{
    /// <summary>
    /// Equivalence class 2 for TDS-5, used for US-17
    /// </summary>
    public class TDS5_EC2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "012345678", "Titel", "Beskrivelse", 0 }; // 9 characters in ISBN
            yield return new object[] { "01234567890", "Titel", "Beskrivelse", 0 }; // 11 characters in ISBN
            yield return new object[] { "012345678901", "Titel", "Beskrivelse", 0 }; // 12 characters in ISBN
            yield return new object[] { "01234567890123", "Titel", "Beskrivelse", 0 }; // 14 characters in ISBN
            yield return new object[] { "0123456789", null, "Beskrivelse", 0 }; // Title is null
            yield return new object[] { "0123456789", string.Empty, "Beskrivelse", 0 }; // Title is empty
            yield return new object[] { "0123456789", string.Concat(Enumerable.Repeat("a", 61)), "Beskrivelse", 0 }; // Title is empty
            yield return new object[] { "012345678", "Titel", null, 0 }; // Description is null
            yield return new object[] { "012345678", "Titel", string.Empty, 0 }; // Description is empty
            yield return new object[] { "012345678", "Titel", string.Empty, unchecked(int.MinValue - 1) }; // Edition overflow
            yield return new object[] { "012345678", "Titel", string.Empty, unchecked(int.MaxValue + 1) }; // Edition overflow
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
