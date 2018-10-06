using System;
using System.Collections.Generic;

namespace TroyerA2
{
    class LiteralsLinkedList
    {
        List<LiteralNode> literals = new List<LiteralNode>();
        public List<string> LiteralErrors = new List<string>();

        public void Add(LiteralNode node)
        {
            literals.Add(node);
        }

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

        public void Display()
        {
            int counter = 0;

            Console.Clear();
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
        }

        public void DisplayErrors()
        {
            foreach (string error in LiteralErrors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
