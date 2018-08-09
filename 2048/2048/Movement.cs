using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
namespace _2048
{
    class Movement
    {
        public static int[,] map = new int[5, 5];
        public static bool key = false;
        public static void Left()
        {
            for (int i = 1; i < 5; i++)
                for (int j = 2; j < 5; j++)
                    MoveLeft(i, j, j - 1);
            move();
        }
        public static void Up()
        {
            for (int j = 1; j < 5; j++)
                for (int i = 2; i < 5; i++)
                    MoveUp(i, j, i - 1);
            move();
        }
        public static void Right()
        {
            for (int i = 1; i < 5; i++)
                for (int j = 3; j > 0; j--)
                    MoveRight(i, j, j + 1);
            move();
        }
        public static void Down()
        {
            for (int j = 1; j < 5; j++)
                for (int i = 3; i >= 1; i--)
                    MoveDown(i, j, i + 1);
            move();
        }
        static void MoveLeft(int i,int j,int n)
        {
            if(map[i,j]!=0&&map[i,n]==0)
            {
                if (n > 1)
                    MoveLeft(i, j, n - 1);
                else
                    BoxMove(i, j, n, 1);
            }
            else if(map[i,j]!=0&&map[i,n]!=0)
            {
                if (map[i, j] == map[i, n])
                    BoxMove(i, j, n, 1);
                else if (map[i, j] != map[i, n] && n + 1 != j)
                    BoxMove(i, j, n + 1, 1);
            }
        }
        static void MoveUp(int i,int j,int n)
        {
            if(map[i,j]!=0&&map[n,j]==0)
            {
                if (n > 1)
                    MoveUp(i, j, n - 1);
                else
                    BoxMove(i, j, n, 2);
            }
            else if(map[i,j]!=0&&map[n,j]!=0)
            {
                if (map[i, j] == map[n, j])
                    BoxMove(i, j, n, 2);
                else if (map[i, j] != map[n, j] && n + 1 != i)
                    BoxMove(i, j, n + 1, 2);
            }
        }
        static void MoveRight(int i,int j,int n)
        {
            if(map[i,j]!=0&&map[i,n]==0)
            {
                if (n < 4)
                    MoveRight(i, j, n + 1);
                else
                    BoxMove(i,j,n,1);
            }
            else if(map[i,j]!=0&&map[i,n]!=0)
            {
                if (map[i, j] == map[i, n])
                    BoxMove(i, j, n, 1);
                else if (map[i, j] != map[i, n] && n - 1 != j)
                    BoxMove(i, j, n - 1, 1);
            }
        }
        static void MoveDown(int i,int j,int n)
        {
            if(map[i,j]!=0&&map[n,j]==0)
            {
                if (n < 4)
                    MoveDown(i, j, n + 1);
                else
                    BoxMove(i, j, n, 2);
            }
            else if(map[i,j]!=0&&map[n,j]!=0)
            {
                if (map[i, j] == map[n, j])
                    BoxMove(i, j, n, 2);
                else if (map[i, j] != map[n, j] && n - 1 != i)
                    BoxMove(i, j, n - 1, 2);
            }
        }
        static void BoxMove(int i,int j,int n,int direct)
        {
            //MessageBox.Show(i + " " + j + " " + n + " " + direct);
            string StartLabel, EndLabel;
            if (direct == 1)
            {
                if (map[i, j] == map[i, n])
                {
                    map[i, n] += map[i, j];
                    Game.game.Label_Score.Text = (Convert.ToInt16(Game.game.Label_Score.Text) + map[i, n]).ToString();
                }
                else
                    map[i, n] = map[i, j];
                map[i, j] = 0;
                StartLabel = "" + (char)('A' + i - 1) + j;
                EndLabel = "" + (char)('A' + i - 1) + n;
            }
            else
            {
                if (map[i, j] == map[n, j])
                {
                    map[n, j] += map[i, j];
                    Game.game.Label_Score.Text = (Convert.ToInt16(Game.game.Label_Score.Text) + map[n, j]).ToString();
                }
                else
                    map[n, j] = map[i, j];
                map[i, j] = 0;
                StartLabel = "" + (char)('A' + i - 1) + j;
                EndLabel = "" + (char)('A' + n - 1) + j;
            }


            //MessageBox.Show(StartLabel + " " + EndLabel);
            Control StartControl = Game.game.Controls.Find(StartLabel, true)[0];
            Control EndControl = Game.game.Controls.Find(EndLabel, true)[0];
            Game.box.StartControl.Add(StartControl);
            Game.box.EndControl.Add(EndControl);
            Game.box.StartX.Add(StartControl.Location.X);
            Game.box.StartY.Add(StartControl.Location.Y);
            Game.box.Direct.Add(direct);
            if (direct == 1)
                Game.box.EndPoint.Add(EndControl.Location.X);
            else if (direct == 2)
                Game.box.EndPoint.Add(EndControl.Location.Y);

        }
        public static void move()
        {
            int count = Game.box.StartControl.Count;
            bool[] check = new bool[count];
            bool exit;

            while (true)
            {
                for (int i = 0; i < count; i++)
                {
                    Game.box.StartControl[i].BringToFront();
                    if (Game.box.Direct[i] == 1 && check[i] == false)
                    {
                        if (Game.box.StartControl[i].Location.X + 10 < Game.box.EndPoint[i])
                            Game.box.StartControl[i].Left += 10;
                        else if (Game.box.StartControl[i].Location.X - 10 > Game.box.EndPoint[i])
                            Game.box.StartControl[i].Left -= 10;
                        else
                            check[i] = true;
                    }
                    if (Game.box.Direct[i] == 2 && check[i] == false)
                    {
                        if (Game.box.StartControl[i].Location.Y + 10 < Game.box.EndPoint[i])
                            Game.box.StartControl[i].Top += 10;
                        else if (Game.box.StartControl[i].Location.Y - 10 > Game.box.EndPoint[i])
                            Game.box.StartControl[i].Top -= 10;
                        else
                            check[i] = true;
                    }
                    if (check[i] == true)
                    {
                        Deal(Game.box.StartControl[i]);
                        Function.TransformBox(Game.box.EndControl[i]);
                    }
                }
                exit = true;
                for (int i = 0; i < count; i++)
                {
                    if (check[i] == false)
                    {
                        exit = false;
                        break;
                    }
                }
                if (exit == true)
                    break;
            }
            for (int i = 0; i < Game.box.StartX.Count; i++)
                Game.box.StartControl[i].Location = new Point(Game.box.StartX[i], Game.box.StartY[i]);
            Game.box.StartControl.Clear();
            Game.box.EndControl.Clear();
            Game.box.StartX.Clear();
            Game.box.StartY.Clear();
            Game.box.Direct.Clear();
            Game.box.EndPoint.Clear();
        }
        public static void Deal(Control control)
        {
            control.Text = "";
            control.BackColor = System.Drawing.Color.Bisque;
            key = true;
        }
    }
}
