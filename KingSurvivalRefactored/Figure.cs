using System;

namespace KingSurvival
{
    public abstract class Figure
    {
        private char symbol;

        private readonly Position position;

        public Figure(Position position, char symbol)
        {
            this.Position = position;
            this.Symbol = symbol;
        }

        public char Symbol
        {
            get
            {
                return this.symbol;
            }
            protected set
            {
                this.symbol = value;
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
                throw new NotImplementedException();
            }
        }
    }
}