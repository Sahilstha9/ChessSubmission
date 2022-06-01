using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class TextGame
    {
        private Board _main;
        public TextGame(Board b)
        {
            _main = b;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        IHavePosition _toDraw = new EmptySquare(j, i / 2); ;
                        foreach (PieceManager p in _main.GameBoard)
                        {
                            if (p.IsEqual(_toDraw))
                                _toDraw = p;
                        }
                        Console.Write("| ");
                        if (_toDraw is PieceManager)
                            Console.Write((_toDraw as PieceManager).Identifier + " ");
                        else
                            Console.Write("    ");
                    }
                    else
                        Console.Write("______");
                }
                if (i % 2 == 0)
                    Console.Write("|  " + i / 2);
                else
                    Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
                Console.Write("   " + i + "  ");
        }

        public void Move(string input, Player wP, Player bP)
        {
            if (input.Length != 5)
                Console.WriteLine("Invalid Command!");
            else
            {
                PieceManager piece = null;
                foreach (PieceManager p in _main.GameBoard)
                {
                    if (p.AreYou(input[0].ToString() + input[1].ToString() + input[2].ToString()))
                        piece = p;
                }
                if (piece is null)
                    Console.WriteLine("No such Piece found");
                else
                {
                    int x = Int32.Parse(input[3].ToString());
                    int y = Int32.Parse(input[4].ToString());
                    if (piece.Player.MovePiece(piece, x, y))
                    {
                        wP.Turn = !wP.Turn;
                        bP.Turn = !bP.Turn;
                        Console.Clear();
                        PrintBoard();
                    }
                    else
                        Console.WriteLine("Invalid Move!!");
                }
            }
        }
    }
}
