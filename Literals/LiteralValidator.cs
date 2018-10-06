using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TroyerA2
{
    class LiteralValidator
    {
        //List<string> literalErrors = new List<string>();
        List<string> processedLiterals = new List<string>();

        public void Validate(LiteralsLinkedList literalsLinkedList, List<string> literals)
        {
            foreach (string literal in literals)
            {
                // Get the literal expression
                string literalToBeValidated = ParseLiteral(literalsLinkedList, literal);

                // Validate literal
                if(literalToBeValidated != "")
                {
                    ValidateLiteral(literalsLinkedList, literalToBeValidated, literal);
                }
            }
        }

        private string ParseLiteral(LiteralsLinkedList literalsLinkedList, string literal)
        {
            string literalToValidate = "";

            Match match = Regex.Match(literal, @"'([^']*)");
            if (match.Success)
            {
                literalToValidate = match.Groups[1].Value;
            }
            else
            {
                literalsLinkedList.LiteralErrors.Add("Error: The literal " + literal + " must be separated by single quotations");
            }
            return literalToValidate;
        }

        private void ValidateLiteral(LiteralsLinkedList literalsLinkedList, string literalToBeValidated, string literal)
        {
            int hexchar;
            int address = 1;

            hexchar = literal.IndexOf("=");

            if (literal[hexchar + 1] == 'C' || literal[hexchar + 1] == 'c')
            {
                // Process Char Statement
                ProcessCharacterLiteral processCharacterLiteral = new ProcessCharacterLiteral();
                processCharacterLiteral.Process(literalsLinkedList, literalToBeValidated, literal, address, processedLiterals);
            }
            else if (literal[hexchar + 1] == 'X' || literal[hexchar + 1] == 'x')
            {
                // Process Hex Statement
                if (!processedLiterals.Contains(literalToBeValidated.ToUpper()) && !processedLiterals.Contains(literalToBeValidated.ToLower()))
                {
                    ProcessHexidecimalLiteral processHexidecimalLiteral = new ProcessHexidecimalLiteral();
                    processHexidecimalLiteral.Process(literalsLinkedList, literalToBeValidated, literal, address, processedLiterals);
                }
                else
                {
                    literalsLinkedList.LiteralErrors.Add("Error: Literal " + literal + " is a duplicate literal");
                }
            }
            else
            {
                // invalid statement
                literalsLinkedList.LiteralErrors.Add("Error: " + literal + " contains an invalid operation");
            }
        }
    }
}