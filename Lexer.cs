using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Lexer
    {
        private string text;
        private int curPos;
        private char curChar;

        public Lexer()
        {

        }

        public void giveCmd(string cmd)
        {
            text = cmd;
            curPos = 0;
            curChar = text[0];
        }

        private void getNextChar()
        {
            curPos++;
            if (curPos == text.Length)
            {
                curChar = '\0';
                return;
            }
            curChar = text[curPos];
        }

        private char peek()
        {
            if (curChar == '\0')
            {
                return '\0';
            }
            return text[curPos + 1];
        }

        private bool isFunc(string value)
        {
            string fn = "function";
            int i = 0;
            int j = 0;
            while (i < fn.Length && j < value.Length)
            {
                while (value[j] == fn[i])
                {
                    i++;
                    if (i == fn.Length)
                    {
                        return false;
                    }
                }
                i++;
                j++;
            }
            return true;
        }

        public Token getNextToken()
        {
            switch (curChar)
            {
                case '\0':
                    return new Token(Token.tokenType.EOF, curChar.ToString());
                case '\n':
                    return new Token(Token.tokenType.NEWLINE, curChar.ToString());
                case ' ':
                    return new Token(Token.tokenType.SPACE, curChar.ToString());
                case '{':
                    return new Token(Token.tokenType.LSQUIRLY, curChar.ToString());
                case '}':
                    return new Token(Token.tokenType.RSQUIRLY, curChar.ToString());
                case '(':
                    return new Token(Token.tokenType.LPAREN, curChar.ToString());
                case ')':
                    return new Token(Token.tokenType.RPAREN, curChar.ToString());
                case '[':
                    return new Token(Token.tokenType.LBLOCK, curChar.ToString());
                case ']':
                    return new Token(Token.tokenType.RBLOCK, curChar.ToString());
                case '<':
                    return new Token(Token.tokenType.LANGLE, curChar.ToString());
                case '>':
                    return new Token(Token.tokenType.RANGLE, curChar.ToString());
                case '?':
                    return new Token(Token.tokenType.DEBUG, curChar.ToString());
                case '!':
                    return new Token(Token.tokenType.ENDLINE, curChar.ToString());
                case '¡':
                    return new Token(Token.tokenType.INV_ENDLINE, curChar.ToString());
                case ',':
                    return new Token(Token.tokenType.SEPERATOR, curChar.ToString());
                case ':':
                    return new Token(Token.tokenType.COLON, curChar.ToString());
                case ';':
                    return new Token(Token.tokenType.NOT, curChar.ToString());
                case '+':
                    if (peek() == '+')
                    {
                        getNextChar();
                        return new Token(Token.tokenType.INC, "++");
                    }
                    return new Token(Token.tokenType.ADD, curChar.ToString());
                case '-':
                    if (peek() == '-')
                    {
                        getNextChar();
                        return new Token(Token.tokenType.DINC, "--");
                    }
                    return new Token(Token.tokenType.SUB, curChar.ToString());
                case '*':
                    return new Token(Token.tokenType.MUL, curChar.ToString());
                case '/':
                    return new Token(Token.tokenType.DIV, curChar.ToString());
                case '=':
                    if (peek() == '=')
                    {
                        getNextChar();
                        if (peek() == '=')
                        {
                            getNextChar();
                            if (peek() == '=')
                            {
                                getNextChar();
                                return new Token(Token.tokenType.STRICTER_EQ, "====");
                            }
                            return new Token(Token.tokenType.STRICT_EQ, "===");
                        }
                        return new Token(Token.tokenType.EQ, "==");
                    }
                    else if (peek() == '>')
                    {
                        return new Token(Token.tokenType.FN_DELIM, "=>");
                    }
                    return new Token(Token.tokenType.INDECISIVE, curChar.ToString());

            }

            if (char.IsLetter(curChar))
            {
                string tempTok = curChar.ToString();
                while (char.IsAsciiLetterOrDigit(peek()))
                {
                    getNextChar();
                    tempTok += curChar;
                }

                if (isFunc(tempTok))
                {
                    return new Token(Token.tokenType.FUNCTION, tempTok);
                }

                int isKeyword = Token.isKeyWord(tempTok);
                if (isKeyword != -1)
                {
                    return new Token((Token.tokenType)isKeyword, tempTok);
                }

                if (Token.isValidAnnotation(tempTok))
                {
                    return new Token(Token.tokenType.ANNOT, tempTok);
                }

                if (tempTok == "print")
                {
                    return new Token(Token.tokenType.PRINT, tempTok);
                }

                if (tempTok == "Infinity")
                {
                    return new Token(Token.tokenType.INFINITY, tempTok);
                }

                if (tempTok == "True")
                {
                    return new Token(Token.tokenType.BOOL, tempTok);
                }

                if (tempTok == "False")
                {
                    return new Token(Token.tokenType.BOOL, tempTok);
                }

                if (tempTok == "Maybe")
                {
                    return new Token(Token.tokenType.BOOL, tempTok);
                }

                return new Token(Token.tokenType.IDENT, tempTok);
            }

            if (char.IsDigit(curChar))
            {
                string tempTok = curChar.ToString();
                while (char.IsDigit(peek()) || peek() == '.')
                {
                    getNextChar();
                    tempTok += curChar;
                }

                if (tempTok[tempTok.Length - 1] == 's')
                {
                    return new Token(Token.tokenType.TIME, tempTok);
                }

                if (tempTok.Length == 1)
                {
                    return new Token(Token.tokenType.DGT, tempTok);
                }

                if (tempTok.Contains("."))
                {
                    return new Token(Token.tokenType.FLOAT, tempTok);
                }

                return new Token(Token.tokenType.NUM, tempTok);
            }

            if (curChar == '"')
            {
                string tempTok = "";
                while (peek() != '"')
                {
                    getNextChar();
                }

                getNextChar();

                if (tempTok.Length == 1)
                {
                    return new Token(Token.tokenType.CHR, tempTok);
                }

                return new Token(Token.tokenType.STR, tempTok);
            }

            getNextChar();

            return new Token(Token.tokenType.ILLEGAL, "ILLEGAL");
        }
    }
}
