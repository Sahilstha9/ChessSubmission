using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PieceFactory
    {
        public IPieceStrategy CreatePiece(PieceType piece, PieceManager p)
        {
            switch (piece)
            {
                case PieceType.King:
                    p.Identifier = "K";
                    return new King(p);

                case PieceType.Queen:
                    p.Identifier = "Q";
                    return new Queen(p);
                    
                case PieceType.Pawn:
                    p.Identifier = "P";
                    return new Pawn(p);
                    
                case PieceType.Bishop:
                    p.Identifier = "B";
                    return new Bishop(p);
                    
                case PieceType.Rook:
                    p.Identifier = "R";
                    return new Rook(p);
                    
                case PieceType.Knight:
                    p.Identifier = "N";
                    return new Knight(p);
                    
                case PieceType.Checker:
                    p.Identifier = "C";
                    return new CheckerPiece(p);
                    
                case PieceType.KingChecker:
                    p.Identifier = "KC";
                    return new CheckerKingPiece(p);
                    
                default:
                    return null;
            }
        }
    }
}
