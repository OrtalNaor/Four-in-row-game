using System.Drawing;
using System.Windows.Forms;
using C18_Ex02;

namespace GameUI
{
    public class ChipUI : PictureBox
    {
        private eChip m_ChipType;

        public ChipUI(Bitmap i_ChipPic)
        {
            this.BackColor = Color.Transparent;
            this.BackgroundImage = i_ChipPic;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Size = new Size(30, 30);
            m_ChipType = eChip.empty;
        }

        public eChip ChipType
        {
            get
            {
                return m_ChipType;
            }

            set
            {
                m_ChipType = value;
            }
        }
    }
}
