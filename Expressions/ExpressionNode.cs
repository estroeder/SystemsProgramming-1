namespace TroyerA2
{
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
        *** FUNCTION    : ExpressionNode                                  ***
        *********************************************************************
        *** DESCRIPTION : The Expression Method is the constructor for    ***
        ***               the ExpresionNode class.                        ***
        *********************************************************************
        *** INPUT ARGS  : inExpression, inValue, inRelocatable, inDirect, ***
        ***               inInderect, inImmediate, inIndexed              ***
        *** OUTPUT ARGS : N/A											  ***
        *** IN/OUT ARGS : N/A											  ***
        *** RETURN      : N/A                                             ***
        ********************************************************************/
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
