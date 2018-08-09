using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace _2048
{
    public struct Box
    {
        public int score;
        public List<Control> StartControl;      //起点控件
        public List<Control> EndControl;        //终点控件
        public List<int> EndPoint;              //终点位置
        public List<int> Direct;                //方向
        public List<int> StartX;                //起点横坐标
        public List<int> StartY;                //起点纵坐标
    }
    public partial class Game : Form
    {
        public static Game game=new Game();
        public static Box box;
        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            box.StartControl=new List<Control>();
            box.EndControl = new List<Control>();
            box.EndPoint = new List<int>();
            box.Direct = new List<int>();
            box.StartX = new List<int>();
            box.StartY = new List<int>();
            Function.NewBox();
            Function.IsEnd();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            if (File.Exists(@"SCORE.XML"))
            {
                doc.Load("SCORE.XML");
                root = doc.DocumentElement;
                this.Label_Score.Text = root.InnerText;
            }
            else
                this.Label_Best.Text = "0";
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            Function.Update(true);
            Movement.key = false;
            if (e.KeyCode == Keys.F1)
                Help.help.ShowDialog();
            else if (e.KeyCode == Keys.Escape)
                Application.Exit();
            else if (e.Shift && e.KeyCode == Keys.OemMinus)
                this.Opacity -= 0.1;
            else if (e.Shift && e.KeyCode == Keys.Oemplus)
                this.Opacity += 0.1;
            else if (e.KeyCode == Keys.A)
                Function.NewBox();
            else if (e.KeyCode == Keys.Left)
                Movement.Left();
            else if (e.KeyCode == Keys.Up)
                Movement.Up();
            else if (e.KeyCode == Keys.Right)
                Movement.Right();
            else if (e.KeyCode == Keys.Down)
                Movement.Down();
            Function.Update(false);
            if (Movement.key == true)
                Function.NewBox();
            Function.IsEnd();
            if(e.KeyCode==Keys.F5)
                if(MessageBox.Show("是否重新开始","提示",MessageBoxButtons.OKCancel)==DialogResult.OK)
                    Function.Init();
            //for (int i = 1; i < 5; i++)
            //{
            //    for (int j = 1; j < 5; j++)
            //    {
            //        Console.Write(Movement.map[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 1; j < 5; j++)
            //    {
            //        string name = "" + (char)('A' + i) + j;
            //        if (this.panel1.Controls.Find(name, true)[0].Text == "")
            //            Console.Write("#");
            //        else
            //            Console.Write(this.panel1.Controls.Find(name, true)[0].Text);
            //        Console.Write(" ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
        }
    }
}
