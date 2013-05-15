using System;
using System.Collections.Generic;

namespace KingSurvival
{
    public class Game
    {
        public const int FieldDimention = 8;
        readonly List<Pawn> pawns = new List<Pawn>();
        readonly King king = new King(new Position(0, 0));
        private readonly int turnsCounter = 0;

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void PrintField()
        {
            throw new NotImplementedException();
        }

        public void CommandPrompt(bool isKingTurn)
        {
            throw new NotImplementedException();
        }

        public void KingTurn(string command)
        {
            throw new NotImplementedException();
        }

        public void PawnTurn(string command)
        {
            throw new NotImplementedException();
        }
    }
}