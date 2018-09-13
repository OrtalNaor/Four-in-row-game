namespace C18_Ex02
{
    public class Player
    {
        private string m_PlayerName;
        private short m_Points;
        private eChip m_TypeOfChip;

        public Player(string i_PlayerName, eChip i_TypeOfChip)
        {
            m_PlayerName = i_PlayerName;
            m_Points = 0;
            m_TypeOfChip = i_TypeOfChip;
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public short Points
        {
            get
            {
                return m_Points;
            }

            set
            {
                m_Points = value;
            }
        }

        public eChip TypeOfChip
        {
            get
            {
                return m_TypeOfChip;
            }

            set
            {
                m_TypeOfChip = value;
            }
        }
    }
}
