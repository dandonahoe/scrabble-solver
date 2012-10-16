using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleSolver
{
    class LetterPosition
    {
        // The letter being played
        public char Letter { get; set; }

        /// <summary>
        /// Absolute X and Y coordinates where letter
        /// will be placed on the gameboard
        /// </summary>
        public byte X { get; set; }
        public byte Y { get; set; }

        // If this letter 
        public bool IsWildcard;
    }
}
