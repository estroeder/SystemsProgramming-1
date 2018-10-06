using System;
using System.Collections.Generic;
using System.Linq;

namespace TroyerA2
{
    class BinarySearchTree
    {
        // Allocate space for a new Sorted Set (Sorted BST)
        SortedSet<Node> binarySearchTree = new SortedSet<Node>();

        /********************************************************************
        *** FUNCTION    :  Insert Function                                ***
        *** DESCRIPTION :  This function inserts a node into the BST.     ***
        *** INPUT ARGS  :  Node nodeValue                                 ***
        *** OUTPUT ARGS :  This function has zero output arguments.       ***
        *** IN/OUT ARGS :  This function has zero input/output arguments. ***
        *** RETURN      :  This function returns a bool value.            ***
        *********************************************************************/
        public bool Insert(Node nodeValue)
        {
            return binarySearchTree.Add(nodeValue);
        }

        /********************************************************************
        *** FUNCTION    : Search Function                                 ***
        *** DESCRIPTION : This function searches the BST for a node that  ***
        ***               contains a specific symbol.                     ***
        *** INPUT ARGS  : string symbol                                   ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns a Node value.             ***
        *********************************************************************/
        public Node Search(string symbol)
        {
            if(symbol.Length > 4)
            {
                symbol = symbol.Substring(0, 4);
            }
            symbol = symbol.ToUpper();

            return binarySearchTree.FirstOrDefault(node => node.Symbol == symbol);
        }

        /********************************************************************
        *** FUNCTION    : View Function                                   ***
        *** DESCRIPTION : This function displays the BST to the user in a ***
        ***               table format.                                   ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing (void).           ***
        *********************************************************************/
        public void View()
        {
            int counter = 0;

            Console.WriteLine("\n+----------------------------------------------------------------+");
            Console.WriteLine("| Symbol        | Value     | RFlag      | IFlag     | MFlag     |");
            Console.WriteLine("+----------------------------------------------------------------+");
            foreach (Node currentNode in binarySearchTree)
            {
                Console.WriteLine(string.Format("{0, 0} {1, -13} | {2, -9} | {3, -10} | {4, -9} | {5, -6}    |", "|", currentNode.Symbol,
                    currentNode.Value.ToString(), currentNode.Rflag.ToString(), currentNode.Iflag.ToString(), currentNode.Mflag.ToString()));

                // Loop through and check to see if at 20 line max
                counter++;
                if (counter == 20)
                {
                    counter = 0;
                    Console.WriteLine("\n--- Press Enter to view more lines ---");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("+----------------------------------------------------------------+");
        }
    }
}