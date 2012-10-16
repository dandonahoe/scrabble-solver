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
                if (args.Length != 2)
                {
                    Console.WriteLine("ScrabbleSolver.exe [path to game board] [letters, no spaces]");
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
                char[] playableLetters = args[1].ToCharArray();

                // Initalize the game board
                var gameBoard = new GameBoard(boardFileName);

                // calculate the best move
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
