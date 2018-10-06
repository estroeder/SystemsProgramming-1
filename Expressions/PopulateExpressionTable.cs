using System;
using System.Collections.Generic;
using System.IO;

namespace TroyerA2
{
    class PopulateExpressionTable
    {
        private List<string> expressions = new List<string>();
        private List<string> literals = new List<string>();

        // Get the expression file name
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

        private string GetExpressionFile(string expressionFileName)
        {
            if (expressionFileName == "")
            {
                // Gets the name of the expression file and save it into a variable
                Console.WriteLine("Please enter the name of the expression file you would like to use: ");
                expressionFileName = Console.ReadLine();
            }

            // Check to see if the specified file name exists in the project directory
            // Program executes within TroyerA2\bin\debug
            while (File.Exists("...\\...\\" + expressionFileName) == false)
            {
                Console.WriteLine("\nError! That expression file name does not exist. Enter a valid file name: ");
                expressionFileName = Console.ReadLine();
            }

            // Return file once a valid file name has been entered
            return expressionFileName;
        }

        private string[] GetExpressionFileContents(string expressionFileName)
        {
            string[] expressionFileContents;
            return expressionFileContents = File.ReadAllLines("...\\...\\" + expressionFileName);
        }

        private void SeparateFileContents(string[] fileContents)
        {
            foreach(string fileLine in fileContents)
            {
                CheckIfLiteral(fileLine);
            }
        }

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
