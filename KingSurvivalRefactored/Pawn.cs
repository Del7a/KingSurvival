using System;

namespace KingSurvival
{
    public class Pawn : Figure
    {
        private bool[] availableMoves;
        
        public Pawn(Position position, char symbol)
            : base(position, symbol)
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
