using System;

namespace KingSurvival
{
    public abstract class Figure
    {
        public Figure(Position position, char symbol)
        {
            this.Position = position;
            this.Symbol = symbol;
        }

        public char Symbol { get; protected set; }

        public bool[] AvailableMoves { get; set; }

        public Position Position { get; set; }
    }
}