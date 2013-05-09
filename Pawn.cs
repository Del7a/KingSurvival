using System;

namespace KingSurvival
{
    public class Pawn : Figure
    {
        public char Symbol { get; set; }

        public override bool TryMove(int rowIncrement, int colIncrement)
        {
            throw new NotImplementedException();
        }
    }
}
