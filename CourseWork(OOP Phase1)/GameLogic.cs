using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using CourseWork_OOP_Phase1_.Enums;
using ApplicationException = System.ApplicationException;

namespace CourseWork_OOP_Phase1_
{
    public sealed class GameLogic
    {

        public int Columns { get; set; }
        public int Rows { get; set; }


        public Chip FirstPlayerChip { get; set; }

       
        public Chip CurrentChipTurn { get;private set; }

        private int LastPlacedChipRow;
        private int LastPlacedChipColumn;
       
        private Chip[,] gameChips;
        public Chip this[int row,int col] => gameChips[row, col];

        public delegate void OnSwitchTurnHandler(object sender);
        public event OnSwitchTurnHandler OnSwitchTurn;

        public delegate void OnChipPlacedHandler(object sender);
        public event OnChipPlacedHandler OnChipPlaced;

        public delegate void OnNewGameHandler (object sender);
        public event OnNewGameHandler OnNewGame;

        public delegate void OnGameOverHandler(object sender,GameStatus gamest);
        public event OnGameOverHandler OnGameOver;

        public delegate void OnGameResetHandler(object sender);
        
        public event OnGameResetHandler OnGameReset;

        public delegate void OnUndoChipHadler(object sender);

        public event OnUndoChipHadler OnUndoChip;
        public bool IsGameOver => CurrGameStatus != GameStatus.Ongoing;

        
        public List<Chip> _score { get;private set; }

        public GameLogic(int col, int row, Chip pl1Chip)
        {
            if (col < 7)
                throw new ApplicationException("");
            if (row < 6)
                throw new ApplicationException("");
            if (pl1Chip == Chip.None)
                throw new ApplicationException();
            this.Columns = col;
            this.Rows = row;
            CurrentChipTurn = FirstPlayerChip = pl1Chip;
            gameChips = new Chip[row, col];
            _score = new List<Chip>();

            StartNewGame(false);

        }

        public void StartNewGame(bool resetScores)
        {
            OnGameReset?.Invoke(this);
           
         
            for (int i = 0; i < gameChips.GetLength(0); i++)
            {
                for (int j = 0; j < gameChips.GetLength(1); j++)
                {
                    gameChips[i, j] = Chip.None;
                }
            }

            if (resetScores)
            {
                _score.Clear();
                CurrentChipTurn = FirstPlayerChip;
            }
            OnNewGame?.Invoke(this);
        }

        public HashSet<Point> GetWinLoc()
        {
            WinnerFour winner;
            return (winner = WinnerPositions(Chip.Player1, LastPlacedChipRow, LastPlacedChipColumn)).PlayerWon ||
                   (winner = WinnerPositions(Chip.Player2, LastPlacedChipRow, LastPlacedChipColumn)).PlayerWon
                ? winner.WinningLocation
                : null;
        }
        public bool IsFilled
        {
            get
            {
                for (int i = 0; i < gameChips.GetLength(1); i++)
                {
                    if (gameChips[0, i] == Chip.None)
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        public int GetChipsEachRow(int row,Chip chip)
        {
            return Enumerable.Range(0, gameChips.GetLength(1)).Count(column => gameChips[row, column] == chip);
        }
        //7=get.length(1)
        //6=get.length(0)

        public int CountChipsInColumn(Chip chip, int column)
        {
            return Enumerable.Range(0, gameChips.GetLength(0)).Count(row => gameChips[row, column] == chip);
        }
        public int CountPlayer1Chips() //pl1c
        {
            return Enumerable.Range(0, gameChips.GetLength(1)).Sum(column => CountChipsInColumn(Chip.Player2,column));
        }
        public int CountPlayer2Chips() //pl2chips
        {
            return Enumerable.Range(0, gameChips.GetLength(0)).Sum(row => GetChipsEachRow( row,Chip.Player1));
        }
        public int CountNoneChips()
        {
            return Enumerable.Range(0, gameChips.GetLength(1)).Sum(row => CountChipsInColumn(Chip.None, row));
        }



        public bool IsColumnAvailable(int column)
        {
            return gameChips[0, column] == Chip.None;
        }

        public List<int> GetAllAvailableColumnPositions()
        {
            List<int> availableCol = new List<int>();

            for (int i = 0; i < Columns; i++)
            {
                if (IsColumnAvailable(i))
                {
                    availableCol.Add(i);
                }

            }

            return availableCol;
        }

        public int PlaceChipOnCol(int col, Chip chip)
        {
            if (!IsColumnAvailable(col))
                throw new Exception("No more columns");

            if (chip == Chip.None)
            {
                throw new InvalidEnumArgumentException("The chip is invalid!");
            }

            int row = getNextAvailableRow(col);

            gameChips[row, col] = chip;
            LastPlacedChipColumn=col;
            LastPlacedChipRow = row;
            if (IsGameOver)
            {
                UpdateScore();
                OnGameOver?.Invoke(this,CurrGameStatus);
            }
            else
            {
                SwitchTurn();
            }

            return row;

        }

        private void UpdateScore()
        {
            GameStatus gameresult = CurrGameStatus;
            if (gameresult == GameStatus.Pl1Won || gameresult == GameStatus.Pl2Won)
            {
                _score.Add(gameresult==GameStatus.Pl1Won ? Chip.Player1 : Chip.Player2);
            }
        }

        public int getNextAvailableRow(int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (gameChips[row, col] == Chip.None)
                {
                    return row;
                }
            }
            return -1;
        }

        public void ChipOperations(Chip chip,int row,int col)
        {
            gameChips[row, col] = chip;
            CurrentChipTurn = CurrentChipTurn ==Chip.Player1 ? Chip.Player2 : Chip.Player1;
        }


        private void SwitchTurn()
        {
            CurrentChipTurn = CurrentChipTurn == Chip.Player1 ? Chip.Player2 : Chip.Player1;
           // OnSwitchTurn?.Invoke(this);
        }
        private HashSet<Point> CheckDirection(Chip chip, int row, int col, int rowStep, int colStep)
        {
            HashSet<Point> winLoc = new HashSet<Point>();

            int count = 0;
           // winLoc.Add(new Point(col, row));

           
            int r = row;
            int c = col;
            while (r < Rows && r >= 0 && c < Columns && c >= 0 && gameChips[r, c] == chip)
            {
                count++;
                winLoc.Add(new Point(c, r));
                r += rowStep;
                c += colStep;
            }

            
            r = row - rowStep;
            c = col - colStep;
            while (r < Rows && r >= 0 && c < Columns && c >= 0 && gameChips[r, c] == chip)
            {
                count++;
                winLoc.Add(new Point(c, r));
                r -= rowStep;
                c -= colStep;
            }

            return count >= 4 ? winLoc : new HashSet<Point>();
        }
      

        private WinnerFour WinnerPositions(Chip chip, int row, int col)
        {
            HashSet<Point> winLoc = new HashSet<Point>();

            winLoc.UnionWith(CheckDirection(chip, row, col, 0, 1));  // Horizontal
            winLoc.UnionWith(CheckDirection(chip, row, col, 1, 0));  // Vertical
            winLoc.UnionWith(CheckDirection(chip, row, col, 1, 1));  // Diagonal
            winLoc.UnionWith(CheckDirection(chip, row, col, 1, -1)); // Inverted diagonal

            bool hasWon = winLoc.Count > 0;
            return new WinnerFour(hasWon, winLoc);
        }

        public GameStatus CurrGameStatus
        {
            get
            {
                if (WinnerPositions(Chip.Player1, LastPlacedChipRow, LastPlacedChipColumn).PlayerWon)
                {
                    return GameStatus.Pl1Won;
                }
                else if (WinnerPositions(Chip.Player2, LastPlacedChipRow, LastPlacedChipColumn).PlayerWon)
                {
                    return GameStatus.Pl2Won;
                }

                if (IsFilled)
                {
                    return GameStatus.Tied;
                }

                return GameStatus.Ongoing;
            }
        }

    

   
    }

}
