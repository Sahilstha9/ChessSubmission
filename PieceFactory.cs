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
                    p.Identifier = "K";
                    return new King(board, p);

                case PieceType.Queen:
                    p.Identifier = "Q";
                    return new Queen(board, p);
                    
                case PieceType.Pawn:
                    p.Identifier = "P";
                    return new Pawn(board, p);
                    
                case PieceType.Bishop:
                    p.Identifier = "B";
                    return new Bishop(board, p);
                    
                case PieceType.Rook:
                    p.Identifier = "R";
                    return new Rook(board, p);
                    
                case PieceType.Knight:
                    p.Identifier = "N";
                    return new Knight(board, p);
                    
                case PieceType.Checker:
                    p.Identifier = "C";
                    return new CheckerPiece(board, p);
                    
                case PieceType.KingChecker:
                    p.Identifier = "KC";
                    return new CheckerKingPiece(board, p);
                    
                default:
                    return null;
            }
        }
    }
}
