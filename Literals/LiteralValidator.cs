/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This LiteralValidator.cs file validates a       ***
***               literal.                                        ***
********************************************************************/

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Literal Validator Class                            ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               validate a literal.                             ***
    *********************************************************************/
    class LiteralValidator
    {
        List<string> processedLiterals = new List<string>();

        /********************************************************************
        *** FUNCTION    : Validate Function                               ***
        *** DESCRIPTION : This function parses and validates a literal.   ***
        *** INPUT ARGS  : LiteralsLinkedList literalsLinkedList,          ***
        ***               List<string> literals                           ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Validate(LiteralsLinkedList literalsLinkedList, List<string> literals)
        {
            foreach (string literal in literals)
            {
                // Get the literal expression
                string literalToBeValidated = ParseLiteral(literalsLinkedList, literal);

                // Validate literal
                if(literalToBeValidated != "")
                {
                    ValidateLiteral(literalsLinkedList, literalToBeValidated, literal);
                }
            }
        }

        /********************************************************************
        *** FUNCTION    : Parse Literal Function                          ***
        *** DESCRIPTION : This function parses a literal so that it can   ***
        ***               be easily validated.                            ***
        *** INPUT ARGS  : LiteralsLinkedList literalsLinkedList,          ***
        ***               string literal                                  ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns literalToValidate as a    ***
        ***               string.                                         ***
        *********************************************************************/
        private string ParseLiteral(LiteralsLinkedList literalsLinkedList, string literal)
        {
            string literalToValidate = "";

            Match match = Regex.Match(literal, @"'([^']*)");
            if (match.Success)
            {
                literalToValidate = match.Groups[1].Value;
            }
            else
            {
                literalsLinkedList.LiteralErrors.Add("Error: The literal " + literal + " must be separated by single quotations");
            }
            return literalToValidate;
        }

        /********************************************************************
        *** FUNCTION    : Validate Literal Function                       ***
        *** DESCRIPTION : This function validates a literal.              ***
        *** INPUT ARGS  : LiteralsLinkedList literalsLinkedList,          ***
        ***               string literalToBeValidated, string literal     ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        private void ValidateLiteral(LiteralsLinkedList literalsLinkedList, string literalToBeValidated, string literal)
        {
            int hexchar;
            int address = 1;

            hexchar = literal.IndexOf("=");

            if (literal[hexchar + 1] == 'C' || literal[hexchar + 1] == 'c')
            {
                // Process character literal
                ProcessCharacterLiteral processCharacterLiteral = new ProcessCharacterLiteral();
                processCharacterLiteral.Process(literalsLinkedList, literalToBeValidated, literal, address, processedLiterals);
            }
            else if (literal[hexchar + 1] == 'X' || literal[hexchar + 1] == 'x')
            {
                // Process hexidecimal literal
                if (!processedLiterals.Contains(literalToBeValidated.ToUpper()) && !processedLiterals.Contains(literalToBeValidated.ToLower()))
                {
                    ProcessHexidecimalLiteral processHexidecimalLiteral = new ProcessHexidecimalLiteral();
                    processHexidecimalLiteral.Process(literalsLinkedList, literalToBeValidated, literal, address, processedLiterals);
                }
            }
            else
            {
                // Process invalid literal by adding it to literal errors list
                literalsLinkedList.LiteralErrors.Add("Error: The literal '" + literal + "' contains an invalid second character (must be C or X).");
            }
        }
    }
}