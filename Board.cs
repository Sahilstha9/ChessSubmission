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
        static int whitecount = 0, blackcount = 0;
        private List<Piece> _board;
        private Piece _checkingPiece;
        private Player wP, bP;
        private bool _gameOver;
        private Game _gameType;
        private static Board _instance;

        //BoardClass takes an string gametype and depending on the string initialises
        //either chess or checkers game
        private Board()
        {
            _gameOver = false;
            _checkingPiece = null;
            _board = new List<Piece>();
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
                _board = new List<Piece>();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (value == Game.Chess)
                            InitialiseChessPieces(i, j);
                        else if (value == Game.Checker)
                            InitialiseCheckerPieces(i, j);
                        else if (value == Game.Mix)
                            InitialiseMixPieces(i, j);
                    }
                }
                List<Piece> whitepiece = new List<Piece>();
                List<Piece> blackpiece = new List<Piece>();
                foreach (Piece p in _board)
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
                    foreach (Piece p in _board)
                        p.Pinned = false;
                    foreach (Piece p in _board)
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
        public Piece MonitorCheck(Player white, Player black)
        {
            if (white.Turn)
            {
                foreach (Piece p in black.Pieces)
                {
                    if (p.AvailableMove().Contains(white.KingPiece) && p is not King)
                    {
                        p.Checker = true;
                        return p;
                    }
                }
            }
            else
            {
                foreach (Piece p in white.Pieces)
                {
                    if (p.AvailableMove().Contains(black.KingPiece) && !(p is King))
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
            foreach (Piece piece in _board)
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
                        if(p is Piece)
                            (p as Piece).Selected = true;
                        piece.Selected = false;
                    }
                    return;
                }
            }    
            if(p is Piece)
                (p as Piece).Selected = true;
        }

        public List<Piece> GameBoard
        {
            get { return _board; }
        }

        public void MovePieces(Piece p, int posX, int posY, bool colour)
        {
            p.Move(posX, posY);
        }

        //Initialises board with chess pieces
        private void InitialiseChessPieces(int i, int j)
        {
            if (j == 1)
                _board.Add(new Pawn(false, i, j, _board));
            else if (j == 6)
                _board.Add(new Pawn(true, i, j, _board));
            else if ((j == 0) && (i == 0 || i == 7))
                _board.Add(new Rook(false, i, j, _board));
            else if ((j == 7) && (i == 0 || i == 7))
                _board.Add(new Rook(true, i, j, _board));
            else if ((j == 0) && (i == 1 || i == 6))
                _board.Add(new Knight(false, i, j, _board));
            else if ((j == 7) && (i == 1 || i == 6))
                _board.Add(new Knight(true, i, j, _board));
            else if ((j == 0) && (i == 2 || i == 5))
                _board.Add(new Bishop(false, i, j, _board));
            else if ((j == 7) && (i == 2 || i == 5))
                _board.Add(new Bishop(true, i, j, _board));
            else if (j == 0 && i == 3)
                _board.Add(new Queen(false, i, j, _board));
            else if (j == 7 && i == 3)
                _board.Add(new Queen(true, i, j, _board));
            else if (j == 0 && i == 4)
                _board.Add(new King(false, i, j, _board));
            else if (j == 7 && i == 4)
                _board.Add(new King(true, i, j, _board));
        }

        //Initialises board with checker pieces
        private void InitialiseCheckerPieces(int i, int j)
        {
            if (j < 3)
            {
                if (j % 2 == 0)
                {
                    if (i % 2 == 1)
                        _board.Add(new CheckerPiece(false, i, j, _board));
                }
                else
                {
                    if (i % 2 == 0)
                        _board.Add(new CheckerPiece(false, i, j, _board));
                }
            }
            else if (j > 4)
            {
                if (j % 2 == 0)
                {
                    if (i % 2 == 1)
                        _board.Add(new CheckerPiece(true, i, j, _board));
                }
                else
                {
                    if (i % 2 == 0)
                        _board.Add(new CheckerPiece(true, i, j, _board));
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
                        _board.Add(new Pawn(false, i, j, _board));
                        break;
                    case 1:
                        _board.Add(new Rook(false, i, j, _board));
                        break;
                    case 2:
                        _board.Add(new Knight(false, i, j, _board));
                        break;
                    case 3:
                        _board.Add(new Bishop(false, i, j, _board));
                        break;
                    case 4:
                        _board.Add(new Queen(false, i, j, _board));
                        break;
                    case 5:
                        _board.Add(new CheckerPiece(false, i, j, _board));
                        break;
                    case 6:
                        _board.Add(new CheckerKingPiece(false, i, j, _board));
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
                        _board.Add(new Pawn(true, i, j, _board));
                        break;
                    case 1:
                        _board.Add(new Rook(true, i, j, _board));
                        break;
                    case 2:
                        _board.Add(new Knight(true, i, j, _board));
                        break;
                    case 3:
                        _board.Add(new Bishop(true, i, j, _board));
                        break;
                    case 4:
                        _board.Add(new Queen(true, i, j, _board));
                        break;
                    case 5:
                        _board.Add(new CheckerPiece(true, i, j, _board));
                        break;
                    case 6:
                        _board.Add(new CheckerKingPiece(true, i, j, _board));
                        break;
                }
            }
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
    }
}
