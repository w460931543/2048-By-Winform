using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
namespace _2048
{
    class Function
    {
        static char[] character = new char[4] { 'A', 'B', 'C', 'D' };
        public static void NewBox()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            string name = character[r.Next(0, 4)] + r.Next(1, 5).ToString();
            Control control = Game.game.Controls.Find(name, true)[0];               //找到指定控件信息
            if (control.Text == "")
            {
                control.Text = "2";
                control.BackColor = System.Drawing.Color.Coral;
            }
            else
                NewBox();
        }
        public static void TransformBox(Control control)
        {
            int i=control.Name[0]-'A'+1;
            int j=control.Name[1]-'0';
            if (Movement.map[i, j] == 2)
                control.BackColor = System.Drawing.Color.Coral;
            else if (Movement.map[i, j] == 4)
                control.BackColor = System.Drawing.Color.DarkGoldenrod;
            else if (Movement.map[i, j] == 8)
                control.BackColor = System.Drawing.Color.Peru;
            else if (Movement.map[i, j] == 16)
                control.BackColor = System.Drawing.Color.Tan;
            else if (Movement.map[i, j] == 32)
                control.BackColor = System.Drawing.Color.Green;
            else if (Movement.map[i, j] == 64)
                control.BackColor = System.Drawing.Color.Khaki;
            else if (Movement.map[i, j] == 128)
                control.BackColor = System.Drawing.Color.NavajoWhite;
            else if (Movement.map[i, j] == 256)
                control.BackColor = System.Drawing.Color.Gold;
            else if (Movement.map[i, j] == 512)
                control.BackColor = System.Drawing.Color.DarkGoldenrod;
            else if (Movement.map[i, j] == 1024)
                control.BackColor = System.Drawing.Color.Orange;
            else if (Movement.map[i, j] == 2048)
                control.BackColor = System.Drawing.Color.DarkOrange;
        }
        public static void Update(bool Direct)
        {
            foreach (Control control in Game.game.panel1.Controls)
            {
                if (control.Name.Length < 3)
                {
                    int i = control.Name[0] - 'A' + 1;
                    int j = control.Name[1] - '0';
                    if (Direct == true)
                    {
                        if (control.Text != "")
                            Movement.map[i, j] = Convert.ToInt16(control.Text);
                        else
                            Movement.map[i, j] = 0;
                    }
                    else
                    {
                        if (Movement.map[i, j] == 0)
                            control.Text = "";
                        else
                            control.Text = Movement.map[i, j].ToString();
                    }
                }
            }
        }
        public static void IsEnd()
        {
           Control control_origin,control_compare;
           string name;
           for(int i=0;i<4;i++)
           {
               for(int j=1;j<5;j++)
               {
                   name = "" + (char)('A' + i) + j.ToString();
                   control_origin = Game.game.Controls.Find(name, true)[0];
                   if (control_origin.Text == "")
                       return;
                   if(j<4)
                   {
                       name = "" + (char)('A' + i) + (j+1).ToString();
                       control_compare = Game.game.Controls.Find(name, true)[0];
                       if (control_origin.Text == control_compare.Text)
                           return;
                   }
                   if(i<3)
                   {
                       name = "" + (char)('A' + i + 1) + j.ToString();
                       control_compare = Game.game.Controls.Find(name, true)[0];
                       if (control_origin.Text == control_compare.Text)
                           return;
                   }
               }
           }
           MessageBox.Show("游戏结束");
           Balance();
           Game.game.Dispose();
           Application.Exit();
        }
        public static void Init()
        {
            Balance();
            for(int i=1;i<5;i++)
                for(int j=1;j<5;j++)
                    Movement.map[i,j]=0;
            foreach(Control control in Game.game.panel1.Controls)
            {
                if(control.Name.Length<3)
                {
                    control.Text="";
                    control.BackColor = System.Drawing.Color.Bisque;
                }
            }
            Game.game.Label_Score.Text = "0";
            NewBox();
        }
        public static void Balance()
        {
            if(int.Parse(Game.game.Label_Score.Text)>int.Parse(Game.game.Label_Best.Text))
            {
                Game.game.Label_Best.Text=Game.game.Label_Score.Text;
                XmlDocument doc=new XmlDocument();
                XmlElement root=doc.CreateElement("BEST");
                XmlDeclaration dec=doc.CreateXmlDeclaration("1.0","UTF-8",null);
                doc.AppendChild(dec);
                doc.AppendChild(root);
                XmlText xmltext=doc.CreateTextNode(Game.game.Label_Score.Text);
                root.AppendChild(xmltext);
                doc.Save("SCORE.XML");
            }
        }
    }
}
