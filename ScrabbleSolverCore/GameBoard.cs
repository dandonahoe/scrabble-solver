using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ScrabbleSolver
{
    class GameBoard
    {
        public GameBoard(string pathToGameBoard)
        {
            var fileInfo = new FileInfo(pathToGameBoard);

            if (fileInfo.Exists == false)
                throw new PrettyException("The path '{0} could not be found'", pathToGameBoard);

            string fileContent = string.Empty;

            using (var gameBoardFileStream = new StreamReader(fileInfo.OpenRead()))
            {
                fileContent = gameBoardFileStream.ReadToEnd();
                gameBoardFileStream.Close();
            }

            string[] boardLines = fileContent.Split("\r\n", StringSplitOptions.None);

            if (boardLines.Length != 15)
                throw new PrettyException("Game board file must contain exactly 15 lines and columns");
        }

        /// <summary>
        /// Standard dimensions for a Scrabble board
        /// </summary>
        private const byte BOARD_WIDTH  = 15;
        private const byte BOARD_HEIGHT = 15;

        /// <summary>
        /// Maintains the current state of the board. Characters correspond to their letter and
        /// null values indicate no letter has been played on that tile
        /// </summary>
        private char[,] _board = new char[BOARD_WIDTH, BOARD_HEIGHT];

        /// <summary>
        /// A list of currently pending placed letters on the board
        /// </summary>
        //private List<LetterPosition> _turnCharacters = new List<LetterPosition>();


        /// <summary>
        /// Add a new
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void addLetter(LetterPosition letterPosition)
        {
            if (letterPosition.X < 0 || letterPosition.Y > BOARD_WIDTH)
                throw new PrettyException("The value {0} must be in the range of 0-14", letterPosition.X);

            if (letterPosition.Y < 0 || letterPosition.Y > BOARD_WIDTH)
                throw new PrettyException("The value {0} must be in the range of 0-14", letterPosition.Y);

            if (_board[letterPosition.X, letterPosition.Y] != ' ')
                throw new PrettyException("The tile [{0},{1}] already contains a letter ({2})", 
                    letterPosition.X, letterPosition.Y, _board[letterPosition.X, letterPosition.Y]);

            if (letterPosition.Letter.IsValidScrabbleLetter() == false)
                throw new PrettyException("The character '{0} is not a valid Scrabble character", letterPosition.Letter);

            _board[letterPosition.X, letterPosition.Y] = letterPosition.Letter;

            return;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="playableLetters"></param>
        public void calculateBestMove(params char[] playableLetters)
        {
        }

        public void debugRowTest()
        {
            var rowFragments = new List<WordSegment>();
            WordSegment currentFragment = null;

            // Starting with the first row
            for (int x = 0; x < 15; x++)
            {
                char? character = _board[0, x];

                if (character == null)
                {
                    if (currentFragment != null)
                    {
                        rowFragments.Add(currentFragment);
                    }

                    currentFragment = null;
                    continue;
                }

                if (currentFragment != null)
                {
                    currentFragment.Letters += _board[6, x];
                    continue;
                }
                else
                {
                    currentFragment = new WordSegment
                    {
                        StartPos = x,
                        Letters = _board[6, x].ToString()
                    };
                }
            }
        }
    }
}
