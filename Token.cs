using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBerdInterp
{
    internal class Token
    {
        public tokenType type;
        public string value;

        public Token(tokenType inType, string inValue)
        {
            type = inType;
            value = inValue;
        }

        public static int isKeyWord(string value)
        {
            if (keyWords.ContainsKey(value))
            {
                return keyWords[value];
            }
            return -1; // Not a keyword
        }

        public static bool isValidAnnotation(string value)
        {
            if (annotations.Contains(value))
            {
                return true;
            }

            // Make sure there's enough room for []
            if (value.Length < 3)
            {
                return false;
            }

            // Check if it's an array of type
            if (value.Substring(value.Length - 3, 2) == "[]")
            {
                // Strip off the "[]" and check the remaining part
                value = value.Substring(0, value.Length - 2);
                return isValidAnnotation(value);
            }

            return false;
        }

        // For checking if a string is a keyword and getting the relevant integer value
        // Integers were used instead of the tokenType so that -1 can be returned as false
        private static Dictionary<string, int> keyWords = new Dictionary<string, int>
        {
            { "if", 30 },
            { "const", 31 },
            { "var", 32 },
            { "when", 33 },
            { "return", 35 },
            { "prev", 36 },
            { "next", 37 },
            { "async", 38 },
            { "await", 39 },
            { "after", 40 },
            { "class", 41 },
            { "new", 42 },
            { "delete", 43 },
            { "reverse", 44 },
            { "use", 45 },
        };

        // For checking whether a string could be a type annotation
        private static string[] annotations = new string[]
        {
            "Int",
            "String",
            "Char",
            "Digit",
            "Int9",
            "Int99",
            "Bool",
        };

        public enum tokenType
        {
            // Whitespace
            EOF = 0,
            ILLEGAL = 1,
            NEWLINE = 2,
            INDENT = 4,//
            SPACE = 5,

            // Brackets
            LSQUIRLY = 10,
            RSQUIRLY = 11,
            LPAREN = 12,
            RPAREN = 13,
            LBLOCK = 14,
            RBLOCK = 15,
            LANGLE = 16,
            RANGLE = 17,

            // Other
            DEBUG = 20,
            IDENT = 21,
            SEPERATOR = 22,
            ENDLINE = 23,
            INV_ENDLINE = 24,
            FN_DELIM = 25,
            COLON = 26,
            ANNOT = 27,
            INDECISIVE = 28,

            // Keywords
            IF = 30,
            CONST = 31,
            VAR = 32,
            WHEN = 33,
            FUNCTION = 34,
            RETURN = 35,
            PREV = 36,
            NEXT = 37,
            ASYNC = 38,
            AWAIT = 39,
            AFTER = 40,
            CLS = 41,
            NEW = 42,
            DEL = 43,
            REV = 44,
            USE = 45,

            // In-Built Funcs
            PRINT = 50,

            // Bool Operators
            NOT = 60,
            LOOSE_EQ = 61,
            EQ = 62,
            STRICT_EQ = 63,
            STRICTER_EQ = 64,

            // Mathematical Operators
            ADD = 70,
            SUB = 71,
            MUL = 72,
            DIV = 73,

            // Other Operators
            ASSIGN = 80,
            INC = 81,
            DINC = 82,

            // Types
            BOOL = 120,
            NUM = 121,
            STR = 122,
            FLOAT = 123,
            TIME = 124,
            INFINITY = 125,
            CHR = 126,
            DGT = 127,
        }
    }
}
