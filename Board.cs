using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Board
    {
        private List<PieceManager> _board;
        private PieceManager _checkingPiece;
        private Player wP, bP;
        private bool _gameOver;
        private Game _gameType;
        private static Board _instance;
        private PieceType _promotionPiece;

        //BoardClass takes an string gametype and depending on the string initialises
        //either chess or checkers game
        private Board()
        {
            _gameOver = false;
            _checkingPiece = null;
            _board = new List<PieceManager>();
            _promotionPiece = PieceType.Queen;
        }

        public static Board Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Board();
                return _instance;
            }
        }

        public Game SetType
        {
            set
            {
                _board = new List<PieceManager>();
                new PieceManagerFactory(value, _board);
                List<PieceManager> whitepiece = new List<PieceManager>();
                List<PieceManager> blackpiece = new List<PieceManager>();
                foreach (PieceManager p in _board)
                {
                    if (p.Colour == false)
                        blackpiece.Add(p);
                    else
                        whitepiece.Add(p);
                }
                wP = new Player(true, true, whitepiece);
                bP = new Player(false, false, blackpiece);
                _gameType = value;
            }
        }

        public void Update()
        {
            _gameOver = TimeMonitor();
            DrawClock();
            switch(_gameType)
            {
                case Game.Checker:
                    GameCheckerOver();
                    break;
                case Game.Mix:
                    GameCheckerOver();
                    break;
                case Game.Chess:
                    foreach (PieceManager p in _board)
                        p.Pinned = false;
                    foreach (PieceManager p in _board)
                    {
                        p.Checker = false;
                        p.AvailableMove();
                    }
                    Player player;
                    _checkingPiece = MonitorCheck(wP, bP);
                    if (wP.Turn)
                        player = wP;
                    else
                        player = bP;
                    if (_checkingPiece != null)
                    {
                        player.KingPiece.CheckMate(_checkingPiece);
                        _gameOver = player.KingPiece.GameOver;
                    }
                    break;
            }
            if (_gameOver)
                DrawGameOver();
            wP.UpdatePieces(_board);
            bP.UpdatePieces(_board);
        }

        public void ChangePromotionPiece()
        {
            int i = (int)_promotionPiece;
            i++;
            if (i > 7)
                i = 1;
            _promotionPiece = (PieceType)i;
        }

        public void GameCheckerOver()
        {
            if (wP.Pieces.Count == 0)
                _gameOver = true;
            else if (bP.Pieces.Count == 0)
                _gameOver = true;
            else
                _gameOver = false;
        }

        //MonitorCheck checks for any checks on the board and returns the piece
        //checking the king
        public PieceManager MonitorCheck(Player white, Player black)
        {
            if (white.Turn)
            {
                foreach (PieceManager p in black.Pieces)
                {
                    if (p.AvailableMove().Contains(white.KingPiece) && p.Piece is not King)
                    {
                        p.Checker = true;
                        return p;
                    }
                }
            }
            else
            {
                foreach (PieceManager p in white.Pieces)
                {
                    if (p.AvailableMove().Contains(black.KingPiece) && p.Piece is not King)
                    {
                        p.Checker = true;
                        return p;
                    }
                }
            }
            return null;
        }

        public void LeftClick(IHavePosition p)
        {
            foreach (PieceManager piece in _board)
            {
                if (piece.Selected)
                {
                    Player player;
                    if (piece.Colour)
                        player = wP;
                    else
                        player = bP;
                    if (player.MovePiece(piece, p.PosX, p.PosY))
                    {
                        wP.UpdatePieces(_board);
                        bP.UpdatePieces(_board);
                        piece.Selected = false;
                        wP.Turn = !wP.Turn;
                        bP.Turn = !bP.Turn;
                    }
                    else
                    {
                        if(p is PieceManager)
                            (p as PieceManager).Selected = true;
                        piece.Selected = false;
                    }
                    return;
                }
            }    
            if(p is PieceManager)
                (p as PieceManager).Selected = true;
        }

        public List<PieceManager> GameBoard
        {
            get { return _board; }
        }

        //Draws board on the screen
        public void Draw()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            SplashKit.FillRectangle(Color.White, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                        else
                            SplashKit.FillRectangle(Color.Black, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                    }
                    else
                    {
                        if (j % 2 == 0)
                            SplashKit.FillRectangle(Color.Black, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                        else
                            SplashKit.FillRectangle(Color.White, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                    }

                }
            }
            SplashKit.DrawText(_promotionPiece.ToString(), Color.Red, 10, 10);
        }

        private void DrawClock()
        {
            SplashKit.FillRectangle(Color.Black, 330, 20, Constants.Instance.OffsetValue, 15);
            SplashKit.DrawText(bP.Clock, Color.White, SplashKit.FontNamed("Arial"), 100, 340, 24);
            SplashKit.FillRectangle(Color.White, 330, 705, Constants.Instance.OffsetValue, 15);
            SplashKit.DrawText(wP.Clock, Color.Black, SplashKit.FontNamed("Arial"), 100, 340, 709);
        }

        private bool TimeMonitor()
        {
            if (wP.Clock == "00:00")
                return true;
            else if (bP.Clock == "00:00")
                return true;
            return false;
        }

        private void DrawGameOver()
        {
            if (wP.Turn)
            {
                SplashKit.FillRectangle(Color.Red, 195, 315, 310, 110);
                SplashKit.FillRectangle(Color.Black, 200, 320, 300, 100);
                SplashKit.DrawText("Click Anywhere to go back.....", Color.White, SplashKit.FontNamed("Calibri"), 200, 225, 400);
                SplashKit.DrawText("You Won!\n by CheckMate", Color.White, SplashKit.FontNamed("Arial"), 100, 225, 350);
            }
            else
            {
                SplashKit.FillRectangle(Color.Red, 195, 315, 310, 110);
                SplashKit.FillRectangle(Color.White, 200, 320, 300, 100);
                SplashKit.DrawText("You Won!\n by CheckMate", Color.Black, SplashKit.FontNamed("Calibri"), 100, 225, 350);
                SplashKit.DrawText("Click Anywhere to go back.....", Color.Black, SplashKit.FontNamed("Arial"), 200, 225, 400);
            }
        }
        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        public PieceType PromotionPiece
        {
            get { return _promotionPiece; }
        }
    }
}
