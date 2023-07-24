using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Panic
    {
        public Panic(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
