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
        private static int index = 0;

        private static Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        public static void takeToken(Token token)
        {
            tokens.Add(token);
        }

        public static void clearTokens()
        {
            index = 0;
            tokens.Clear();
        }

        public static void start()
        {
            Panic err = statement(tokens);
            if (err != null)
            {
                err.raise();
                return;
            }
        }

        private static Panic statement(List<Token> tTokens)
        {
            string key;

            switch (tTokens[index].type)
            {
                case Token.tokenType.PRINT:
                    index += 2;
                    switch (tTokens[index].type)
                    {
                        case Token.tokenType.IDENT:
                            Console.WriteLine(variables[tTokens[index].value].value);
                            break;
                        default:
                            Console.WriteLine(tTokens[index].value);
                            break;
                    }
                    
                    break;
                case Token.tokenType.VAR:
                    index++;
                    switch (tTokens[1].type)
                    {
                        case Token.tokenType.VAR:
                            index++;
                            if (tTokens[index].type == Token.tokenType.COLON)
                            {
                                index += 2;
                            }

                            key = tTokens[index].value;

                            index += 2;

                            dynamic output;
                            dynamic term;
                            Token.tokenType operatorType;

                            if (tTokens[index].type == Token.tokenType.NUM)
                            {
                                output = Convert.ToInt32(tTokens[index].value);
                            }
                            else if (tTokens[index].type == Token.tokenType.STR)
                            {
                                output = tTokens[index].value;
                            }
                            else
                            {
                                return new Panic("Either not implemented or wrong.");
                            }

                            index++;

                            while (!(tTokens[index].type == Token.tokenType.ENDLINE || tTokens[index].type == Token.tokenType.INV_ENDLINE))
                            {
                                operatorType = tTokens[index].type;
                                index++;
                                if (tTokens[index].type == Token.tokenType.NUM)
                                {
                                    term = Convert.ToInt32(tTokens[index].value);
                                }
                                else if (tTokens[index].type == Token.tokenType.STR)
                                {
                                    term = tTokens[index].value;
                                }
                                else
                                {
                                    return new Panic("Either not implemented or wrong.");
                                }

                                switch (operatorType)
                                {
                                    case Token.tokenType.ADD:
                                        output += term;
                                        break;
                                    case Token.tokenType.SUB:
                                        output -= term;
                                        break;
                                    case Token.tokenType.MUL:
                                        output *= term;
                                        break;
                                    case Token.tokenType.DIV:
                                        output /= term;
                                        break;
                                }
                                index++;
                            }

                            variables.Add(key, new Variable(output, Variable.Mutability.Full));

                            index += 2;

                            break;
                        case Token.tokenType.CONST:
                            break;
                    }

                    break;
            }

            return null;
        }
    }
}
