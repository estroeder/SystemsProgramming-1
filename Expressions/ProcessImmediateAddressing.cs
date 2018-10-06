/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ProcessImmediateAddressing.cs file         ***
***               processes the rules included for immediate      ***
***               addressing.                                     ***
********************************************************************/

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Process Immediate Addressing Class                 ***
    *** DESCRIPTION : This class consists of all the functions that   ***
    ***               are used to process immediate addressing.       ***
    *********************************************************************/
    class ProcessImmediateAddressing
    {
        Node foundSymbol;
        Node symbolToAdd;

        /********************************************************************
        *** FUNCTION    : Process Function                                ***
        *** DESCRIPTION : This function processes an expression that      ***
        ***               utilizes immediate addressing.                  ***
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
            bool dupestring = false;
            string[] tempStrippedStatement;
            tempStrippedStatement = expression.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            // Checks for subtraction
            if (tempStrippedStatement[0].Contains('-'))
            {
                tempStrippedStatement = tempStrippedStatement[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
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
                            ExpressionNode node = new ExpressionNode(expression, final, false, false, false, true, false);
                            expressionsLinkedList.Add(node);
                        }
                        // ABSOLUTE - Symbol check
                        else if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            foundSymbol = symbolTable.Search(secondary);
                            if (foundSymbol != null)
                            {
                                if (foundSymbol.Rflag == false)
                                {
                                    final = Convert.ToInt32(primary) - foundSymbol.Value;
                                    ExpressionNode node = new ExpressionNode(expression, final, false, false, false, true, false);
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
                            foundSymbol = symbolTable.Search(secondary);
                            if (symbolToAdd != null && foundSymbol != null)
                            {
                                final = symbolToAdd.Value - foundSymbol.Value;
                                if (symbolToAdd.Rflag)
                                {
                                    if (foundSymbol.Rflag)
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, false, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, false, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                                else
                                {
                                    if (foundSymbol.Rflag)
                                    {
                                        Console.WriteLine("Error: The expression " + expression + " contains an invalid Rflag combination (0 - 1)");
                                    }
                                    else
                                    { 
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, false, true, false);
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
                                else if (foundSymbol == null)
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
                            foundSymbol = symbolTable.Search(primary);
                            if (foundSymbol != null)
                            {
                                final = foundSymbol.Value - Convert.ToInt32(secondary);
                                ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, false, true, false);
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
                    Console.WriteLine("Error: The expression " + expression + " cannot perform any special addressing");
                }
            }
            // Checks for addition
            else if (tempStrippedStatement[0].Contains('+'))
            {
                tempStrippedStatement = tempStrippedStatement[0].Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStrippedStatement.Count() != 1)
                {
                    dupestring = expressionsLinkedList.SearchExpressions(expression);
                    if (!dupestring)
                    {
                        // ABSOLUTE + ABSOLUTE check
                        if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            Console.WriteLine("Error: The expression " + expression + " cannot perform any special addressing");
                        }
                        // Symbol + ABSOLUTE || ABSOLUTE + Symbol check
                        else if ((!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && tempStrippedStatement[1].Any(x => char.IsLetter(x))) || (tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x))))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            if (primary.Any(x => char.IsLetter(x)))
                            {
                                foundSymbol = symbolTable.Search(primary);
                                if (foundSymbol != null)
                                {
                                    final = foundSymbol.Value + Convert.ToInt32(secondary);
                                    ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, false, true, false);
                                    expressionsLinkedList.Add(node);
                                }
                                else
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
                            }
                            else
                            {
                                foundSymbol = symbolTable.Search(secondary);
                                if (foundSymbol != null)
                                {
                                    final = Convert.ToInt32(primary) + foundSymbol.Value;
                                    ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, false, true, false);
                                    expressionsLinkedList.Add(node);
                                }
                                else
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
                            }
                        }
                        // Symbol + Symbol check
                        else if (tempStrippedStatement[0].Any(x => char.IsLetter(x)) && tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            symbolToAdd = symbolTable.Search(primary);
                            foundSymbol = symbolTable.Search(secondary);
                            if (symbolToAdd != null && foundSymbol != null)
                            {
                                final = symbolToAdd.Value + foundSymbol.Value;
                                if (symbolToAdd.Rflag)
                                {
                                    if (foundSymbol.Rflag)
                                    {
                                        Console.WriteLine("Error: The expression " + expression + " contains an invalid Rflag combination (1 + 1)");
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, false, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                                else
                                {
                                    if (foundSymbol.Rflag)
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, false, true, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, false, true, false);
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
                                else if (foundSymbol == null)
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
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
                Regex rg = new Regex(@"^[a-zA-Z0-9]+$");
                if (rg.IsMatch(tempStrippedStatement[0]))
                {
                    if (tempStrippedStatement[0].Length < 17)
                    {
                        if (tempStrippedStatement[0].Length > 6)
                        {
                            substring = tempStrippedStatement[0].Substring(0, 6);
                        }
                        else
                        {
                            substring = tempStrippedStatement[0];
                        }

                        dupestring = expressionsLinkedList.SearchExpressions("#" + substring);
                        if (!dupestring)
                        {
                            if (substring.Any(x => char.IsLetter(x)))
                            {
                                symbolToAdd = symbolTable.Search(substring);
                                try
                                {
                                    ExpressionNode node = new ExpressionNode("#" + substring, symbolToAdd.Value, symbolToAdd.Rflag, false, false, true, false);
                                    expressionsLinkedList.Add(node);
                                }
                                catch
                                {
                                    Console.WriteLine("Error: The symbol " + substring + " was not found in the symbol table");
                                }
                            }
                            else
                            {
                                ExpressionNode node = new ExpressionNode(expression, Convert.ToInt32(substring), false, false, false, true, false);
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
                else
                {
                    Console.WriteLine("Error: Expression cannot contain special characters: " + expression);
                }
            }
        }
    }
}
