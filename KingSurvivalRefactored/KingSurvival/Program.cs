using System;
using System.Collections.Generic;

namespace KingSurvival
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figure> figures = new List<Figure>();
            Pawn pawn1 = new Pawn(new Position(0, 0), 'A');
            Pawn pawn2 = new Pawn(new Position(0, 2), 'B');
            Pawn pawn3 = new Pawn(new Position(0, 4), 'C');
            Pawn pawn4 = new Pawn(new Position(0, 6), 'D');
            King king = new King(new Position(7, 3));

            figures.Add(pawn1);
            figures.Add(pawn2);
            figures.Add(pawn3);
            figures.Add(pawn4);
            figures.Add(king);

            Field board = new Field(figures);

            ConsoleEngine engine = new ConsoleEngine(board, figures);
            engine.Run();
            Console.WriteLine("Game Ended. New one???");
            Console.WriteLine("Y/N");
            bool newGame = false;
            while (true)
            {
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    newGame = true;
                }
                else if (newGame == true)
                {
                    engine = new ConsoleEngine(board, figures);
                }
                else
                {
                    break;
                }
            }
        }
    }
}