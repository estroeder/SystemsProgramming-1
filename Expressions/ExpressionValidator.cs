using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TroyerA2
{
    class ExpressionValidator
    {
        enum AddressType { Direct, Indirect, Immediate, Indexed };
        
        public void ValidateExpressions(ExpressionsLinkedList expressionsLinkedList, BinarySearchTree symbolTable, List<string> expressions)
        {
            foreach (string expression in expressions)
            {
                // Validate the expression
                bool isValidExpression = ValidateExpression(expression);

                // Store expression into a node
                if (isValidExpression == true)
                {
                    ProcessExpression(expressionsLinkedList, symbolTable, expression);
                }
            }
        }

        #region Validation

        private bool ValidateExpression(string expression)
        {
            bool isValid = true;
            string splitExpression = expression.Split(new char[] { '@', '#' }, StringSplitOptions.RemoveEmptyEntries)[0].ToString();

            isValid = SpecialCharacterValidation(splitExpression);
            if (isValid)
            {
                isValid = OperationValidation(splitExpression, expression);
            }
            return isValid;
        }

        private bool SpecialCharacterValidation(string expression)
        {
            bool isValid = true;
            Regex rg = new Regex(@"^[a-zA-Z0-9]+$");

            if (!rg.IsMatch(expression) && !expression.Contains("-") && !expression.Contains("'") && !expression.Contains(",") && !expression.Contains("+") || expression.Contains("*"))
            {
                Console.WriteLine("Error: Expression cannot contain special characters: " + expression);
                isValid = false;
            }
            return isValid;
        }

        private bool OperationValidation(string splitExpression, string expression)
        {
            bool isValid;

            isValid = AdditionValidation(splitExpression, expression);
            if (isValid)
            {
                isValid = SubtractionValidation(splitExpression, expression);
            }
            if (isValid)
            {
                isValid = OperationCountValidation(splitExpression, expression);
            }
            return isValid;
        }

        private bool AdditionValidation(string splitExpression, string expression)
        {
            bool isValid = true;

            if (expression.Contains("+"))
            {
                int doubleplus = expression.IndexOf("++");

                // Case: '+ABSOLUTE'
                if (expression[0] == '+' && Char.IsDigit(expression[1]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // Case: '+SYMBOL'
                else if (expression[0] == '+' && Char.IsLetter(expression[1]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: '++ABSOLUTE'
                else if (doubleplus != -1 && Char.IsDigit(expression[doubleplus + 2]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // Case: '++SYMBOL'
                else if (doubleplus != -1 && Char.IsLetter(expression[doubleplus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: 'ABSOLUTE+, SYMBOL+'
                else if (expression.EndsWith("+"))
                {
                    Console.WriteLine("Error: Invalid Operation " + expression);
                    isValid = false;
                }
                // Case: 'Valid '+' Expression'
                else
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        private bool SubtractionValidation(string splitExpression, string expression)
        {
            bool isValid = true;

            if (splitExpression.Contains("-"))
            {
                int doubleminus = splitExpression.IndexOf("--");
                int minusplus = splitExpression.IndexOf("-+");
                int plusminus = splitExpression.IndexOf("+-");

                // Case: '-SYMBOL'
                if (splitExpression[0] == '-' && Char.IsLetter(splitExpression[1]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: '--SYMBOL'
                else if (doubleminus != -1 && Char.IsLetter(splitExpression[doubleminus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: '-+ABSOLUTE' FIXD
                else if (minusplus != -1 && Char.IsDigit(splitExpression[minusplus + 2]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // Case: '-+SYMBOL'
                else if (minusplus != -1 && Char.IsLetter(splitExpression[minusplus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: '+-SYMBOL'
                else if (plusminus != -1 && Char.IsLetter(splitExpression[plusminus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // Case: 'ABSOLUTE-, SYMBOL-'
                else if (splitExpression.EndsWith("-"))
                {
                    Console.WriteLine("Error: Invalid Operation " + expression);
                    isValid = false;
                }
                // Case: 'Valid '-' Expression'
                else
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        private bool OperationCountValidation(string splitExpression, string expression)
        {
            bool isValid = true;
            var singlePlusCount = splitExpression.Count(x => x == '+');
            var singleMinusCount = splitExpression.Count(x => x == '-');
            int plus = splitExpression.IndexOf("+");

            if (singlePlusCount > 1)
            {
                Console.WriteLine("Error: Invalid number of operations: " + expression);
                isValid = false;
            }
            else if (singleMinusCount > 3)
            {
                Console.WriteLine("Error: Invalid number of operations: " + expression);
                isValid = false;
            }
            else if (singleMinusCount == 1 && singlePlusCount == 1)
            {
                if (expression[0] != '-' && !(expression.Contains("+-")))
                {
                    Console.WriteLine("Error: Invalid number of operations: " + expression);
                    isValid = false;
                }
            }
            else if (singleMinusCount == 2 && expression[0] == '-')
            {
                if (expression.Contains("--") || !(expression[plus + 1] == '-'))
                {
                    Console.WriteLine("Error: Invalid number of operations: " + expression);
                    isValid = false;
                }
            }
            else if (singleMinusCount == 2 && expression[0] != '-')
            {
                if (!expression.Contains("--"))
                {
                    Console.WriteLine("Error: Invalid number of operations: " + expression);
                    isValid = false;
                }
            }
            else if (singleMinusCount == 3)
            {
                if (singlePlusCount > 0)
                {
                    Console.WriteLine("Error: Invalid number of operations: " + expression);
                    isValid = false;
                }
                else if (!(expression.Contains("--")) || expression[0] != '-')
                {
                    Console.WriteLine("Error: Invalid number of operations: " + expression);
                    isValid = false;
                }
            }
            return isValid;
        }

        #endregion Validation

        private void ProcessExpression(ExpressionsLinkedList expressionsLinkedList, BinarySearchTree symbolTable, string expression)
        {
            // Process the valid expression
            if(expression[0] == '@') // Indirect
            {
                ProcessIndirectAddressing processIndirectAddressing = new ProcessIndirectAddressing();
                processIndirectAddressing.Process(expressionsLinkedList, symbolTable, expression);
            }
            else if(expression[0] == '#') // Immediate
            {
                ProcessImmediateAddressing processImmediateAddressing = new ProcessImmediateAddressing();
                processImmediateAddressing.Process(expressionsLinkedList, symbolTable, expression);
            }
            else if(expression.Contains(",")) // Indexed
            {
                ProcessIndexedAddressing processIndexedAddressing = new ProcessIndexedAddressing();
                processIndexedAddressing.Process(expressionsLinkedList, symbolTable, expression);
            }
            else // Direct
            {
                ProcessDirectAddressing processDirectAddressing = new ProcessDirectAddressing();
                processDirectAddressing.Process(expressionsLinkedList, symbolTable, expression);
            }
        }

    }
}