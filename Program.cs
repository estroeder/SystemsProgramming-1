/********************************************************************
*** NAME       : Tucker Troyer                                    ***
*** CLASS      : CSc 354                                          ***
*** ASSIGNMENT : Assignment 2 - Expressions Processing            ***
*** DUE DATE   : 10/03/2018                                       ***
*** INSTRUCTOR : Gamradt                                          ***
*********************************************************************
*** DESCRIPTION : This Program.cs file is the main driver         ***
***               file of the program.                            ***
********************************************************************/

using System;

namespace SystemsProgramming
{
    /********************************************************************
    *** CLASS    : Program Class                                      ***
    *** DESCRIPTION : This class is the main driver class of the      ***
    ***               program and calls all the other classes and     ***
    ***               functions in the program.                       ***
    *********************************************************************/
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree binarySearchTree = new BinarySearchTree();
            PopulateSymbolTable symbolTable = new PopulateSymbolTable();
            PopulateExpressionTable expressionTable = new PopulateExpressionTable();
            ExpressionsLinkedList expressionsLinkedList = new ExpressionsLinkedList();
            LiteralsLinkedList literalsLinkedList = new LiteralsLinkedList();
            
            // 1. Read from SYMS.DAT file and populate the symbol table
            binarySearchTree = symbolTable.CreateBinarySearchTreeFromDataFile();

            // 2. Create expressions
            if(args.Length > 0)
            {
                expressionTable.ProcessFile(expressionsLinkedList, literalsLinkedList, binarySearchTree, args[0]);
            }
            else
            {
                expressionTable.ProcessFile(expressionsLinkedList, literalsLinkedList, binarySearchTree);
            }

            Console.ReadKey();

            // 3. Display expression data
            expressionsLinkedList.Display();

            Console.ReadKey();

            // 4. Display literal error data
            literalsLinkedList.DisplayErrors();

            Console.ReadKey();

            // 5. Display literal data
            literalsLinkedList.Display();

            Console.ReadKey();
        }
    }
}
