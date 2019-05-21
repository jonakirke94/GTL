using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GTL.EndToEndTests
{
    public static class ThreadHelpers
    {

        public static void Wait()
        {
            Wait(2000);
        }

        public static void Wait(int time)
        {
            try
            {
                Thread.Sleep(time);
            }
            catch (ThreadInterruptedException)
            {
            }
        }

    }
}
