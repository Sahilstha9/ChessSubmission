using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PieceFactory
    {
        public Piece CreatePiece(PieceType piece, List<PieceManager> board, PieceManager p)
        {
            switch (piece)
            {
                case PieceType.King:
                    return new King(board, p);

                case PieceType.Queen:
                    return new Queen(board, p);
                    
                case PieceType.Pawn:
                    return new Pawn(board, p);
                    
                case PieceType.Bishop:
                    return new Bishop(board, p);
                    
                case PieceType.Rook:
                    return new Rook(board, p);
                    
                case PieceType.Knight:
                    return new Knight(board, p);
                    
                case PieceType.Checker:
                    return new CheckerPiece(board, p);
                    
                case PieceType.KingChecker:
                    return new CheckerKingPiece(board, p);
                    
                default:
                    return null;
            }
        }
    }
}
