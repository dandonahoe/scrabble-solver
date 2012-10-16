using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleSolver
{
    class CharFragments
    {
        public int StartPos = 0;
        public string Letters = string.Empty;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[,] board = new string[15, 15] 
            { 
                {"", "", "", "",  "",  "",  "",  "",   "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "",  "",   "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "O",  "",  "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "B", "O", "D",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "G", "O", "O", "N",  "", "",  "", "",  "", "", ""},
                {"", "", "", "",  "O", "",  "E",  "",  "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "A", "",  "S",  "E",  "E",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "L", "",  "",  "",  "", "",  "", "",  "", "", ""},
                {"", "", "", "A", "I", "",  "",  "",  "", "",  "", "",  "", "", ""},
                {"", "", "", "R", "E", "",  "",  "",  "", "",  "", "",  "", "", ""},
                {"", "", "", "T", "",  "",  "",  "",  "", "",  "", "",  "", "", ""},
                {"", "", "", "",  "",  "",  "",  "", "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "",  "", "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "",  "", "",  "", "",  "", "",  "", ""},
                {"", "", "", "",  "",  "",  "",  "", "",  "", "",  "", "",  "", ""},
            };

            var rowFragments = new List<CharFragments>();
            CharFragments currentFragment = null;

            bool onWord = false;

            for (int x = 0; x < 15; x++)
            {
                string character = board[6, x];

                if (character == string.Empty)
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
                    currentFragment.Letters += board[6, x];
                    continue;
                }
                else
                {
                    currentFragment = new CharFragments
                    {
                        StartPos = x,
                        Letters = board[6, x]
                    };
                }
            }
        }
    }
}
