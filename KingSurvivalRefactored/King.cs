using System;

namespace KingSurvival
{
    public class King : Figure
    {
        private readonly char kingSymbol = 'K';

        private bool[] availableMoves;

        public King(Position position)
            : base(position, kingSymbol)
        { 
            
        }

        public bool[] AvailableMoves
        {
            get
            {
                return this.availableMoves;
            }
            set
            {
                this.availableMoves = value;
            }
        }
    }
}
