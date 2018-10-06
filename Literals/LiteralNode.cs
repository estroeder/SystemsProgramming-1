/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 1 - Symbol Table                      ***
*** DUE DATE   : 09/12/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This LiteralNode.cs file contains functions     ***
***               that will perform actions on a literal node.    ***
********************************************************************/

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : LiteralNode Class                                  ***
    *** DESCRIPTION : This class consists of all the functions that   ***
    ***               are used to perform actions on a Literal node.  ***
    *********************************************************************/
    class LiteralNode
    {
        public string Name;
        public string Value;
        public int Length;
        public int Address;

        /********************************************************************
        *** FUNCTION    : Literal Node Function                           ***
        *** DESCRIPTION : This functions sets the name, value, length,    ***
        ***               and address variables in a literal node.        ***
        *** INPUT ARGS  : This function has zero input arguments.         ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public LiteralNode(string name, string value, int length, int address)
        {
            Name = name;
            Value = value;
            Length = length;
            Address = address;
        }
    }
}