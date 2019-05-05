using System.Collections;
using System.Collections.Generic;
using GTL.Domain.Enums;

namespace Application.Tests
{
    /// <summary>
    /// Equivalence class 1 for TDS-5, used for US-17
    /// </summary>
    public class TDS5_EC1 : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "0805000763", "Titel", "Beskrivelse", 1, MaterialType.Book };
            yield return new object[] { "9780261102385", "Titel", "Beskrivelse", 5, MaterialType.Map };
            yield return new object[] { "9788740046526", "Titel", "Beskrivelse", int.MaxValue, MaterialType.ReferenceBook };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
