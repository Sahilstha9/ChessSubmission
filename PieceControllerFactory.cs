using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PieceManagerFactory
    {
        private List<PieceManager> _board;
        public PieceManagerFactory(Game game, List<PieceManager> board)
        {
            _board = board;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (game)
                    {
                        case Game.Chess:
                            InitialiseChessPieces(i, j);
                            break;
                        case Game.Checker:
                            InitialiseCheckerPieces(i, j);
                            break;
                        case Game.Mix:
                            InitialiseMixPieces(i, j);
                            break;
                    }
                }
            }
        }

        private void InitialiseChessPieces(int i, int j)
        {
            if (j == 1)
                _board.Add(new PieceManager(i, j, false, _board, PieceType.Pawn));
            else if (j == 6)
                _board.Add(new PieceManager(i, j, true, _board, PieceType.Pawn));
            else if ((j == 0) && (i == 0 || i == 7))
                _board.Add(new PieceManager(i, j, false, _board, PieceType.Rook));
            else if ((j == 7) && (i == 0 || i == 7))
                _board.Add(new PieceManager(i, j, true, _board, PieceType.Rook));
            else if ((j == 0) && (i == 1 || i == 6))
                _board.Add(new PieceManager(i, j, false, _board, PieceType.Knight));
            else if ((j == 7) && (i == 1 || i == 6))
                _board.Add(new PieceManager(i, j, true, _board, PieceType.Knight));
            else if ((j == 0) && (i == 2 || i == 5))
                _board.Add(new PieceManager(i, j, false, _board, PieceType.Bishop));
            else if ((j == 7) && (i == 2 || i == 5))
                _board.Add(new PieceManager(i, j, true, _board, PieceType.Bishop));
            else if (j == 0 && i == 3)
                _board.Add(new PieceManager(i, j, false, _board, PieceType.Queen));
            else if (j == 7 && i == 3)
                _board.Add(new PieceManager(i, j, true, _board, PieceType.Queen));
            else if (j == 0 && i == 4)
                _board.Add(new PieceManager(i, j, false, _board, PieceType.King));
            else if (j == 7 && i == 4)
                _board.Add(new PieceManager(i, j, true, _board, PieceType.King));
        }

        //Initialises board with checker pieces
        private void InitialiseCheckerPieces(int i, int j)
        {
            if (j < 3)
            {
                if (j % 2 == 0)
                {
                    if (i % 2 == 1)
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Checker));
                }
                else
                {
                    if (i % 2 == 0)
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Checker));
                }
            }
            else if (j > 4)
            {
                if (j % 2 == 0)
                {
                    if (i % 2 == 1)
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Checker));
                }
                else
                {
                    if (i % 2 == 0)
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Checker));
                }
            }
        }

        private void InitialiseMixPieces(int i, int j)
        {
            if (j < 2)
            {
                int index = 0;
                Random rnd = new Random();
                index = rnd.Next(8);
                switch (index)
                {
                    case 0:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Pawn));
                        break;
                    case 1:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Rook));
                        break;
                    case 2:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Knight));
                        break;
                    case 3:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Bishop));
                        break;
                    case 4:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Queen));
                        break;
                    case 5:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.Checker));
                        break;
                    case 6:
                        _board.Add(new PieceManager(i, j, false, _board, PieceType.KingChecker));
                        break;
                }
            }
            else if (j > 5)
            {
                int index = 0;
                Random rnd = new Random();
                index = rnd.Next(8);
                switch (index)
                {
                    case 0:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Pawn));
                        break;
                    case 1:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Rook));
                        break;
                    case 2:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Knight));
                        break;
                    case 3:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Bishop));
                        break;
                    case 4:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Queen));
                        break;
                    case 5:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.Checker));
                        break;
                    case 6:
                        _board.Add(new PieceManager(i, j, true, _board, PieceType.KingChecker));
                        break;
                }
            }
        }
    }
}
