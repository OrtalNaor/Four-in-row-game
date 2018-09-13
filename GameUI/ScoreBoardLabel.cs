using System.Drawing;
using System.Windows.Forms;
using C18_Ex02;

namespace GameUI
{
    public class ScoreBoardLabel : Label
    {
        public ScoreBoardLabel(Color i_Color, Player i_Player, string i_NumberOfPlayer)
        {
            this.Font = new Font("Tahoma", 9, FontStyle.Bold);
            this.ForeColor = i_Color;
            this.BackColor = Color.Transparent;

            if (i_Player.PlayerName == string.Empty)
            {
                this.Text = string.Format("Player {0}: {1}", i_NumberOfPlayer, i_Player.Points);
                i_Player.PlayerName = string.Format("Player {0}", i_NumberOfPlayer);
            }
            else
            {
                this.Text = string.Format("{0} : {1}", i_Player.PlayerName, i_Player.Points);
            }

            this.Width = 90;
        }
    }
}
