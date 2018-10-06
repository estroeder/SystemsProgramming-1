/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ProcessHexidecimalLiteral.cs file          ***
***               processes a hexidecimal literal.                ***
********************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Process Hexidecimal Literal Class                  ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               process a hexidecimal literal.                  ***
    *********************************************************************/
    class ProcessHexidecimalLiteral
    {
        /********************************************************************
        *** FUNCTION    : Process Function                                ***
        *** DESCRIPTION : This function processes a hexidecimal literal   ***
        ***               and adds it to either the literal errors list   ***
        ***               or to the list of valid literals.               ***
        *** INPUT ARGS  : LiteralsLinkedList literalsLinkedList,          ***
        ***               string literalToBeValidated, string literal,    ***
        ***               int address, List<string> processedLiterals     ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Process(LiteralsLinkedList literalsLinkedList, string literalToBeValidated, string literal, int address, List<string> processedLiterals)
        {
            string final = "";
            Regex rg = new Regex("[0-9A-Fa-f]+");

            if (rg.IsMatch(literalToBeValidated))
            {
                if (literalToBeValidated[0] <= '7' && literalToBeValidated[0] >= '0')
                {
                    if (literalToBeValidated.Length % 2 != 0)
                    {
                        final = "0";
                        foreach (char ch in literalToBeValidated)
                        {
                            final += ch;
                        }
                    }
                    else
                    {
                        final = literalToBeValidated;
                    }
                }
                else if (literalToBeValidated[0] == '8' || literalToBeValidated[0] == '9' || literalToBeValidated[0] >= 'A' || literalToBeValidated[0] <= 'B' || literalToBeValidated[0] == 'C' || literalToBeValidated[0] == 'D' || literalToBeValidated[0] == 'E' || literalToBeValidated[0] == 'F')
                {
                    if (literalToBeValidated.Length % 2 != 0)
                    {
                        final = "F";
                        foreach (char ch in literalToBeValidated)
                        {
                            final += ch;
                        }
                    }
                    else
                    {
                        final = literalToBeValidated;
                    }
                }
                else
                {
                    literalsLinkedList.LiteralErrors.Add("Error: " + literal + " may only contain hexadecimal characters (0 - F)");
                }
                if (!processedLiterals.Contains(literalToBeValidated))
                {
                    processedLiterals.Add(literalToBeValidated);
                    LiteralNode node = new LiteralNode(literal, final, (final.Count() / 2), address);
                    literalsLinkedList.Add(node);
                    address++;
                }
                else
                {
                    literalsLinkedList.LiteralErrors.Add("Error: Literal " + literal + " is a duplicate literal");
                }
            }
            else
            {
                literalsLinkedList.LiteralErrors.Add("Error: " + literal + " may only contain hexadecimal characters (0 - F)");
            }
        }
    }
}
