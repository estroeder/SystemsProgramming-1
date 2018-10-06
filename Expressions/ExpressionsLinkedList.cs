using System;
using System.Collections.Generic;

namespace TroyerA2
{
    class ExpressionsLinkedList
    {
        List<ExpressionNode> expressions = new List<ExpressionNode>();

        public void Add(ExpressionNode node)
        {
            expressions.Add(node);
        }

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

        public void Display()
        {
            int counter = 0;

            Console.Clear();
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
                    Console.WriteLine("-- Press enter to DISPLAY MORE LINES --");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("+-----------------------------------------------------------------------------+");
        }
    }
}