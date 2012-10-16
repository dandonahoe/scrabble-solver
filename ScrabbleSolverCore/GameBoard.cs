using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ScrabbleSolver
{
    class GameBoard
    {
        /// <summary>
        /// Standard dimensions for a Scrabble board
        /// </summary>
        private const byte BOARD_WIDTH = 15;
        private const byte BOARD_HEIGHT = 15;

        /// <summary>
        /// Maintains the current state of the board. Characters correspond to their letter and
        /// null values indicate no letter has been played on that tile
        /// </summary>
        private char[,] _board = new char[BOARD_WIDTH, BOARD_HEIGHT];


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToGameBoard"></param>
        public GameBoard(string pathToGameBoard)
        {
            var fileInfo = new FileInfo(pathToGameBoard);

            // if we can't find the file, no point in continuing
            if (fileInfo.Exists == false)
                throw new PrettyException("The path '{0} could not be found'", pathToGameBoard);

            string fileContent = string.Empty;

            // read all game data and close the file out
            using (var gameBoardFileStream = new StreamReader(fileInfo.OpenRead()))
            {
                fileContent = gameBoardFileStream.ReadToEnd();
                gameBoardFileStream.Close();
            }

            // split it based on row data
            string[] boardLines = fileContent.Split(new string[] { "\r\n"}, StringSplitOptions.None);

            // header row + 15 rows for tile fata
            if (boardLines.Length != 16)
                throw new PrettyException("Game board file must contain exactly 15 lines");

            // don't bother processing the header row
            for (int rowNum = 1; rowNum < boardLines.Length; rowNum++)
                ProcessRow(rowNum, boardLines[rowNum]);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="rowData"></param>
        private void ProcessRow(int rowNum, string rowData)
        {
            char[] tileValues = TokenizeRowData(rowNum, rowData);

            for (int a = 0; a < tileValues.Length; a++)
                _board[a, rowNum - 1] = tileValues[a];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private char[] TokenizeRowData(int rowNum, string rowData)
        {
            // The row in the file is one more than the logical row of the board
            string rowPrefix = (rowNum - 1) + "\t";

            // each row must start with the correct row number
            if(rowData.StartsWith(rowPrefix) == false)
                throw new PrettyException("File row {0} does not start with the correct number followed by a tab", rowNum);


            var rowCharacters = new char[15];
            int tilesRead = -1;

            // Starting with the first character after the row prefix
            // read the rest of the line. Each tile is either represented
            // by a single tab (blank space) or a letter followed by a tab
            for (int a = rowPrefix.Length; a < rowData.Length; a++)
            {
                tilesRead++;

                // Convert mixed case to upper rather than failing
                char currentChar = char.ToUpper(rowData[a]);

                // if the first character in this tile is a
                // tab, then it's a blank character
                if (currentChar == '\t')
                {
                    rowCharacters[tilesRead] = ' ';

                    continue;
                }

                // if the specified character isnt * or A-Z, fail
                if (currentChar.IsValidScrabbleLetter() == false)
                    throw new PrettyException("Character '{0} on board is not a valid Scrabble character. Allowed characters are * or A-Z", currentChar);

                // save
                rowCharacters[tilesRead] = currentChar;

                // skip the following tab character. 
                a++;
            }

            return rowCharacters;
        }


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
            var possiblePlays = new List<object>();

            for (int col = 0; col < BOARD_WIDTH; col++)
            {
                possiblePlays.AddRange(AnalyzePossiblePlays(GetLettersFromRow(col)));
            }

            for (int row = 0; row < BOARD_HEIGHT; row++)
            {
                possiblePlays.AddRange(AnalyzePossiblePlays(GetLettersFromCol(row)));
            }

            return;
        }

        private List<object> AnalyzePossiblePlays(char[] lineLetters)
        {
            var possiblePlays = new List<object>();

            return possiblePlays;
        }

        private List<object> AnalyzeCharacterString(char[] tileLetters)
        {
            var possiblePlays = new List<object>();

            return possiblePlays;
        }

        private char[] GetLettersFromCol(int col)
        {
            var tileLetters = new char[BOARD_HEIGHT];

            for (int a = 0; a < BOARD_HEIGHT; a++)
                tileLetters[a] = _board[col, a];

            return tileLetters;
        }

        private char[] GetLettersFromRow(int row)
        {
            var tileLetters = new char[BOARD_WIDTH];

            for (int a = 0; a < BOARD_WIDTH; a++)
                tileLetters[a] = _board[a, row];

            return tileLetters;
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
