/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ExpressionValidator.cs file validates an   ***
***               expression.                                     ***
********************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Expression Validator Class                         ***
    *** DESCRIPTION : This class contains all the methods used to     ***
    ***               validate an expression.                         ***
    *********************************************************************/
    class ExpressionValidator
    {
        enum AddressType { Direct, Indirect, Immediate, Indexed };

        /********************************************************************
        *** FUNCTION    : Validate Expressions Function                   ***
        *** DESCRIPTION : This function loops through the expressions and ***
        ***               validates them and stores the valid expressions ***
        ***               into a node.                                    ***
        *** INPUT ARGS  : ExpressionsLinkedList expressionsLinkedList,    ***
        ***               BinarySearchTree symbolTable,                   ***
        ***               List<string> expressions                        ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
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

        /********************************************************************
        *** FUNCTION    : Validate Expression Function                    ***
        *** DESCRIPTION : This function validates an expression.          ***
        *** INPUT ARGS  : string expression                               ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns isValid as a bool.        ***
        *********************************************************************/
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

        /*********************************************************************
        *** FUNCTION    : Special Character Validation Function            ***
        *** DESCRIPTION : This function checks the expression to see if    ***
        ***               it contains invalid special characters.          ***
        *** INPUT ARGS  : string expression                                ***
        *** OUTPUT ARGS : This function has zero output arguments.         ***
        *** IN/OUT ARGS : This function has zero input/output arguments.   ***
        *** RETURN      : This function returns isValid as a bool.         ***
        *********************************************************************/
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

        /********************************************************************
        *** FUNCTION    : Operation Validation Function                   ***
        *** DESCRIPTION : This function checks to see if valid operations ***
        ***               using addition and subtraction are performed    ***
        ***               in the expression.                              ***
        *** INPUT ARGS  : string splitExpression, string expression       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returnsa isValid as a bool.       ***
        *********************************************************************/
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

        /********************************************************************
        *** FUNCTION    : Addition Validation Function                    ***
        *** DESCRIPTION : This function validates the use of addition in  ***
        ***               an expression.                                  ***
        *** INPUT ARGS  : string splitExpression, string expression       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns isValid as a bool.        ***
        *********************************************************************/
        private bool AdditionValidation(string splitExpression, string expression)
        {
            bool isValid = true;

            if (expression.Contains("+"))
            {
                int doubleplus = expression.IndexOf("++");

                // +ABSOLUTE
                if (expression[0] == '+' && Char.IsDigit(expression[1]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // +SYMBOL
                else if (expression[0] == '+' && Char.IsLetter(expression[1]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // ++ABSOLUTE
                else if (doubleplus != -1 && Char.IsDigit(expression[doubleplus + 2]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // ++SYMBOL
                else if (doubleplus != -1 && Char.IsLetter(expression[doubleplus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // ABSOLUTE+, SYMBOL+
                else if (expression.EndsWith("+"))
                {
                    Console.WriteLine("Error: Invalid Operation " + expression);
                    isValid = false;
                }
                // Valid '+' Expression
                else
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        /********************************************************************
        *** FUNCTION    : Subtraction Validation Function                 ***
        *** DESCRIPTION : This function validates the use of subtraction  ***
        ***               in an expression.                               ***
        *** INPUT ARGS  : string splitExpression, string expression       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns isValid as a bool.        ***
        *********************************************************************/
        private bool SubtractionValidation(string splitExpression, string expression)
        {
            bool isValid = true;

            if (splitExpression.Contains("-"))
            {
                int doubleminus = splitExpression.IndexOf("--");
                int minusplus = splitExpression.IndexOf("-+");
                int plusminus = splitExpression.IndexOf("+-");

                // -SYMBOL
                if (splitExpression[0] == '-' && Char.IsLetter(splitExpression[1]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // --SYMBOL
                else if (doubleminus != -1 && Char.IsLetter(splitExpression[doubleminus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // -+ABSOLUTE
                else if (minusplus != -1 && Char.IsDigit(splitExpression[minusplus + 2]))
                {
                    Console.WriteLine("Error: Value must be of integer format: " + expression);
                    isValid = false;
                }
                // -+SYMBOL
                else if (minusplus != -1 && Char.IsLetter(splitExpression[minusplus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // +-SYMBOL
                else if (plusminus != -1 && Char.IsLetter(splitExpression[plusminus + 2]))
                {
                    Console.WriteLine("Error: Symbol cannot contain special characters: " + expression);
                    isValid = false;
                }
                // ABSOLUTE-, SYMBOL-
                else if (splitExpression.EndsWith("-"))
                {
                    Console.WriteLine("Error: Invalid Operation " + expression);
                    isValid = false;
                }
                // Valid '-' Expression
                else
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        /********************************************************************
        *** FUNCTION    : Operation Count Validation Function             ***
        *** DESCRIPTION : This function checks the number of '+' and '-'  ***
        ***               operations in an expression.                    ***
        *** INPUT ARGS  : string splitExpression, string expression       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns isValid as a bool.        ***
        *********************************************************************/
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

        /********************************************************************
        *** FUNCTION    : Process Expression Function                     ***
        *** DESCRIPTION : This function checks to see if an expression    ***
        ***               uses indirect, immediate, indexed, or direct    ***
        ***               addressing and then processes the expression.   ***
        *** INPUT ARGS  : ExpressionsLinkedList expressionsLinkedList,    ***
        ***               BinarySearchTree symbolTable, string expression ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
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