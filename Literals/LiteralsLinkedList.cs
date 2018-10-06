/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This LiteralsLinkedList.cs file holds the       ***
***               operations performed on the literals linked     ***
***               list.                                           ***
********************************************************************/

using System;
using System.Collections.Generic;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Literals Linked List Class                         ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               perform actions on the literals linked list.    ***
    *********************************************************************/
    class LiteralsLinkedList
    {
        List<LiteralNode> literals = new List<LiteralNode>();
        public List<string> LiteralErrors = new List<string>();

        /********************************************************************
        *** FUNCTION    : Add Function                                    ***
        *** DESCRIPTION : This function adds a node to the literals       ***
        ***               linked list.                                    ***
        *** INPUT ARGS  : LiteralNode node                                ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Add(LiteralNode node)
        {
            literals.Add(node);
        }

        /********************************************************************
        *** FUNCTION    : Search Literals Function                        ***
        *** DESCRIPTION : This function searches each node in the         ***
        ***               literals linked list for a specific string.     ***
        *** INPUT ARGS  : string literalString                            ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns containsLiteral as a bool.***
        *********************************************************************/
        public bool SearchLiterals(string literalString)
        {
            bool containsLiteral = false;

            foreach (LiteralNode literal in literals)
            {
                if (literal.Name == literalString)
                {
                    containsLiteral = true;
                }
            }

            return containsLiteral;
        }

        /********************************************************************
        *** FUNCTION    : Display Function                                ***
        *** DESCRIPTION : This function displays the literals linked list ***
        ***               in a table format.                              ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Display()
        {
            int counter = 0;

            Console.Clear();
            Console.WriteLine("===============================================================================");
            Console.WriteLine("                              Literals Table                                   ");
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine(string.Format("{0, 0} {1, -20} | {2, -18} | {3, -13} | {4, -15} |", "|", "Name", "Value", "Length", "Address"));
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            foreach (LiteralNode literal in literals)
            {

                Console.WriteLine(string.Format("{0, 0} {1, -20} | {2, -18} | {3, -13} | {4, -15} |", "|", literal.Name, literal.Value.ToString(), literal.Length.ToString(), counter.ToString()));
                counter++;
                if (counter == 20)
                {
                    counter = 0;
                    Console.WriteLine("-- Press enter to DISPLAY MORE LINES --");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("--- Press Enter to quit and exit the program---");
        }

        /********************************************************************
        *** FUNCTION    : Display Errors Function                         ***
        *** DESCRIPTION : This function displays the literal errors list. ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void DisplayErrors()
        {
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("                                  List of Literal Errors                         ");
            Console.WriteLine("==========================================================================================\n");
            foreach (string error in LiteralErrors)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("\n==========================================================================================");
            Console.WriteLine("--- Press Enter to view the Literals Table ---");
        }
    }
}
