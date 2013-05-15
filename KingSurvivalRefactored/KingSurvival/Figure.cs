using System;

namespace KingSurvival
{
    public abstract class Figure
    {
        private Position position;
        private bool[] availableMoves;

        public Figure(Position position, char symbol)
        {
            this.Position = position;
            this.Symbol = symbol;
        }

        public char Symbol { get; protected set; }

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

        public Position Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
    }
}