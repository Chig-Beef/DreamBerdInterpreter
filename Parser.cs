using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Parser
    {
        private Lexer lexer;

        public Parser()
        {
            lexer = new Lexer();
        }

        public void runCmd(string cmd)
        {
            Token tok = lexer.getNextToken();
            while (tok.type != Token.tokenType.EOF)
            {
                tok = lexer.getNextToken();
            }
        }
    }
}
