using System;

namespace KingSurvival
{
    public class King : Figure
    {
        private static readonly char kingSymbol = 'K';

        public King(Position position) : base(position, kingSymbol)
        {
            this.AvailableMoves = new bool[] { true, true, true, true };
        }

    }
}