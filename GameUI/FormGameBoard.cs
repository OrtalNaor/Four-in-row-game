using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using C18_Ex02;

namespace GameUI
{
    public partial class FormGameBoard : Form
    {
        private FormLogin m_FormLogin;
        private GameLogic m_GameLogic;
        private GameInfo m_GameState;
        private short m_ColClicked;
        private ChipUI[,] m_ChipUIBoard;
        private List<ArrowButton> m_ListArrowsButtons;
        private ScoreBoardLabel m_Player1Details;
        private ScoreBoardLabel m_Player2Details;
        private short m_NumberOfRows;
        private short m_NumberOfCols;

        public FormGameBoard(FormLogin i_FormLogin)
        {
            InitializeComponent();
            m_FormLogin = i_FormLogin;
            m_ListArrowsButtons = new List<ArrowButton>();
            setUpGameInfo();
        }

        private void makeMove()
        {
            if (m_ColClicked != -1)
            {
                m_GameLogic.MakeRound(m_ColClicked);
            }
        }

        private void setUpGameInfo()
        {
            GameInfo gameInfo = getGameInfo();
            m_GameLogic.GameSetUp(gameInfo);
            createBoardGame();
            createScoreBoard();
        }

        private void createScoreBoard()
        {
            Panel scorePanel = new Panel();
            scorePanel.BackColor = Color.Transparent;
            m_Player1Details = new ScoreBoardLabel(Color.Blue, m_GameState.Player1, "1");
            m_Player2Details = new ScoreBoardLabel(Color.Red, m_GameState.Player2, "2");
            m_Player1Details.Top = scorePanel.Top;
            m_Player2Details.Top = scorePanel.Top;
            m_Player2Details.Left = m_Player1Details.Width + 5;
            scorePanel.Controls.Add(m_Player1Details);
            scorePanel.Controls.Add(m_Player2Details);
            scorePanel.Left = (this.Width / 2) - (scorePanel.Width / 2);
            scorePanel.Width = m_Player1Details.Width + m_Player2Details.Width + 10;
            scorePanel.Height = m_Player1Details.Height + 5;
            scorePanel.Top = 10;
            this.Controls.Add(scorePanel);
        }

        private GameInfo getGameInfo()
        {
            bool isGameAgainstComputer = checkIfGameAgainstComputer();
            Player player1 = player1Details();
            Player player2 = player2Details(isGameAgainstComputer);
            m_NumberOfRows = (short)m_FormLogin.NumericUpDownRows.Value;
            m_NumberOfCols = (short)m_FormLogin.NumericUpDownCols.Value;

            m_GameLogic = new GameLogic(player1);
            m_GameState = new GameInfo(player1, player2, isGameAgainstComputer, m_NumberOfCols, m_NumberOfRows);

            return m_GameState;
        }

        private bool checkIfGameAgainstComputer()
        {
            return !m_FormLogin.CheckBoxPlayer2.Checked;
        }

        private Player player2Details(bool i_IsGameAgainstComputer)
        {
            Player player2;

            if (i_IsGameAgainstComputer)
            {
                player2 = new Player("computer", eChip.type2);
            }
            else
            {
                player2 = new Player(m_FormLogin.NamePlayer2.Text, eChip.type2);
            }

            return player2;
        }

        private Player player1Details()
        {
            Player player1;
            player1 = new Player(m_FormLogin.NamePlayer1.Text, eChip.type1);

            return player1;
        }

        private void createBoardGame()
        {
            int numberOfRows = (int)m_FormLogin.NumericUpDownRows.Value;
            int numberOfCols = (int)m_FormLogin.NumericUpDownCols.Value;

            createArrowsLine(numberOfCols);
            createChipBoard(numberOfRows, numberOfCols);
        }

        private void createArrowsLine(int i_NumberOfCols)
        {
            int left = 10;
            ArrowButton arrow;
            for (short i = 0; i < i_NumberOfCols; i++)
            {
                arrow = new ArrowButton(i);
                arrow.Top = 50;
                arrow.Left = left + 10;
                left += 60;
                this.Controls.Add(arrow);
                arrow.Click += arrow_Click;
                m_ListArrowsButtons.Add(arrow);
            }
        }

        private void arrow_Click(object sender, EventArgs e)
        {
            m_ColClicked = (sender as ArrowButton).IndexCol;
            short currentRow = m_GameLogic.GetEmptyRow(m_ColClicked);

            m_ChipUIBoard[currentRow, m_ColClicked].BackgroundImage = m_GameLogic.CurrentPlayer.TypeOfChip == eChip.type1 ? Properties.Resources.blueBtn1 : Properties.Resources.redBtn1;
            m_ChipUIBoard[currentRow, m_ColClicked].ChipType = m_GameLogic.CurrentPlayer.TypeOfChip;
            m_ChipUIBoard[currentRow, m_ColClicked].Refresh();

            m_GameLogic.MakeRound(m_ColClicked);
            if (m_GameLogic.GameState.IsGameAgainstComputer)
            {
                Thread.Sleep(200);
                updatePictureBoard();
            }

            if (checkIfColFull())
            {
                (sender as ArrowButton).Enabled = false;
                (sender as ArrowButton).BackgroundImage = Properties.Resources.arrowImageDisable2;
            }

            if (m_GameLogic.IsGameOver)
            {
                manageResult();
            }
        }

        private void updatePictureBoard()
        {
            for (int i = 0; i < m_GameState.Rows; i++)
            {
                for (int j = 0; j < m_GameState.Cols; j++)
                {
                    if (m_ChipUIBoard[i, j].ChipType != m_GameLogic.Board[i, j])
                    {
                        m_ChipUIBoard[i, j].BackgroundImage = Properties.Resources.redBtn1;
                        m_ChipUIBoard[i, j].ChipType = m_GameLogic.Board[i, j];
                        m_ChipUIBoard[i, j].Refresh();
                        if (i == 0)
                        {
                            m_ListArrowsButtons[j].Enabled = false;
                            m_ListArrowsButtons[j].BackgroundImage = Properties.Resources.arrowImageDisable2;
                        }

                        return;
                    }
                }
            }
        }

        private void manageResult()
        {
            GameInfo gameInfo = m_GameLogic.GameState;
            updateResults(gameInfo);
        }

        private void updateResults(GameInfo i_GameState)
        {
            updatePoints(i_GameState);
            if (m_GameLogic.IsDraw)
            {
                displayMessageBox(string.Format("Tie!!{0}another round?", Environment.NewLine), "A Tie!");
            }
            else
            {
                displayMessageBox(string.Format("{0} won!!{1}Another round?", m_GameLogic.GetWinner(), Environment.NewLine), "A Win!");
            }
        }

        private void updatePoints(GameInfo i_GameState)
        {
            m_Player1Details.Text = string.Format("{0} : {1}", i_GameState.Player1.PlayerName, i_GameState.Player1.Points);
            m_Player2Details.Text = string.Format("{0} : {1}", i_GameState.Player2.PlayerName, i_GameState.Player2.Points);
        }

        private void displayMessageBox(string i_MessageText, string i_MessageTitle)
        {
            if (MessageBox.Show(i_MessageText, i_MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                anotherGame();
            }
            else
            {
                this.Close();
            }
        }

        private void anotherGame()
        {
            m_GameLogic.RoundSetUp();
            clearBoard();
            restartButtons();
        }

        private void restartButtons()
        {
            foreach (ArrowButton arrowButton in m_ListArrowsButtons)
            {
                arrowButton.Enabled = true;
                arrowButton.BackgroundImage = Properties.Resources.arrowImage1;
            }
        }

        private void clearBoard()
        {
            for (int i = 0; i < m_GameState.Rows; i++)
            {
                for (int j = 0; j < m_GameState.Cols; j++)
                {
                    m_ChipUIBoard[i, j].BackgroundImage = Properties.Resources.greyBtn1;
                    m_ChipUIBoard[i, j].ChipType = eChip.empty;
                }
            }

            Refresh();
        }

        private bool checkIfColFull()
        {
            List<short> availbleCols = m_GameLogic.GetAvailableCols();
            bool isFull = false;

            if (!availbleCols.Contains(m_ColClicked))
            {
                isFull = true;
            }

            return isFull;
        }

        private void createChipBoard(int i_NumberOfRows, int i_NumberOfCols)
        {
            ChipUI chipUI;
            int left = 10;
            int top = 100;

            m_ChipUIBoard = new ChipUI[i_NumberOfRows, i_NumberOfCols];
            for (int i = 0; i < i_NumberOfRows; i++)
            {
                for (int j = 0; j < i_NumberOfCols; j++)
                {
                    chipUI = new ChipUI(Properties.Resources.greyBtn1);
                    chipUI.Top = top;
                    chipUI.Left = left + 10;
                    left += 60;
                    this.Controls.Add(chipUI);
                    m_ChipUIBoard[i, j] = chipUI;
                }

                left = 10;
                top += 50;
            }
        }
    }
}
