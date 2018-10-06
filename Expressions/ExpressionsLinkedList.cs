/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ExpressionsLinkedList.cs file holds the    ***
***               operations performed on the expressions linked  ***
***               list.                                           ***
********************************************************************/

using System;
using System.Collections.Generic;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Expressions Linked List Class                      ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               perform actions on the expressions linked list. ***
    *********************************************************************/
    class ExpressionsLinkedList
    {
        List<ExpressionNode> expressions = new List<ExpressionNode>();

        /********************************************************************
        *** FUNCTION    : Add Function                                    ***
        *** DESCRIPTION : This function adds a node to the expressions    ***
        ***               linked list.                                    ***
        *** INPUT ARGS  : expressionNode node                             ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Add(ExpressionNode node)
        {
            expressions.Add(node);
        }

        /********************************************************************
        *** FUNCTION    : Search Expressions Function                     ***
        *** DESCRIPTION : This function searches each node in the         ***
        ***               expressions linked list for a specific string.  ***
        *** INPUT ARGS  : string expressionString                         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns containsExpression as a   ***
        ***               bool.                                           ***
        *********************************************************************/
        public bool SearchExpressions(string expressionString)
        {
            bool containsExpression = false;

            foreach (ExpressionNode exp in expressions)
            {
                if (exp.expression == expressionString)
                {
                    containsExpression = true;
                }
            }
            return containsExpression;
        }

        /********************************************************************
        *** FUNCTION    : Display Function                                ***
        *** DESCRIPTION : This function displays the expressions linked   ***
        ***               list in a table format.                         ***
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
            Console.WriteLine("                           Expression Table Data                               ");
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine(string.Format("{0, 0} {1, -16} | {2, -7} | {3, -5} | {4, -6} | {5, -8} | {6, -12} |", "|", "Expression", "Value", "Relocatable", "N-Bit", "I-Bit", "X-Bit"));        // Formatting for output
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            foreach (ExpressionNode expression in expressions)
            {

                string relocatable = "ABSOLUTE";

                if(expression.relocatable)
                {
                    relocatable = "RELATIVE";
                }
                Console.WriteLine(string.Format("{0, 0} {1, -16} | {2, -7} | {3, -11} | {4, -6} | {5, -8} | {6, -12} |", "|", expression.expression, expression.value.ToString(), relocatable, Convert.ToInt32(expression.indirect), Convert.ToInt32(expression.immediate), Convert.ToInt32(expression.indexed)));

                counter++;
                if (counter == 20)
                {
                    counter = 0;
                    Console.WriteLine("--- Press Enter to view more lines ---");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("--- Press Enter to view the list of literal errors ---\n");
        }
    }
}