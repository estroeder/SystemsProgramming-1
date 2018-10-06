using System;
using System.IO;
using System.Linq;

namespace TroyerA2
{
    class PopulateSymbolTable
    {
        // Constant for the name of data file (Program can be easily changed if name of file changes)
        private const string DATA_FILE_NAME = "SYMS.DAT";

        // Binary search tree variable that can be accessed by PopulateSymbolTableProcess()
        BinarySearchTree binarySearchTree = new BinarySearchTree();

        /********************************************************************
        *** FUNCTION    : Create Binary Search Tree From Data Function    ***
        *** DESCRIPTION : This function utilizes the other functions in   ***
        ***               this class to add the validated nodes to the    ***
        ***               BST and displays the list of errors it          ***
        ***               encounters while processing the data.           ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns a BinarySearchTree.       ***
        *********************************************************************/
        public BinarySearchTree CreateBinarySearchTreeFromDataFile()
        {
            // Get contents of data file and save into memory
            string[] dataFileContents = GetSymbolTableFileData();

            Console.WriteLine("==================================================================================================");
            Console.WriteLine("                        List of Errors While Processing Data File:                 ");
            Console.WriteLine("==================================================================================================");
            foreach (string dataFileLine in dataFileContents)
            {
                // Save the parsed data file line into an array of strings
                string[] parsedDataFileLine = ParseDataFileLine(dataFileLine);

                // Validate the data in the parsed data file line
                ValidateSymbolTable symbolTableValidator = new ValidateSymbolTable();

                Node validatedNode = symbolTableValidator.ValidateDataFileLine(parsedDataFileLine);

                AddNodeToBinarySearchTree(validatedNode);
            }
            Console.WriteLine("==================================================================================================");
            return binarySearchTree;
        }

        /********************************************************************
        *** FUNCTION    : Get Symbol Table File Data Function             ***
        *** DESCRIPTION : This function reads each line of the data file  ***
        ***               data and saves each line into an array of       ***
        ***               strings.                                        ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This functions returns an array of strings.     ***
        *********************************************************************/
        private string[] GetSymbolTableFileData()
        {
            string[] dataFileContents = File.ReadAllLines("...\\...\\" + DATA_FILE_NAME);
            return dataFileContents;
        }

        /********************************************************************
        *** FUNCTION    : Parse Data File Line Function                   ***
        *** DESCRIPTION : This function takes a line of the data file and ***
        ***               parses the line. This function saves the symbol ***
        ***               and the value and the Rflag into an array of    ***
        ***               strings as separate entities.                   ***
        *** INPUT ARGS  : string dataFileLine                             ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      :  This function returns an array of strings.     ***
        *********************************************************************/
        private string[] ParseDataFileLine(string dataFileLine)
        {
            // String array to hold the contents of the line
            string[] lineContents;

            // Trim off whitespace from the beginning and end of line
            dataFileLine = dataFileLine.Trim();

            // Split line every time it encounters white space
            lineContents = dataFileLine.Split(' ');

            // Loop through the trimmed line contents and eliminate all array items that are just a space
            lineContents = lineContents.Where(arrayItem => !String.IsNullOrWhiteSpace(arrayItem)).ToArray();

            // Return the array of strings containing the symbol, the value, and the RFlag
            return lineContents;
        }

        /********************************************************************
        *** FUNCTION    : Add Node To Binary Search Tree Function         ***
        *** DESCRIPTION : This function adds a node to the Binary Search  ***
        ***               Tree if the nodes is not empty. If the node is  ***
        ***               already in the Binary Search Tree, an error     ***
        ***               message is displayed.                           ***
        *** INPUT ARGS  : Node node                                       ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing (void).           ***
        *********************************************************************/
        public void AddNodeToBinarySearchTree(Node node)
        {
            if (node.Symbol != null)
            {
                if (binarySearchTree.Insert(node) == false)
                {
                    Node duplicateNode = binarySearchTree.Search(node.Symbol);
                    duplicateNode.Mflag = true;

                    Console.WriteLine("The symbol: '" + node.Symbol + "' was previously defined in the Symbol Table.");
                }
            }
        }
    }
}
