/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 1 - Symbol Table                      ***
*** DUE DATE   : 09/12/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This Node.cs file contains functions that will  ***
***               perform actions on a Node.                      ***
********************************************************************/

using System;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Node Class                                         ***
    *** DESCRIPTION : This class consists of all the functions that   ***
    ***               are used to perform actions on a Node.          ***
    *********************************************************************/
    public class Node : IComparable<Node>
    {
        // Properties (Getters, Setters)
        public string Symbol { get; set; }
        public int Value { get; set; }
        public bool Rflag { get; set; }
        public bool Iflag { get; set; }
        public bool Mflag { get; set; }

        /********************************************************************
        *** FUNCTION    : Node Function                                   ***
        *** DESCRIPTION : Function that sets Iflag in node to true and    ***
        ***               the Mflag in node to false.                     ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public Node()
        {
            Iflag = true;
            Mflag = false;

            return;
        }

        /********************************************************************
        *** FUNCTION    : Compare To Function                             ***
        *** DESCRIPTION : Compares the nodes in the Binary Search Tree    ***
        ***               so that duplicate node symbol values are        ***
        ***               excluded.                                       ***
        *** INPUT ARGS  : Node nodeToCompare                              ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : int                                             ***
        *********************************************************************/
        public int CompareTo(Node nodeToCompare)
        {
            return (string.Compare(this.Symbol, nodeToCompare.Symbol, StringComparison.Ordinal));
        }
    }
}
