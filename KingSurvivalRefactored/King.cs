using System;

namespace KingSurvival
{
    public class King : Figure
    {
        private readonly char kingSymbol = 'K';

        public King(Position position) : base(position, this.kingSymbol)
        { 
        }

        public bool[] AvailableMoves { get; set; }
    }
}