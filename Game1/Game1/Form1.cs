using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        public static Random rnd = new Random();

        /// Random numbers generator
        static int RN(int n)
        { return rnd.Next(n); }

        int X1, X2, X3;
        int first_ch;
        int second_ch;
        int third_ch;

        int[,] P1, P2, P3;
        int[] P4;

        int[] chosedNumber = new int[7];

        public Form1()
        {
            InitializeComponent();

            // this three varialbles is for storing the correct code
            initialization();

            // this array here is for storing the rest of numbers except the correct ones
            rest_nmbrs();

            // and this is for all the possibiltie that could be made in this game
            possibilities();
            
            // all the code below is for dicisions that could be made by the program
            decisions();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            // this three varialbles is for storing the correct code
            initialization();

            // this array here is for storing the rest of numbers except the correct ones
            rest_nmbrs();

            // and this is for all the possibiltie that could be made in this game
            possibilities();

            // all the code below is for dicisions that could be made by the program
            decisions();

            txtbx_X1.Clear();
            txtbx_X2.Clear();
            txtbx_X3.Clear();

            txtbx_X1.Focus();
        }

        private void info_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have to figure out the code from the given hints ");
        }

        private void check_btn_Click(object sender, EventArgs e)
        {
            if (txtbx_X1.Text == Convert.ToString(X1))
            {
                if (txtbx_X2.Text == Convert.ToString(X2))
                {
                    if (txtbx_X3.Text == Convert.ToString(X3))
                    {
                        MessageBox.Show("Congratulation, Your answer is correct!");
                    }
                    else
                        MessageBox.Show("You're wrong try again");
                }
                else
                    MessageBox.Show("You're wrong try again");
            }
            else
                MessageBox.Show("You're wrong try again");
        }

        /// the three events below is to move the window form

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void rest_nmbrs()
        {
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i == X1 || i == X2 || i == X3)
                    continue;
                else
                {
                    chosedNumber[j] = i;
                    j++;
                    if (j == 7) break;
                }
            }
        }

        private void initialization()
        {
            X1 = rnd.Next(9);
            X2 = rnd.Next(9);
            X3 = rnd.Next(9);
        }

        private void possibilities()
        {
            P1 = new int[,] {
                { chosedNumber[RN(7)], chosedNumber[RN(7)], X1 },
                         { chosedNumber[RN(7)], X1, chosedNumber[RN(7)]},
                         { chosedNumber[RN(7)], chosedNumber[RN(7)], X2},
                         { X2, chosedNumber[RN(7)], chosedNumber[RN(7)]},
                         { chosedNumber[RN(7)], X3, chosedNumber[RN(7)]},
                         { X3, chosedNumber[RN(7)], chosedNumber[RN(7)]}
            };

            P2 = new int[,] {
                { X1, chosedNumber[RN(7)], chosedNumber[RN(7)] },
                           { chosedNumber[RN(7)], X2, chosedNumber[RN(7)] },
                           { chosedNumber[RN(7)], chosedNumber[RN(7)], X3 }
            };

            P3 = new int[,] {
                { chosedNumber[RN(7)], X3, X1 },
                           { X3, chosedNumber[RN(7)], X1 },
                           { chosedNumber[RN(7)], X1, X2 },
                           { X2, X1, chosedNumber[RN(7)] },
                           { chosedNumber[RN(7)], X3, X2 },
                           { X3, chosedNumber[RN(7)], X2 }, };

            P4 = new int[] { chosedNumber[RN(7)], chosedNumber[RN(7)], chosedNumber[RN(7)] };
            while (P4[0] == P4[1] || P4[0] == P4[2] || P4[1] == P4[2])
            {
                P4[0] = chosedNumber[RN(7)];
                P4[1] = chosedNumber[RN(7)];
                P4[2] = chosedNumber[RN(7)];
            }
        }

        private void decisions()
        {
            int[][] Groups = new int[6][];

            Groups[0] = new int[] { 0, 1 };
            Groups[1] = new int[] { 2, 3 };
            Groups[2] = new int[] { 4, 5 };
            Groups[3] = new int[] { 0, 1, 2, 3 };
            Groups[4] = new int[] { 0, 1, 4, 5 };
            Groups[5] = new int[] { 2, 3, 4, 5 };

            first_ch = rnd.Next(5);
            second_ch = rnd.Next(3);
            third_ch = 0;

            if ((first_ch == 0 || first_ch == 1) && second_ch == 0)
                third_ch = Groups[2][rnd.Next(Groups[2].Length - 1)];

            if ((first_ch == 5 || first_ch == 4) && second_ch == 2)
                third_ch = Groups[1][rnd.Next(Groups[1].Length - 1)];

            if ((first_ch == 2 || first_ch == 3) && second_ch == 1)
                third_ch = Groups[0][rnd.Next(Groups[0].Length - 1)];

            if (((first_ch == 0 || first_ch == 1) && second_ch == 1) || ((first_ch == 2 || first_ch == 3) && second_ch == 0))
                third_ch = Groups[4][rnd.Next(Groups[4].Length - 1)];

            if (((first_ch == 0 || first_ch == 1) && second_ch == 2) || ((first_ch == 4 || first_ch == 5) && second_ch == 0))
                third_ch = Groups[5][rnd.Next(Groups[5].Length - 1)];

            if (((first_ch == 4 || first_ch == 5) && second_ch == 1) || ((first_ch == 2 || first_ch == 3) && second_ch == 2))
                third_ch = Groups[3][rnd.Next(Groups[3].Length - 1)];

            hint_11.Text = Convert.ToString(P1[first_ch, 0]);
            hint_12.Text = Convert.ToString(P1[first_ch, 1]);
            hint_13.Text = Convert.ToString(P1[first_ch, 2]);

            hint_21.Text = Convert.ToString(P2[second_ch, 0]);
            hint_22.Text = Convert.ToString(P2[second_ch, 1]);
            hint_23.Text = Convert.ToString(P2[second_ch, 2]);

            hint_31.Text = Convert.ToString(P3[third_ch, 0]);
            hint_32.Text = Convert.ToString(P3[third_ch, 1]);
            hint_33.Text = Convert.ToString(P3[third_ch, 2]);

            hint_41.Text = Convert.ToString(P4[0]);
            hint_42.Text = Convert.ToString(P4[1]);
            hint_43.Text = Convert.ToString(P4[2]);
        }
    }
}
