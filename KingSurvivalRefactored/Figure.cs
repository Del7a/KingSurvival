using System;

namespace KingSurvival
{
    public abstract class Figure
    {
        Position position;

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

        public abstract bool TryMove(int rowIncrement, int colIncrement);
    }
}
