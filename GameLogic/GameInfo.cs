namespace C18_Ex02
{
    public class GameInfo
    {
        private Player m_Player1;
        private Player m_Player2;
        private bool m_IsGameAgainstComputer;
        private short m_Cols;
        private short m_Rows;

        public GameInfo(Player i_Player1, Player i_Player2, bool i_IsGameAgainstComputer, short i_Cols, short i_Rows)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_IsGameAgainstComputer = i_IsGameAgainstComputer;
            m_Cols = i_Cols;
            m_Rows = i_Rows;
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        public bool IsGameAgainstComputer
        {
            get
            {
                return m_IsGameAgainstComputer;
            }
        }

        public short Cols
        {
            get
            {
                return m_Cols;
            }
        }

        public short Rows
        {
            get
            {
                return m_Rows;
            }
        }
    }
}