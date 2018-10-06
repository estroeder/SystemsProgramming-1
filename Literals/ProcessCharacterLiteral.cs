/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ProcessCharacterLiteral.cs file            ***
***               processes a character literal.                  ***
********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Process Character Literal Class                    ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               process a character literal.                    ***
    *********************************************************************/
    class ProcessCharacterLiteral
    {
        /********************************************************************
        *** FUNCTION    : Process Function                                ***
        *** DESCRIPTION : This function processes a character literal     ***
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

            foreach (char character in literalToBeValidated)
            {
                int value = Convert.ToInt32(character);
                string hexOutput = String.Format("{0:X}", value);

                final += hexOutput;
            }
            if (!processedLiterals.Contains(literalToBeValidated))
            {
                processedLiterals.Add(literalToBeValidated);
                LiteralNode node = new LiteralNode(literal, final, literalToBeValidated.Count(), address);
                literalsLinkedList.Add(node);
                address++;
            }
        }
    }
}
