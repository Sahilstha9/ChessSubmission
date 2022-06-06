using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PieceManagerFactory
    {
        public PieceManagerFactory()
        {
            
        }

        public List<PieceManager> InitalisePiece(Game game)
        {
            switch (game)
            {
                case Game.Chess:
                    return InitialiseChessPieces();
                    break;
                case Game.Checker:
                    return InitialiseCheckerPieces();
                    break;
                case Game.Mix:
                    return InitialiseMixPieces();
                    break;
            }
            return new List<PieceManager>();
        }

        private List<PieceManager> InitialiseChessPieces()
        {
            List<PieceManager> _board = new List<PieceManager>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 1)
                        _board.Add(new PieceManager(i, j, false, PieceType.Pawn));
                    else if (j == 6)
                        _board.Add(new PieceManager(i, j, true, PieceType.Pawn));
                    else if ((j == 0) && (i == 0 || i == 7))
                        _board.Add(new PieceManager(i, j, false, PieceType.Rook));
                    else if ((j == 7) && (i == 0 || i == 7))
                        _board.Add(new PieceManager(i, j, true, PieceType.Rook));
                    else if ((j == 0) && (i == 1 || i == 6))
                        _board.Add(new PieceManager(i, j, false, PieceType.Knight));
                    else if ((j == 7) && (i == 1 || i == 6))
                        _board.Add(new PieceManager(i, j, true, PieceType.Knight));
                    else if ((j == 0) && (i == 2 || i == 5))
                        _board.Add(new PieceManager(i, j, false, PieceType.Bishop));
                    else if ((j == 7) && (i == 2 || i == 5))
                        _board.Add(new PieceManager(i, j, true, PieceType.Bishop));
                    else if (j == 0 && i == 3)
                        _board.Add(new PieceManager(i, j, false, PieceType.Queen));
                    else if (j == 7 && i == 3)
                        _board.Add(new PieceManager(i, j, true, PieceType.Queen));
                    else if (j == 0 && i == 4)
                        _board.Add(new PieceManager(i, j, false, PieceType.King));
                    else if (j == 7 && i == 4)
                        _board.Add(new PieceManager(i, j, true, PieceType.King));
                }
            }
            return _board;
        }

        //Initialises board with checker pieces
        private List<PieceManager> InitialiseCheckerPieces()
        {
            List<PieceManager> _board = new List<PieceManager>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j < 3)
                    {
                        if (j % 2 == 0)
                        {
                            if (i % 2 == 1)
                                _board.Add(new PieceManager(i, j, false, PieceType.Checker));
                        }
                        else
                        {
                            if (i % 2 == 0)
                                _board.Add(new PieceManager(i, j, false, PieceType.Checker));
                        }
                    }
                    else if (j > 4)
                    {
                        if (j % 2 == 0)
                        {
                            if (i % 2 == 1)
                                _board.Add(new PieceManager(i, j, true, PieceType.Checker));
                        }
                        else
                        {
                            if (i % 2 == 0)
                                _board.Add(new PieceManager(i, j, true, PieceType.Checker));
                        }
                    }
                }
            }
                    
            return _board;
        }

        private List<PieceManager> InitialiseMixPieces()
        {
            List<PieceManager> _board = new List<PieceManager>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j < 2)
                    {
                        int index = 0;
                        Random rnd = new Random();
                        index = rnd.Next(8);
                        switch (index)
                        {
                            case 0:
                                _board.Add(new PieceManager(i, j, false, PieceType.Pawn));
                                break;
                            case 1:
                                _board.Add(new PieceManager(i, j, false, PieceType.Rook));
                                break;
                            case 2:
                                _board.Add(new PieceManager(i, j, false, PieceType.Knight));
                                break;
                            case 3:
                                _board.Add(new PieceManager(i, j, false, PieceType.Bishop));
                                break;
                            case 4:
                                _board.Add(new PieceManager(i, j, false, PieceType.Queen));
                                break;
                            case 5:
                                _board.Add(new PieceManager(i, j, false, PieceType.Checker));
                                break;
                            case 6:
                                _board.Add(new PieceManager(i, j, false, PieceType.KingChecker));
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
                                _board.Add(new PieceManager(i, j, true, PieceType.Pawn));
                                break;
                            case 1:
                                _board.Add(new PieceManager(i, j, true, PieceType.Rook));
                                break;
                            case 2:
                                _board.Add(new PieceManager(i, j, true, PieceType.Knight));
                                break;
                            case 3:
                                _board.Add(new PieceManager(i, j, true, PieceType.Bishop));
                                break;
                            case 4:
                                _board.Add(new PieceManager(i, j, true, PieceType.Queen));
                                break;
                            case 5:
                                _board.Add(new PieceManager(i, j, true, PieceType.Checker));
                                break;
                            case 6:
                                _board.Add(new PieceManager(i, j, true, PieceType.KingChecker));
                                break;
                        }
                    }
                }
            }
            return _board;
        }
    }
}
