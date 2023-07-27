using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Variable
    {
        public Mutability mut;
        public dynamic value;

        public Variable(dynamic value, Mutability mut)
        {
            this.value = value;
            this.mut = mut;
        }

        public enum Mutability
        {
            None,
            Reassign,
            Properties,
            Full,
        }
    }
}
