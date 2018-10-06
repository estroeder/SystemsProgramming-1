/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ValidateSymbolTable.cs file validates the  ***
***               symbols from the SYMS.dat file.                 ***
********************************************************************/

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Validate Symbol Class                              ***
    *** DESCRIPTION : This class contains all the methods needed to   ***
    ***               validate a symbol from the SYMS.dat file.       ***
    *********************************************************************/
    class ValidateSymbolTable
    {
        /********************************************************************
        *** FUNCTION    : Validate Data File Line Function                ***
        *** DESCRIPTION : This function stores a validated symbol and a   ***
        ***               validated value and a validated Rflag into a    ***
        ***               node if they meet the validation requirements.  ***
        *** INPUT ARGS  : strings[] parsedDataFileLine                    ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns a Node value.             ***
        *********************************************************************/
        public Node ValidateDataFileLine(string[] parsedDataFileLine)
        {
            Node node = new Node();
            string symbol;
            int? value;
            bool? rFlag;

            symbol = ValidateSymbol(parsedDataFileLine[0]);
            value = ValidateValue(parsedDataFileLine[0], parsedDataFileLine[1]);
            rFlag = ValidateRFlag(parsedDataFileLine[0], parsedDataFileLine[2]);

            // Check if the node is valid
            if (symbol != string.Empty && value != null && rFlag != null)
            {
                node.Symbol = symbol;
                node.Value = (int)value;
                node.Rflag = (bool)rFlag;
            }
            return node;
        }

        /********************************************************************
        *** FUNCTION    : Validate Symbol Function                        ***
        *** DESCRIPTION : This function validates a symbol using the      ***
        ***               given symbol requirements and returns the       ***
        ***               symbol if it is valid or and empty string if    ***
        ***               it is invalid.                                  ***
        *** INPUT ARGS  : string symbolValue                              ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns a string value.           ***
        *********************************************************************/
        private string ValidateSymbol(string symbolValue)
        {
            string validatedSymbol = string.Empty;
            bool isValidSymbol = true;
            Regex regex = new Regex(@"^[a-zA-Z0-9_]*$");

            if (symbolValue.Length > 10)
            {
                Console.WriteLine("The symbol: '" + symbolValue + "' exceeds the maximum character limit of 10 characters.");
                isValidSymbol = false;
            }
            if (isValidSymbol == true && Char.IsLetter(symbolValue[0]) == false)
            {
                if (symbolValue[0] == '$')
                {
                    Console.WriteLine("The symbol: '" + symbolValue + "' begins with a $ instead of a letter (a-z or A-Z).");
                }
                else if (symbolValue[0] == '#')
                {
                    Console.WriteLine("The symbol: '" + symbolValue + "' begins with a # instead of a letter (a-z or A-Z).");
                }
                else
                {
                    Console.WriteLine("The symbol: '" + symbolValue + "' does not begin with a letter (a-z or A-Z).");
                    isValidSymbol = false;
                }
            }
            if (isValidSymbol == true && regex.IsMatch(symbolValue) == false)
            {
                Console.WriteLine("The symbol: '" + symbolValue + "' contains a character that is not alphanumeric or an underscore.");
                isValidSymbol = false;
            }

            if (isValidSymbol == true)
            {
                if (symbolValue.Length > 4)
                {
                    validatedSymbol = symbolValue.Substring(0, 4);
                }
                else
                {
                    validatedSymbol = symbolValue;
                }
            }
            return validatedSymbol.ToUpper();
        }

        /********************************************************************
        *** FUNCTION    : Validate Value Function                         ***
        *** DESCRIPTION : This function validates a value using the given ***
        ***               value requirements and returns the value if it  ***
        ***               is valid or a null integer if it is not valid.  ***
        *** INPUT ARGS  : string symbolValue, string valueValue           ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns an int? value.            ***
        *********************************************************************/
        private int? ValidateValue(string symbolValue, string valueValue)
        {
            int? value = null;
            bool isDigit = true;

            if (valueValue[0] == '-' || valueValue[0] == '+')
            {
                string substring = valueValue.Substring(1, valueValue.Length - 1);
                isDigit = substring.All(Char.IsDigit);
            }
            else
            {
                isDigit = valueValue.All(Char.IsDigit);
            }

            if (isDigit == true)
            {
                value = Convert.ToInt32(valueValue);
            }
            else
            {
                Console.WriteLine("The symbol: '" + symbolValue + "' contains an invalid value: '" + valueValue + "' that is not a signed integer.");
            }
            return value;
        }

        /********************************************************************
        *** FUNCTION    : Validate RFlag Function                         ***
        *** DESCRIPTION : This function validates a Rflag using the given ***
        ***               Rflag requirements and returns the Rflag if it  ***
        ***               is valid or a null boolean if it is not valid.  ***
        *** INPUT ARGS  : string symbolValue, string rFlagValue           ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns a bool? value.            ***
        *********************************************************************/
        private bool? ValidateRFlag(string symbolValue, string rFlagValue)
        {
            bool? value = null;
            if (rFlagValue.ToUpper() == "TRUE" || rFlagValue.ToUpper() == "T" || rFlagValue == "1")
            {
                value = true;
            }
            else if (rFlagValue.ToUpper() == "FALSE" || rFlagValue.ToUpper() == "F" || rFlagValue == "0")
            {
                value = false;
            }
            else
            {
                Console.WriteLine("The symbol '" + symbolValue + "' contains an invalid RFlag value '" + rFlagValue + "' that is not a boolean value.");
            }
            return value;
        }
    }
}