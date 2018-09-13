using System;
using System.Windows.Forms;

namespace GameUI
{
    public partial class FormLogin : Form
    {
        private bool m_IsAgainstComputer = true;

        public FormLogin()
        {
            InitializeComponent();
            this.buttonStart.Click += buttonStart_Click;
            this.checkBoxPlayer2.Click += checkBoxPlayer2_Click;
        }

        private void checkBoxPlayer2_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = !m_IsAgainstComputer;
            if (m_IsAgainstComputer)
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
            else
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
                textBoxPlayer2.Focus();
            }
        }

        public TextBox NamePlayer1
        {
            get
            {
                return textBoxPlayer1;
            }
        }

        public TextBox NamePlayer2
        {
            get
            {
                return textBoxPlayer2;
            }
        }

        public CheckBox CheckBoxPlayer2
        {
            get
            {
                return checkBoxPlayer2;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            FormGameBoard formGameBoard = new FormGameBoard(this);
            this.Hide();
            formGameBoard.FormClosed += FormGameBoard_FormClosed;
            formGameBoard.ShowDialog();
        }

        private void FormGameBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        public NumericUpDown NumericUpDownRows
        {
            get
            {
                return numericUpDownRows;
            }
        }

        public NumericUpDown NumericUpDownCols
        {
            get
            {
                return numericUpDownCols;
            }
        }
    }
}
