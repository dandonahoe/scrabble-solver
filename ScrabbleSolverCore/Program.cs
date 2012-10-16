using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScrabbleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // must have at least one argument (the path to the game board)
                // 
                if (args.Length < 1)
                {
                    Console.WriteLine("ScrabbleSolver.exe [path to game board] [letters]");
                    return;
                }

                // verify that the game board file actually exists
                string boardFileName = args[0];

                if (File.Exists(boardFileName) == false)
                {
                    Console.WriteLine("File named '{0}' not found", boardFileName);
                    return;
                }

                // grab all the playable characters
                var playableLetters = new char[args.Length - 1];

                for (int a = 1; a < args.Length - 1; a++)
                    playableLetters[a - 1] = args[a].ToChar();

                var gameBoard = new GameBoard(boardFileName);

                gameBoard.calculateBestMove(playableLetters);
            }
            catch (PrettyException exc)
            {
                Console.WriteLine("Unrecoverable error: {0}", exc.Message);
            }
            catch(Exception exc)
            {
                Console.WriteLine("Unhandled exception occured: {0}", exc.Message);
            }
        }
    }
}
