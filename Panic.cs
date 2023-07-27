using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Panic
    {
        private string message;

        public Panic(string msg)
        {
            message = msg;
        }

        public void raise()
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
