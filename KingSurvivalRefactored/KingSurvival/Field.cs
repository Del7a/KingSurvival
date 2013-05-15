using System;
using System.Collections.Generic;

namespace KingSurvival
{
    public class Field
    {
        private static readonly Position topLeft = new Position(PaddingHeight, PaddingWidth);
        private static readonly Position topRight = new Position(PaddingHeight, (PaddingWidth + 2 * Width) - 2);// 2 is for the right padding
        private static readonly Position bottomLeft = new Position(Height + PaddingHeight - 1, PaddingWidth);//-1 because it is 0 based
        private static readonly Position bottomRight = new Position(Height + PaddingHeight - 1, (PaddingWidth + 2 * Width) - 2);

        private const int Height = 8;
        private const int Width = 8;
        private const int PaddingHeight = 2;
        private const int PaddingWidth = 4;
        private const int PaddingBetweenFigures = TotalWidth / Width;//try with different widths
        private const int TotalHeight = Height + 2 * PaddingHeight;
        private const int TotalWidth = 2 * Width + 2 * PaddingWidth - 1;

        private char[,] field;

        public Field(List<Figure> figures)
        {
            this.InitializeGameField(figures);
        }

        public char this[int i, int j]
        {
            get
            {
                return this.field[i, j];
            }
            set
            {
                this.field[i, j] = value;
            }
        }

        public bool IsInPlayField(Position position)
        {
            int positonRow = position.Row;
            bool isRowInBoard = (positonRow >= topLeft.Row) && (positonRow <= bottomLeft.Row);
            int positonCol = position.Col;
            bool isColInBoard = (positonCol >= topLeft.Col) && (positonCol <= topRight.Col);
            bool isValidPosition = isRowInBoard && isColInBoard;

            return isValidPosition;
        }

        public void DrawGameBoard()
        {
            Console.WriteLine();
            for (int row = 0; row < this.field.GetLength(0); row++)
            {
                for (int col = 0; col < this.field.GetLength(1); col++)
                {
                    Position positionCell = new Position(row, col);
                    bool isCellIn = this.IsInPlayField(positionCell);
                    if (isCellIn)
                    {
                        if (row % 2 == 0)
                        {
                            if (col % 4 == 0)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(this.field[row, col]);
                                Console.ResetColor();
                            }
                            else if (col % 2 == 0)
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(this.field[row, col]);
                                Console.ResetColor();
                            }
                            else //if (col % 2 != 0)
                            {
                                Console.Write(this.field[row, col]);
                            }
                        }
                        else if (col % 4 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(this.field[row, col]);
                            Console.ResetColor();
                        }
                        else if (col % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(this.field[row, col]);
                            Console.ResetColor();
                        }
                        else //if (col % 2 != 0)
                        {
                            Console.Write(this.field[row, col]);
                        }
                    }
                    else
                    {
                        Console.Write(this.field[row, col]);
                    }
                }
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private void InitializeGameField(List<Figure> figures)
        {
            this.field = new char[TotalHeight, TotalWidth];

            //all cells are empty now
            for (int row = 0; row < TotalHeight; row++)
            {
                for (int col = 0; col < TotalWidth; col++)
                {
                    this.field[row, col] = ' ';
                }
            }

            //put figures
            foreach (var figure in figures)
            {
                figure.Position.Row += PaddingHeight;
                figure.Position.Col *= PaddingBetweenFigures;
                figure.Position.Col += PaddingWidth;

                if (this.IsInPlayField(figure.Position))
                {
                    this.field[figure.Position.Row, figure.Position.Col] = figure.Symbol;
                }
                else
                {
                    throw new ArgumentException("Invalid figure position!");
                }
            }

            //left and right borders
            for (int row = PaddingHeight; row <= TotalHeight - PaddingHeight; row++)
            {
                this.field[row, PaddingWidth - 2] = '|';
                this.field[row, TotalWidth - PaddingWidth + 1] = '|';
            }

            //top and bottom borders
            for (int col = PaddingWidth; col < TotalWidth - PaddingWidth; col++)
            {
                this.field[PaddingHeight - 1, col] = '_';
                this.field[Height + PaddingHeight, col] = '_';
            }

            //left and right numerations
            int rowNumber = 1;
            for (int row = PaddingHeight; row < PaddingHeight + Height; row++)
            {
                this.field[row, 0] = rowNumber.ToString()[0];
                rowNumber++;
            }

            //top and bottom numerations
            int colNumber = 1;
            for (int col = PaddingWidth; col < TotalWidth - PaddingWidth; col += PaddingBetweenFigures)
            {
                this.field[0, col] = colNumber.ToString()[0];
                colNumber++;
            }
        }
    }
}