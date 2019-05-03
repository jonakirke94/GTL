using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Application.Tests.US_17
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
            yield return new object[] { null, "Titel", "Beskrivelse", int.MaxValue };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
