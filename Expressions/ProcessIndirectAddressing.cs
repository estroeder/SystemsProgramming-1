﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TroyerA2
{
    class ProcessIndirectAddressing
    {
        Node foundSymbol;
        Node symbolToAdd;

        public void Process(ExpressionsLinkedList expressionsLinkedList, BinarySearchTree symbolTable, string expression)
        {
            string substring, primary, secondary;
            int final;
            bool dupestring = false;
            string[] tempStrippedStatement;
            tempStrippedStatement = expression.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            //////// SUBTRACTION CHECKS
            if (tempStrippedStatement[0].Contains('-'))
            {
                tempStrippedStatement = tempStrippedStatement[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStrippedStatement.Count() != 1)
                {
                    dupestring = expressionsLinkedList.SearchExpressions(expression);
                    if (!dupestring)
                    {
                        // ABSOLUTE - ABSOLUTE
                        if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            Console.WriteLine("Error: The expression " + expression + " cannot perform any special addressing");
                        }
                        // ABSOLUTE - SYMBOL
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
                                    ExpressionNode node = new ExpressionNode(expression, final, false, false, true, false, false);
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
                        // SYMBOL - SYMBOL
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
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, true, false, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, true, false, false);
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
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, true, false, false);
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
                        // SYMBOL - ABSOLUTE
                        else if (tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            primary = tempStrippedStatement[0];
                            secondary = tempStrippedStatement[1];
                            foundSymbol = symbolTable.Search(primary);
                            if (foundSymbol != null)
                            {
                                final = foundSymbol.Value - Convert.ToInt32(secondary);
                                ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, true, false, false);
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
            ///////// ADDITION CHECKS
            else if (tempStrippedStatement[0].Contains('+'))
            {
                tempStrippedStatement = tempStrippedStatement[0].Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStrippedStatement.Count() != 1)
                {
                    dupestring = expressionsLinkedList.SearchExpressions(expression);
                    if (!dupestring)
                    {
                        // ABSOLUTE + ABSOLUTE
                        if (!tempStrippedStatement[0].Any(x => char.IsLetter(x)) && !tempStrippedStatement[1].Any(x => char.IsLetter(x)))
                        {
                            Console.WriteLine("Error: The expression " + expression + " cannot perform any special addressing");
                        }
                        // ABSOLUTE + SYMBOL || Symbol + ABSOLUTE
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
                                    ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, true, false, false);
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
                                    ExpressionNode node = new ExpressionNode(expression, final, foundSymbol.Rflag, false, true, false, false);
                                    expressionsLinkedList.Add(node);
                                }
                                else
                                {
                                    Console.WriteLine("Error: The symbol " + secondary + " was not found in the symbol table for the expression: " + expression);
                                }
                            }
                        }
                        // SYMBOL + SYMBOL
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
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, true, false, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                }
                                else
                                {
                                    if (foundSymbol.Rflag)
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, true, false, true, false, false);
                                        expressionsLinkedList.Add(node);
                                    }
                                    else
                                    {
                                        ExpressionNode node = new ExpressionNode(expression, final, false, false, true, false, false);
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
            //////// NO EXPRESSION
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

                        dupestring = expressionsLinkedList.SearchExpressions(substring);
                        if (!dupestring)
                        {
                            if (substring.Any(x => char.IsLetter(x)))
                            {
                                symbolToAdd = symbolTable.Search(substring);
                                try
                                {
                                    ExpressionNode node = new ExpressionNode(expression, symbolToAdd.Value, symbolToAdd.Rflag, false, true, false, false);
                                    expressionsLinkedList.Add(node);
                                }
                                catch
                                {
                                    Console.WriteLine("Error: The symbol " + substring + " was not found in the symbol table");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: The expression " + expression + " cannot perform any special addressing");
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
