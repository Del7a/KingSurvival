using System;

namespace KingSurvival
{
    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public static Position operator +(Position left, Position right)
        {
            Position newPosition = new Position(0, 0);
            newPosition.Row = left.Row + right.Row;
            newPosition.Col = left.Col + right.Col;

            return newPosition;
        }
    }
}
