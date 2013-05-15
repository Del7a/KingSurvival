using System;

namespace KingSurvival
{
    public class Pawn : Figure
    {
        public Pawn(Position position, char symbol) : base(position, symbol)
        {
            this.AvailableMoves = new bool[] { true, true };
        }
    }
}