/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This PopulateExpressionTable.cs file processes  ***
***               the data from the expressions file.             ***
********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Populate Expression Table Class                    ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               populate the expression table.                  ***
    *********************************************************************/
    class PopulateExpressionTable
    {
        private List<string> expressions = new List<string>();
        private List<string> literals = new List<string>();

        /********************************************************************
        *** FUNCTION    : Process File Function                           ***
        *** DESCRIPTION : This funciton gets the expression file name and ***
        ***               reads in the contents of the expression file    ***
        ***               into an array of strings. It then separates the ***
        ***               array of strings into literals and expressions. ***
        ***               It then validates the expressions and validates ***
        ***               the literals.                                   ***
        *** INPUT ARGS  : ExpressionsLinkedList expressionsLinkedList,    ***
        ***               LiteralsLinkedList literalsLinkedList,          ***
        ***               BinarySearchTree symbolTable, string arg = ""   ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public void ProcessFile(ExpressionsLinkedList expressionsLinkedList, LiteralsLinkedList literalsLinkedList, BinarySearchTree symbolTable, string arg = "")
        {
            // Get file name if it exists
            string expressionFileName = GetExpressionFile(arg);

            // Read in the expression file and save it into a string array
            string[] fileContents = GetExpressionFileContents(expressionFileName);

            // Separate the file contents into literals and expressions
            SeparateFileContents(fileContents);

            // Validate the expressions
            ExpressionValidator expressionValidator = new ExpressionValidator();
            expressionValidator.ValidateExpressions(expressionsLinkedList, symbolTable, expressions);

            // Validate the literals
            LiteralValidator literalValidator = new LiteralValidator();
            literalValidator.Validate(literalsLinkedList, literals);
        }

        /********************************************************************
        *** FUNCTION    : Get Expression File Function                    ***
        *** DESCRIPTION : This function gets the expression file name.    ***
        *** INPUT ARGS  : string expressionFileName                       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns expressionFileName as a   ***
        ***               string.                                         ***
        *********************************************************************/
        private string GetExpressionFile(string expressionFileName)
        {
            if (expressionFileName == "")
            {
                // Gets the name of the expression file and save it into a variable
                Console.WriteLine("\nPlease enter the name of the expression file you would like to use: ");
                expressionFileName = Console.ReadLine();

                // Check to see if the specified file name exists in the project directory
                // Program executes within TroyerA2\bin\debug
                while (File.Exists("...\\...\\" + expressionFileName) == false)
                {
                    Console.WriteLine("\nError! That expression file name does not exist. Enter a valid file name: ");
                    expressionFileName = Console.ReadLine();
                }

                Console.WriteLine("\n---Press Enter to view the expression table data---");
            }
            else
            {
                Console.WriteLine("---Press Enter to view the expression table data---");
            }
            // Return file once a valid file name has been entered
            return expressionFileName;
        }

        /********************************************************************
        *** FUNCTION    : Get Expression File Contents Function           ***
        *** DESCRIPTION : This function saves the contents of the         ***
        ***               expression file into an array of strings.       ***
        *** INPUT ARGS  : string expressionFileName                       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns expressionFileContents as ***
        ***               an array of strings.                            ***
        *********************************************************************/
        private string[] GetExpressionFileContents(string expressionFileName)
        {
            string[] expressionFileContents;
            return expressionFileContents = File.ReadAllLines("...\\...\\" + expressionFileName);
        }

        /********************************************************************
        *** FUNCTION    : Separate File Contents Function                 ***
        *** DESCRIPTION : This function separates the file contents into  ***
        ***               expressions and literals.                       ***
        *** INPUT ARGS  : string[] fileContents                           ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        private void SeparateFileContents(string[] fileContents)
        {
            foreach(string fileLine in fileContents)
            {
                CheckIfLiteral(fileLine);
            }
        }

        /********************************************************************
        *** FUNCTION    : Check If Literal Function                       ***
        *** DESCRIPTION : This function checks to see if an expression is ***
        ***               a literal.                                      ***
        *** INPUT ARGS  : string dataFileLine                             ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        private void CheckIfLiteral(string dataFileLine)
        {
            // Trim off whitespace from the beginning and end of line
            dataFileLine = dataFileLine.Trim();
            if(dataFileLine[0] == '=')
            {
                literals.Add(dataFileLine);
            }
            else
            {
                expressions.Add(dataFileLine);
            }
        }
    }
}
