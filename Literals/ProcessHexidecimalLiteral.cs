using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TroyerA2
{
    class ProcessHexidecimalLiteral
    {
        public void Process(LiteralsLinkedList literalsLinkedList, string literalToBeValidated, string literal, int address, List<string> processedLiterals)
        {
            string final = "";
            Regex rg = new Regex("[0-9A-Fa-f]+");

            if (rg.IsMatch(literalToBeValidated))
            {
                if (literalToBeValidated[0] <= '7' && literalToBeValidated[0] >= '0')
                {
                    if (literalToBeValidated.Length % 2 != 0)
                    {
                        final = "0";
                        foreach (char ch in literalToBeValidated)
                        {
                            final += ch;
                        }
                    }
                    else
                    {
                        final = literalToBeValidated;
                    }
                }
                else if (literalToBeValidated[0] == '8' || literalToBeValidated[0] == '9' || literalToBeValidated[0] >= 'A' || literalToBeValidated[0] <= 'B' || literalToBeValidated[0] == 'C' || literalToBeValidated[0] == 'D' || literalToBeValidated[0] == 'E' || literalToBeValidated[0] == 'F')
                {
                    if (literalToBeValidated.Length % 2 != 0)
                    {
                        final = "F";
                        foreach (char ch in literalToBeValidated)
                        {
                            final += ch;
                        }
                    }
                    else
                    {
                        final = literalToBeValidated;
                    }
                }
                else
                {
                    literalsLinkedList.LiteralErrors.Add("Error: " + literal + " may only contain hexadecimal characters (0 - F)");
                }
                if (!processedLiterals.Contains(literalToBeValidated))
                {
                    processedLiterals.Add(literalToBeValidated);
                    LiteralNode node = new LiteralNode(literal, final, (final.Count() / 2), address);
                    literalsLinkedList.Add(node);
                    address++;
                }
                else
                {
                    literalsLinkedList.LiteralErrors.Add("Error: Literal " + literal + " is a duplicate literal");
                }
            }
            else
            {
                literalsLinkedList.LiteralErrors.Add("Error: " + literal + " may only contain hexadecimal characters (0 - F)");
            }
        }
    }
}
