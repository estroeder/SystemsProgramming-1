/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 1 - Symbol Table                      ***
*** DUE DATE   : 09/12/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This ExpressionNode.cs file contains functions  ***
***               that will perform actions on an expression node.***
********************************************************************/

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : ExpressionNode Class                               ***
    *** DESCRIPTION : This class consists of all the functions that   ***
    ***               are used to perform actions on an expression    ***
    ***               node.                                           ***
    *********************************************************************/
    class ExpressionNode
    {
        public string expression;
        public int value;
        public bool relocatable = false;
        public bool direct = false;
        public bool indirect = false;
        public bool immediate = false;
        public bool indexed = false;

        /********************************************************************
        *** FUNCTION    : Expression Node Function                        ***
        *** DESCRIPTION : This functions sets the expression, value,      ***
        ***               relocatable, direct, indirect, immediate, and   ***
        ***               indexed variables in an expression node.        ***
        *** INPUT ARGS  : string inExpression, int inValue,               ***
        ***               bool inRelocatable, bool inDirect,              ***
        ***               bool inIndirect, bool inImmediate,              ***
        ***               bool inIndexed                                  ***
        *** OUTPUT ARGS : This function has zero output arguments.        ***
        *** IN/OUT ARGS : This function has zero input/output arguments.  ***
        *** RETURN      : This function returns nothing.                  ***
        *********************************************************************/
        public ExpressionNode(string inExpression, int inValue, bool inRelocatable, bool inDirect, bool inIndirect, bool inImmediate, bool inIndexed)
        {
            expression = inExpression;
            value = inValue;
            relocatable = inRelocatable;
            direct = inDirect;
            indirect = inIndirect;
            immediate = inImmediate;
            indexed = inIndexed;
        }
    }
}
