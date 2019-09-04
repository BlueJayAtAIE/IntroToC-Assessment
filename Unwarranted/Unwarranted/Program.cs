using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Unwarranted
{
    class Program
    {
        static void Main(string[] args)
        {
            Tools tl = new Tools();
            //WARNING: TESTING GARBAGE AHEAD
            while (Tools.gameOn)
            {
                tl.AdvanceTime();
                Console.ReadKey();
            }
        }
    }
}
