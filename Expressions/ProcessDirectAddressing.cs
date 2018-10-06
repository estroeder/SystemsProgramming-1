/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ProcessDirectAddressing.cs file processes  ***
***               the rules included for direct addressing.       ***
********************************************************************/

using System;
using System.Linq;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Process Direct Addressing Class                    ***
    *** DESCRIPTION : This class consists of all the functions that   ***
    ***               are used to process direct addressing.          ***
    *********************************************************************/
    class ProcessDirectAddressing
    {
        Node tempNode;
        Node symbolToAdd;

        /********************************************************************
        *** FUNCTION    : Process Function                                ***
        *** DESCRIPTION : This function processes an expression that      ***
        ***               utilizes direct addressing.                     ***
        *** INPUT ARGS  : ExpressionsLinkedList expressionsLinkedList,    ***
        ***               BinarySearchTree symbolTable, string expression ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void Process(ExpressionsLinkedList expressionsLinkedList, BinarySearchTree symbolTable, string expression)
        {
            string substring, primary, secondary;
            int final;
            string[] tempStrippedStatement;
            bool dupestring = false;

            // Checks for subtraction
            if (expression.Contains("-"))
            {
                tempStrippedStatement = expression.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStrippedStatement.Count() != 1)
                {
                    dupestring = expressionsLinkedList.SearchExpressions(expression);
                    if (!dupestring)
                    {
                        // ABSOLUTE - ABSOLUTE check
                        if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            final = Convert.ToInt32(primary) - Convert.ToInt32(secondary);
                            ExpressionNode node = new ExpressionNode(expression, final, false, false, true, true, false); // Expression, Value, relocatable, direct, NBit, IBit, Xbit
                            expressionsLinkedList.Add(node);
                        }
                        // ABSOLUTE - Symbol check
                        else if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            tempNode = symbolTable.Search(secondary);
                            if (tempNode != null)
                            {
                                if (tempNode.Rflag == false)
                                {
                                    final = Convert.ToInt32(primary) - tempNode.Value;
                                    ExpressionNode node = new ExpressionNode(expression, final, false, true, true, true, false);
                                    expressionsLinkedList.Add(node);
                                }
                                else
                                {
                                    Console.WriteLine("Error: The expression " + expression + " contains an invalid Rflag combination (0 - 1)");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                            }
                        }
                        // Symbol - Symbol check
                        else if (tempStrippedStatement[0].Any(x => char.IsLetter(x)) && tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            symbolToAdd = symbolTable.Search(primary);
                            tempNode = symbolTable.Search(secondary);
                            if (symbolToAdd != null && tempNode != null)
                            {
                                final = symbolToAdd.Value - tempNode.Value;
                                if (symbolToAdd.Rflag)
                                {
                                    if (tempNode.Rflag)
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                                else
                                {
                                    if (tempNode.Rflag)
                                    {
                                        Console.WriteLine("Error: The expression " + expression + " contains an invalid Rflag combination (0 - 1)");
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                            }
                            else
                            {
                                if (symbolToAdd == null)
                                {
                                    Console.WriteLine("Error: The symbol " + primary + " was not found in the symbol table for the expression: " + expression);
                                }
                                else if (tempNode == null)
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
                            }
                        }
                        // Symbol - ABSOLUTE check
                        else if (tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            tempNode = symbolTable.Search(primary);
                            if (tempNode != null)
                            {
                                final = tempNode.Value - Convert.ToInt32(secondary);
                                ExpressionNode node = new ExpressionNode(expression, final, tempNode.Rflag, true, true, true, false);
                                expressionsLinkedList.Add(node);
                            }
                            else
                            {
                                Console.WriteLine("Error: The symbol " + primary + " was not found in the symbol table for the expression: " + expression);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: The symbol " + expression + " is a duplicate expression");
                    }
                }
                else
                {
                    if (expression.Length < 17)
                    {
                        if (expression.Length > 6)
                        {
                            substring = expression.Substring(0, 6);
                        }
                        else
                        {
                            substring = expression;
                        }

                        dupestring = expressionsLinkedList.SearchExpressions(substring);
                        if (!dupestring)
                        {
                            ExpressionNode node = new ExpressionNode(substring, Convert.ToInt32(substring), false, false, true, true, false);
                            expressionsLinkedList.Add(node);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Symbol maximum length of 17: " + expression);
                    }
                }
            }
            // Checks for addition
            else if (expression.Contains('+'))
            {
                tempStrippedStatement = expression.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStrippedStatement.Count() != 1)
                {
                    dupestring = expressionsLinkedList.SearchExpressions(expression);
                    if (!dupestring)
                    {
                        bool firstContainsLetter = tempStrippedStatement[0].Any(x => char.IsLetter(x));
                        bool secondContainsLetter = tempStrippedStatement[1].Any(x => char.IsLetter(x));

                        // ABSOLUTE + ABSOLUTE check
                        if (!firstContainsLetter && !secondContainsLetter)
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            final = Convert.ToInt32(primary) + Convert.ToInt32(secondary);
                            ExpressionNode node = new ExpressionNode(expression, final, false, true, true, true, false);
                            expressionsLinkedList.Add(node);
                        }
                        // Symbol + Symbol check
                        else if (firstContainsLetter && secondContainsLetter)
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            symbolToAdd = symbolTable.Search(primary);
                            tempNode = symbolTable.Search(secondary);
                            if (symbolToAdd != null && tempNode != null)
                            {
                                final = symbolToAdd.Value + tempNode.Value;
                                if (symbolToAdd.Rflag)
                                {
                                    if (tempNode.Rflag)
                                    {
                                        Console.WriteLine("Error: The expression " + expression + " contains an invalid Rflag combination (1 + 1)");
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                                else
                                {
                                    if (tempNode.Rflag)
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, true, true, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                            }
                            else
                            {
                                if (symbolToAdd == null)
                                {
                                    Console.WriteLine("Error: The symbol " + primary + " was not found in the symbol table for the expression: " + expression);
                                }
                                else if (tempNode == null)
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
                            }
                        }
                        else // Symbol + ABSOLUTE || ABSOLUTE + Symbol check
                        {
                            string symbol;
                            string value;
                            if (firstContainsLetter)
                            {
                                symbol = tempStrippedStatement[0];
                                value = tempStrippedStatement[1];
                                tempNode = symbolTable.Search(symbol);
                            }
                            else
                            {
                                value = tempStrippedStatement[0];
                                symbol = tempStrippedStatement[1];
                                tempNode = symbolTable.Search(symbol);
                            }

                            if (tempNode != null)
                            {
                                final = Convert.ToInt32(value) + tempNode.Value;
                                ExpressionNode node = new ExpressionNode(expression, final, tempNode.Rflag, true, true, true, false);
                                expressionsLinkedList.Add(node);
                            }
                            else
                            {
                                Console.WriteLine("Error: The symbol " + symbol + " was not found in the symbol table for the expression: " + expression);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: The symbol " + expression + " is a duplicate expression");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The symbol " + expression + " contains an invalid character");
                }
            }
            // No addition or subtraction
            else
            {
                if (expression.Length < 17)
                {
                    if (expression.Length > 6)
                    {
                        substring = expression.Substring(0, 6);
                    }
                    else
                    {
                        substring = expression;
                    }

                    dupestring = expressionsLinkedList.SearchExpressions(substring);
                    if (!dupestring)
                    {
                        if (expression.Any(x => char.IsLetter(x)))
                        {
                            symbolToAdd = symbolTable.Search(substring);
                            try
                            {
                                ExpressionNode node = new ExpressionNode(symbolToAdd.Symbol, symbolToAdd.Value, symbolToAdd.Rflag, true, true, true, false);
                                expressionsLinkedList.Add(node);
                            }
                            catch
                            {
                                Console.WriteLine("Error: The symbol " + substring + " was not found in the symbol table");
                            }
                        }
                        else
                        {
                            ExpressionNode node = new ExpressionNode(substring, Convert.ToInt32(substring), false, false, false, true, false);
                            expressionsLinkedList.Add(node);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: The symbol " + substring + " is a duplicate symbol");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Symbol maximum length of 17: " + expression);
                }
            }
        }
    }
}