﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * Look, don't ask why I started doing Go-like error checking, it just felt right
 * 
 */


namespace DreamBerdInterp
{
    internal class Parser
    {
        private Lexer lexer;
        private Token curToken;
        private Token peekToken;

        public Parser()
        {
            lexer = new Lexer();
        }

        public void runCmd(string cmd)
        {
            lexer.giveCmd(cmd);

            curToken = lexer.getNextToken();
            lexer.getNextChar();// Look, I had to do this here because of the way I did lexer.getNextToken(), sorry
            peekToken = lexer.getNextToken();
            lexer.getNextChar();

            Panic err = block();
            if (err == null)
            {
                Logic.start();
            }
        }

        public void getNextToken()
        {
            Logic.takeToken(curToken);
            curToken = peekToken;
            peekToken = lexer.getNextToken();
            lexer.getNextChar();
        }

        public Panic block()
        {
            Panic err;
            while (curToken.type != Token.tokenType.EOF)
            {
                err = statement();
                if (err != null) // Oh my gosh Go
                {
                    return err;
                }
                getNextToken();
            }
            return null;
        }

        public Panic statement()
        {
            Panic err;

            switch (curToken.type)
            {
                case Token.tokenType.IF:
                    break;
                case Token.tokenType.CONST:
                    break;
                case Token.tokenType.VAR:
                    break;
                case Token.tokenType.WHEN:
                    break;
                case Token.tokenType.ASYNC:
                    break;
                case Token.tokenType.FUNCTION:
                    break;
                case Token.tokenType.RETURN:
                    break;
                case Token.tokenType.AFTER:
                    break;
                case Token.tokenType.CLS:
                    break;
                case Token.tokenType.DEL:
                    break;
                case Token.tokenType.REV:
                    break;
                case Token.tokenType.PRINT: // print

                    getNextToken();
                    if (curToken.type != Token.tokenType.LPAREN) // print(
                    {
                        return new Panic("Invalid syntax.");
                    }

                    getNextToken();
                    if (curToken.type != Token.tokenType.IDENT)
                    {
                        err = type(); // print("Hello, World"
                        if (err != null)
                        {
                            return err;
                        }
                    }

                    getNextToken();
                    if (curToken.type != Token.tokenType.RPAREN) // print("Hello, World")
                    {
                        return new Panic("Invalid syntax.");
                    }

                    getNextToken();
                    err = el(); // print("Hello, World")!!!!
                    if (err != null)
                    {
                        return err;
                    }

                    break;
                default:
                    return new Panic("Invalid syntax.");
            }

            return null;
        }

        public Panic expression()
        {
            return null;
        }

        public Panic comparison()
        {
            return null;
        }

        public Panic eq()
        {
            return null;
        }

        public Panic type()
        {
            switch (curToken.type)
            {
                case Token.tokenType.BOOL:
                    break;
                case Token.tokenType.NUM:
                    break;
                case Token.tokenType.STR:
                    break;
                case Token.tokenType.FLOAT:
                    break;
                case Token.tokenType.TIME:
                    break;
                case Token.tokenType.INFINITY:
                    break;
                case Token.tokenType.CHR:
                    break;
                case Token.tokenType.DGT:
                    break;
                default:
                    return new Panic("Invalid syntax."); // Currently I think every error is invalid syntax, it is what it is
            }
            return null;
        }

        public Panic el()
        {
            // Make sure there's that first one
            if (curToken.type == Token.tokenType.ENDLINE || curToken.type == Token.tokenType.INV_ENDLINE)
            {
                getNextToken();
            }
            else return new Panic("Invalid syntax.");

            // Any extras
            while (peekToken.type == Token.tokenType.ENDLINE || peekToken.type == Token.tokenType.INV_ENDLINE)
            {
                getNextToken();
            }

            return null;
        }
    }
}
