using System;
using System.Collections.Generic;

namespace C18_Ex02
{
    public class GameLogic
    {
        private GameInfo m_GameState;
        private eChip[,] m_Board;
        private bool m_IsGameOver;
        private Computer m_Computer;
        private Player m_CurrentPlayer;
        private short m_RowCurrentLocation;
        private Player m_Winner;
        private bool m_IsDraw;
        private short m_NumOfMoves;
        private bool m_IsQuit;

        public GameLogic(Player i_CurrentPlayer)
        {
            m_CurrentPlayer = i_CurrentPlayer;
            m_Computer = new Computer();
            m_IsGameOver = false;
            m_NumOfMoves = 0;
            m_IsDraw = false;
            m_IsQuit = false;
        }

        public bool IsQuit
        {
            get
            {
                return m_IsQuit;
            }

            set
            {
                m_IsQuit = value;
            }
        }

        public bool IsDraw
        {
            get
            {
                return m_IsDraw;
            }

            set
            {
                m_IsDraw = value;
            }
        }

        public short NumOfMoves
        {
            get
            {
                return m_NumOfMoves;
            }

            set
            {
                m_NumOfMoves = value;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public GameInfo GameState
        {
            get
            {
                return m_GameState;
            }

            set
            {
                m_GameState = value;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsGameOver;
            }

            set
            {
                m_IsGameOver = value;
            }
        }

        public eChip[,] Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public void RoundSetUp()
        {
            m_IsGameOver = false;
            ClearBoard();
            m_NumOfMoves = 0;
            m_CurrentPlayer = m_GameState.Player1;
            m_IsDraw = false;
            m_IsQuit = false;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < m_GameState.Rows; i++)
            {
                for (int j = 0; j < m_GameState.Cols; j++)
                {
                    m_Board[i, j] = eChip.empty;
                }
            }
        }

        public string GetWinner()
        {
            return m_Winner.PlayerName;
        }

        public void GameSetUp(GameInfo i_GameInfo)
        {
            m_Board = new eChip[i_GameInfo.Rows, i_GameInfo.Cols];
            m_GameState = i_GameInfo;
            m_NumOfMoves = 0;
        }

        public List<short> GetAvailableCols()
        {
            List<short> availableCols = new List<short>();
            short counter = 0;

            for (short j = 0; j < m_GameState.Cols; j++)
            {
                if (m_Board[0, j].Equals(eChip.empty))
                {
                    availableCols.Add(j);
                    counter++;
                }
            }

            if (counter == 0)
            {
                m_IsGameOver = true;
            }

            return availableCols;
        }

        public void MakeRound(short i_ChosenCol)
        {
            SetNextMove(i_ChosenCol);
            if (m_GameState.IsGameAgainstComputer && !IsGameOver)
            {
                short computerMove = m_Computer.GetNextMove(GetAvailableCols());
                SetNextMove(computerMove);
            }
        }

        public void SetNextMove(short i_ChosenCol)
        {
            m_NumOfMoves++;
            updateBoard(i_ChosenCol);
            m_IsGameOver = checkIfGameOver(i_ChosenCol);
            if (m_IsGameOver)
            {
                m_Winner = m_CurrentPlayer;
                if (m_GameState.Player1 == m_Winner)
                {
                    m_GameState.Player1.Points++;
                }
                else
                {
                    m_GameState.Player2.Points++;
                }
            }
            else if (checkIfBoardFull())
            {
                m_IsGameOver = checkIfBoardFull();
                if (m_IsGameOver)
                {
                    m_IsDraw = true;
                }
            }
            else
            {
                replaceTurn();
            }
        }

        private bool checkIfBoardFull()
        {
            return m_GameState.Rows * m_GameState.Cols == m_NumOfMoves;
        }

        private void replaceTurn()
        {
            if (m_CurrentPlayer.TypeOfChip == eChip.type1)
            {
                m_CurrentPlayer = m_GameState.Player2;
            }
            else
            {
                m_CurrentPlayer = m_GameState.Player1;
            }
        }

        private void updateBoard(short i_ChosenCol)
        {
            int i = (int)GetEmptyRow(i_ChosenCol);
            if (m_CurrentPlayer.TypeOfChip == eChip.type1)
            {
                m_Board[i, i_ChosenCol] = eChip.type1;
            }
            else
            {
                m_Board[i, i_ChosenCol] = eChip.type2;
            }

            m_RowCurrentLocation = (short)i;
        }

        public short GetEmptyRow(short i_ChosenCol)
        {
            short emptyRow = -1;
            for (int i = m_GameState.Rows - 1; i >= 0; i--)
            {
                if (m_Board[i, i_ChosenCol].Equals(eChip.empty))
                {
                    emptyRow = (short)i;
                    break;
                }
            }

            return emptyRow;
        }

        private bool checkIfGameOver(short i_ChosenCol)
        {
            return checkIfRowWin() || checkIfColumnWin(i_ChosenCol) || checkIfDiagonalWin(i_ChosenCol);
        }

        public void handleQuitState()
        {
            if (m_CurrentPlayer.TypeOfChip.Equals(eChip.type1))
            {
                m_GameState.Player2.Points++;
                m_Winner = m_GameState.Player2;
            }
            else
            {
                m_GameState.Player1.Points++;
                m_Winner = m_GameState.Player1;
            }

            m_IsQuit = true;
            m_IsGameOver = true;
        }

        private bool checkIfColumnWin(short i_ChosenCol)
        {
            short counter = 0;
            bool isWin = false;
            eChip currentChipType = m_CurrentPlayer.TypeOfChip;

            for (int i = 0; i < m_GameState.Rows; i++)
            {
                if (currentChipType == m_Board[i, i_ChosenCol])
                {
                    counter++;
                    if (counter == 4)
                    {
                        isWin = true;
                        break;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            return isWin;
        }

        private bool checkIfRowWin()
        {
            short counter = 0;
            bool isWin = false;
            eChip currentChipType = m_CurrentPlayer.TypeOfChip;

            for (int j = 0; j < m_GameState.Cols; j++)
            {
                if (currentChipType == m_Board[m_RowCurrentLocation, j])
                {
                    counter++;
                    if (counter == 4)
                    {
                        isWin = true;
                        break;
                    }
                }
                else
                {
                    counter = 0;
                }
            }

            return isWin;
        }

        private bool checkIfDiagonalWin(short i_ChosenCol)
        {
            bool isWin = false;

            if (isRightDiagonalWin(i_ChosenCol) || isLeftDiagonalWin(i_ChosenCol))
            {
                isWin = true;
            }

            return isWin;
        }

        private bool isRightDiagonalWin(short i_ChosenCol)
        {
            bool isWin = false;
            int rowStartDiagonal = m_RowCurrentLocation;
            int colStartDiagonal = i_ChosenCol;

            while (rowStartDiagonal < m_GameState.Rows - 1 && colStartDiagonal > 0)
            {
                rowStartDiagonal++;
                colStartDiagonal--;
            }

            isWin = checkDiagonal(rowStartDiagonal, colStartDiagonal, -1);

            return isWin;
        }

        private bool isLeftDiagonalWin(short i_ChosenCol)
        {
            bool isWin = false;
            int rowStartDiagonal;
            int colStartDiagonal;

            rowStartDiagonal = m_RowCurrentLocation - Math.Min(i_ChosenCol, m_RowCurrentLocation);
            colStartDiagonal = i_ChosenCol - Math.Min(i_ChosenCol, m_RowCurrentLocation);

            isWin = checkDiagonal(rowStartDiagonal, colStartDiagonal, 1);

            return isWin;
        }

        private bool checkDiagonal(int i_RowStartDiagonal, int i_ColStartDiagonal, int i_addedValue)
        {
            eChip currentChipType = m_CurrentPlayer.TypeOfChip;
            short counter = 0;
            bool isWin = false;

            while ((i_RowStartDiagonal < m_GameState.Rows) && (i_ColStartDiagonal < m_GameState.Cols) && (i_RowStartDiagonal >= 0) && (i_ColStartDiagonal >= 0))
            {
                if (m_Board[i_RowStartDiagonal, i_ColStartDiagonal] == currentChipType)
                {
                    counter++;
                    if (counter == 4)
                    {
                        isWin = true;
                        break;
                    }
                }
                else
                {
                    counter = 0;
                }

                i_RowStartDiagonal += i_addedValue;
                i_ColStartDiagonal++;
            }

            return isWin;
        }
    }
}
