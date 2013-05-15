using System;
using System.Collections.Generic;
using System.Linq;

namespace KingSurvival
{
    public class ConsoleEngine
    {
        private readonly Field gameBoard;
        private readonly List<Figure> figures;
        private readonly List<char> charRepresentationsPawns;
        private readonly char kingSymbol = 'K';
        private int moveCounter = 0;
        private bool gameIsInProgress = true;
        private bool kingHasAvailableMoves = false;
        private bool pawnsHaveAvailableMoves = false;

        public ConsoleEngine(Field gameBoard, List<Figure> figures)
        {
            this.gameBoard = gameBoard;
            this.figures = figures;
            this.charRepresentationsPawns = new List<char>();

            foreach (var figure in this.figures)
            {
                if (figure is Pawn)
                {
                    this.charRepresentationsPawns.Add(figure.Symbol);
                }
            }
        }
        
        internal int MoveCounter { get; set; }

        internal bool GameIsInProgress { get; set; }

        internal bool KingHasAvailableMoves { get; set; }

        internal bool PawnsHaveAvailableMoves { get; set; }

        public void Run()
        {
            while (this.gameIsInProgress)
            {
                if (this.moveCounter % 2 == 0)
                {
                    this.gameBoard.DrawGameBoard();
                    this.ProcessKing();
                }
                else
                {
                    this.gameBoard.DrawGameBoard();
                    this.ProcessPawn();
                }
            }
        }

        public void ProcessKing()
        {
            this.ProcessASide("King");
        }

        public void ProcessPawn()
        {
            this.ProcessASide("Pawn");
        }

        internal void ProcessASide(string side)
        {
            bool isValidCommand = false;
            while (!isValidCommand)
            {
                if (side == "King")
                {
                    //Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Please enter king's turn: ");
                }
                else if (side == "Pawn")
                {
                    //Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("Please enter pawn's turn: ");
                }

                Console.ResetColor();
                string input = Console.ReadLine();

                if (input != null)
                {
                    input = input.ToUpper();
                    isValidCommand = this.ValidateCommand(input);
                    if (isValidCommand)
                    {
                        this.ProcessCommand(input);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command name!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    isValidCommand = false;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Please enter a valid command!");
                    Console.ResetColor();
                }
            }
            Console.Clear();
        }

        internal void CheckForFiguresBlocked()
        {
            if (!this.pawnsHaveAvailableMoves)
            {
                Console.WriteLine("End!");
                Console.WriteLine("All pawns are blocked! King wins in {0} moves!", (this.moveCounter / 2) + 1); //added one for the last move
                this.gameIsInProgress = false;
            }

            if (!this.kingHasAvailableMoves)
            {
                Console.WriteLine("King is blocked! King loses in {0} moves!", (this.moveCounter / 2) + 1);
                this.gameIsInProgress = false;
            }
        }

        internal void CheckForKingExit(int currentKingRow)
        {
            if (currentKingRow == 2) //actually gameBoard.HeightPadding
            {
                Console.WriteLine("End!");
                Console.WriteLine("King wins in {0} moves!", (this.moveCounter / 2) + 1); //added one for the last move
                this.gameIsInProgress = false;
            }
        }

        internal bool ValidateCommand(string commandToCheck)
        {
            bool commandIsValid = false;

            if (this.moveCounter % 2 == 0) //king's move
            {
                if (commandToCheck == string.Format("{0}{1}", this.kingSymbol, Direction.DL.ToString()) ||
                    commandToCheck == string.Format("{0}{1}", this.kingSymbol, Direction.DR.ToString()) ||
                    commandToCheck == string.Format("{0}{1}", this.kingSymbol, Direction.UL.ToString()) ||
                    commandToCheck == string.Format("{0}{1}", this.kingSymbol, Direction.UR.ToString()))
                {
                    commandIsValid = true;
                }
            }
            else if (this.moveCounter % 2 != 0) //pawn's move
            {
                foreach (var figureChar in this.charRepresentationsPawns)
                {
                    if (commandToCheck == string.Format("{0}{1}", figureChar, Direction.DL.ToString()) ||
                        commandToCheck == string.Format("{0}{1}", figureChar, Direction.DR.ToString()))
                    {
                        commandIsValid = true;
                        break;
                    }
                }
            }

            return commandIsValid;
        }

        private void ProcessCommand(string input)
        {
            char figureLetter = input[0];
            Figure currentFigure = null;
            foreach (var figure in this.figures)
            {
                if (figure.Symbol == figureLetter)
                {
                    currentFigure = figure;
                }
            }

            string commandDirection = input.Substring(1, 2);
            Direction direction = this.GetDirection(commandDirection);
            Position currentPosition = currentFigure.Position;

            while (currentPosition != null)
            {
                currentPosition = this.GetNewCoordinates(currentFigure, direction); //returns null for invalid coordinates
                if (currentPosition != null) //we found valid coordinates
                {
                    this.UpdateGameField(currentFigure, direction); // this moves the char
                    currentFigure.Position = currentPosition; // this changes the position
                    this.UpdateAllAvailableMoves(); // we moved a figure and update available moves for all figures
                    this.SetFiguresHaveAvailableMoves();
                    this.CheckForFiguresBlocked();
                    break;
                }
            }
        }

        private Direction GetDirection(string commandDirection)
        {
            Direction direction = default(Direction);
            switch (commandDirection)
            {
                case "DL":
                    direction = Direction.DL;
                    break;
                case "DR":
                    direction = Direction.DR;
                    break;
                case "UL":
                    direction = Direction.UL;
                    break;
                case "UR":
                    direction = Direction.UR;
                    break;
                default:
                    break;
            }

            return direction;
        }

        /// <summary>
        /// Gets the displacement for figure movement
        /// </summary>
        private Position GetDisplacement(Direction direction)
        {
            Position displacement = null;
            switch (direction)
            {
                case Direction.DL:
                    displacement = new Position(1, -2);
                    break;
                case Direction.DR:
                    displacement = new Position(1, 2);
                    break;
                case Direction.UL:
                    displacement = new Position(-1, -2);
                    break;
                case Direction.UR:
                    displacement = new Position(-1, 2);
                    break;
                default:
                    break;
            }

            return displacement;
        }

        private bool IsValidGameBoardCell(Position position)
        {
            bool valid = this.gameBoard.IsInPlayField(position) && this.gameBoard[position.Row, position.Col] == ' ';
            return valid;
        }

        private Position GetNewCoordinates(Figure currentFigure, Direction direction)
        {
            Position currentPosition = currentFigure.Position;
            Position displacement = this.GetDisplacement(direction);
            Position newPosition = currentPosition + displacement;

            if (this.IsValidGameBoardCell(newPosition))
            {
                return newPosition;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("You can't move there!");
                Console.ResetColor();
                return null;
            }
        }

        private void UpdateGameField(Figure currentFigure, Direction direction)
        {
            Position oldPosition = currentFigure.Position;
            Position displacement = this.GetDisplacement(direction);
            Position newPosition = oldPosition + displacement;
            char sign = this.gameBoard[oldPosition.Row, oldPosition.Col];
            this.gameBoard[oldPosition.Row, oldPosition.Col] = ' ';
            this.gameBoard[newPosition.Row, newPosition.Col] = sign;
            this.moveCounter++; //we processed a valid command so the moves increment

            this.CheckForKingExit(newPosition.Row);
        }

        private void UpdateAllAvailableMoves()
        {
            foreach (Figure figure in this.figures)
            {
                if (figure.GetType() == typeof(Pawn))
                {
                    Position downLeft = figure.Position + this.GetDisplacement(Direction.DL);
                    Position downRight = figure.Position + this.GetDisplacement(Direction.DR);
                    Position[] neighbours = { downLeft, downRight };
                    for (int i = 0; i < neighbours.Length; i++)
                    {
                        if (!this.IsValidGameBoardCell(neighbours[i]))
                        {
                            (figure as Pawn).AvailableMoves[i] = false;
                        }
                        else
                        {
                            (figure as Pawn).AvailableMoves[i] = true;
                        }
                    }
                }
                else if (figure.GetType() == typeof(King))
                {
                    Position downLeft = figure.Position + this.GetDisplacement(Direction.DL);
                    Position downRight = figure.Position + this.GetDisplacement(Direction.DR);
                    Position upLeft = figure.Position + this.GetDisplacement(Direction.UL);
                    Position upRight = figure.Position + this.GetDisplacement(Direction.UR);
                    Position[] neighbours = { downLeft, downRight, upLeft, upRight };
                    for (int i = 0; i < neighbours.Length; i++)
                    {
                        if (!this.IsValidGameBoardCell(neighbours[i]))
                        {
                            (figure as King).AvailableMoves[i] = false;
                        }
                        else
                        {
                            (figure as King).AvailableMoves[i] = true;
                        }
                    }
                }
            }
        }

        private void SetFiguresHaveAvailableMoves()
        {
            var allPawns = from pawn in this.figures
                           where (pawn.GetType() == typeof(Pawn))
                           select pawn;
            this.pawnsHaveAvailableMoves = false;

            foreach (var pawn in allPawns)
            {
                foreach (var move in (pawn as Pawn).AvailableMoves)
                {
                    if (move == true)
                    {
                        this.pawnsHaveAvailableMoves = true;
                    }
                }
            }

            var kings = from king in this.figures
                        where (king.GetType() == typeof(King))
                        select king;

            this.kingHasAvailableMoves = false;
            foreach (var king in kings)
            {
                foreach (var move in (king as King).AvailableMoves)
                {
                    if (move == true)
                    {
                        this.kingHasAvailableMoves = true;
                    }
                }
            }
        }
    }
}