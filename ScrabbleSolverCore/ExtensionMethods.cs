using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleSolver
{
    public static class ExtensionMethods
    {
        private static const string VALID_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static bool IsValidScrabbleLetter(this char character)
        {
            return VALID_LETTERS.Contains(character);
        }
    }
}
