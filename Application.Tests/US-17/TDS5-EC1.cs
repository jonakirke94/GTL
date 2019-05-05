using System.Collections;
using System.Collections.Generic;

namespace Application.Tests
{
    /// <summary>
    /// Equivalence class 1 for TDS-5, used for US-17
    /// </summary>
    public class TDS5_EC1 : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "0123456789", "Titel", "Beskrivelse", 0 };
            yield return new object[] { "0123456789012", "Titel", "Beskrivelse", int.MinValue };
            yield return new object[] { "9788740046526", "Titel", "Beskrivelse", int.MaxValue };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
