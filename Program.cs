using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TroyerA2
{
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

            // 4. Display literal data
            literalsLinkedList.DisplayErrors();

            Console.ReadKey();

            literalsLinkedList.Display();

            Console.ReadKey();
        }
    }
}
