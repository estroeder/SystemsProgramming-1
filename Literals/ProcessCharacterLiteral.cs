using System;
using System.Collections.Generic;
using System.Linq;

namespace TroyerA2
{
    class ProcessCharacterLiteral
    {
        public void Process(LiteralsLinkedList literalsLinkedList, string literalToBeValidated, string literal, int address, List<string> processedLiterals)
        {
            string final = "";

            foreach (char character in literalToBeValidated)
            {
                int value = Convert.ToInt32(character);
                string hexOutput = String.Format("{0:X}", value);

                final += hexOutput;
            }
            if (!processedLiterals.Contains(literalToBeValidated))
            {
                processedLiterals.Add(literalToBeValidated);
                LiteralNode node = new LiteralNode(literal, final, literalToBeValidated.Count(), address);
                literalsLinkedList.Add(node);
                address++;
            }
            else
            {
                literalsLinkedList.LiteralErrors.Add("Error: Literal " + literal + " is a duplicate literal");
            }
        }
    }
}
