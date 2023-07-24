using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Logic
    {
        private static List<Token> tokens = new List<Token>();

        private static Dictionary<string, int> variables;

        public static void takeToken(Token token)
        {
            tokens.Add(token);
        }

        public static void clearTokens()
        {
            tokens.Clear();
        }

        public static void start()
        {
            Panic err = execute(tokens);
            if (err != null)
            {
                return;
            }
        }

        private static Panic execute(List<Token> tTokens)
        {
            switch (tTokens[0].type)
            {
                case Token.tokenType.PRINT:
                    Console.WriteLine(tTokens[2].value);
                    break;
            }

            return null;
        }
    }
}
