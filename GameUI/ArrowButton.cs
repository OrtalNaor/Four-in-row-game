using System.Drawing;
using System.Windows.Forms;

namespace GameUI
{
    public class ArrowButton : PictureBox
    {
        private short m_IndexCol;

        public ArrowButton(short i_IndexCol)
        {
            this.BackgroundImage = Properties.Resources.arrowImage1;
            this.m_IndexCol = i_IndexCol;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
            this.Size = new Size(30, 30);
        }

        public short IndexCol
        {
            get
            {
                return m_IndexCol;
            }
        }
    }
}
