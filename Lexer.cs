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

        public void getNextChar()
        {
            curPos++;
            if (curPos >= text.Length) // Rather than erroring with out of bounds, just dump a bunch of EOFs
            {
                curChar = '\0';
                return;
            }
            curChar = text[curPos];
        }

        private char peek()
        {
            return curChar == '\0' ? '\0' : text[curPos + 1];
        }

        private bool isFunc(string value)
        {
            // I'm not 110% sure why this works (I did write it myself), but I don't think it's wrong
            // The idea is that we check if the next character in function is the next character in the value
            // If it's further in, like at index 2 or 3, then we have an error, because that means that eithr
            // A: Other characters are in there
            // B: Wrong order
            // -1 means that we can also skip characters in "function"
            // The variable "found" is used to make sure at least 1 match is found

            string fn = "function";

            int index;
            bool found = false;
            for (int i = 0; i < fn.Length; i++)
            {
                index = value.IndexOf(fn[i]);
                if (index == 0)
                {
                    found = true;
                }
                else if (index == -1)
                {
                    continue;
                }
                else
                {
                    return false;
                }
                value = value.Substring(1);
            }

            return found;
        }

        public Token getNextToken()
        {
            // Basic tokens
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

                    // There's probably a better way to write this, but it's not the fun way
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

            // Keywords, strings, etc. etc.
            if (char.IsLetter(curChar))
            {

                // Getting the full text of token
                string tempTok = curChar.ToString();
                while (char.IsAsciiLetterOrDigit(peek()))
                {
                    getNextChar();
                    tempTok += curChar;
                }

                // As per DreamBerd specification, functions can be declared with any text that is in order and contains letters of "function"
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

                // I'll keep these 3 seperate for now just in case something changes
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

                // Timing for lifetimes
                if (tempTok[tempTok.Length - 1] == 's')
                {
                    return new Token(Token.tokenType.TIME, tempTok);
                }

                // Since a number is an array of digits
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

                getNextChar();
                do
                {
                    tempTok += curChar;
                    getNextChar();
                }
                while (curChar != '"');

                // Character if you didn't gather
                if (tempTok.Length == 1)
                {
                    return new Token(Token.tokenType.CHR, tempTok);
                }

                return new Token(Token.tokenType.STR, tempTok);
            }

            return new Token(Token.tokenType.ILLEGAL, "ILLEGAL");
        }
    }
}
