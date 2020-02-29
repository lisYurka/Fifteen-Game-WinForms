using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIIS_4
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Label label2 = new Label();
            label2.Text = "Место";
            label2.Font = new Font(FontFamily.GenericSerif,14.0F,FontStyle.Bold);
            label2.TextAlign = ContentAlignment.MiddleCenter;
            Label label3 = new Label();
            label3.Text = "Имя";
            label3.Font = new Font(FontFamily.GenericSerif, 14.0F, FontStyle.Bold);
            label3.TextAlign = ContentAlignment.MiddleCenter;
            Label label4 = new Label();
            label4.Text = "Ходов";
            label4.Font = new Font(FontFamily.GenericSerif, 14.0F, FontStyle.Bold);
            label4.TextAlign = ContentAlignment.MiddleCenter;
            Label label5 = new Label();
            label5.Text = "Время";
            label5.Font = new Font(FontFamily.GenericSerif, 14.0F, FontStyle.Bold);
            label5.TextAlign = ContentAlignment.MiddleCenter;
            Label label6 = new Label();
            label6.Text = "Уровень";
            label6.Font = new Font(FontFamily.GenericSerif, 14.0F, FontStyle.Bold);
            label6.TextAlign = ContentAlignment.MiddleCenter;
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 1, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(label5, 3, 0);
            tableLayoutPanel1.Controls.Add(label6, 4, 0);
        }
        public void ShowRecordTable()
        {
            var a = Serializator.Deserialize<List<Record>>("data.dat");
            int countX = 0, countY = 0;
            a.Sort(delegate (Record x, Record y)
            {
                if (x.PlayerMoves == 0 && y.PlayerMoves == 0) return 0;
                else if (x.PlayerMoves == 0) return -1;
                else if (y.PlayerMoves == 0) return 1;
                else return x.PlayerMoves.CompareTo(y.PlayerMoves);
            });
            Label l1;
            Label l2;
            Label l3;
            Label l4;
            Label l5;
            foreach (var item in a)
            {
                countY++;
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();
                l4 = new Label();
                l5 = new Label();
                l4.Text = countY.ToString();
                if (l4.Text == "1")
                    l4.BackColor = Color.Gold;
                if (l4.Text == "2")
                    l4.BackColor = Color.LightSteelBlue;
                if (l4.Text == "3")
                    l4.BackColor = Color.Peru;
                l4.Font = new Font(FontFamily.GenericSerif, 12.0F, FontStyle.Regular);
                l4.TextAlign = ContentAlignment.MiddleCenter;
                l1.Text = item.PlayerName.ToString();
                l1.Font = new Font(FontFamily.GenericSerif, 12.0F, FontStyle.Underline);
                l1.TextAlign = ContentAlignment.MiddleCenter;
                l2.Text = item.PlayerMoves.ToString();
                l2.Font = new Font(FontFamily.GenericSerif, 12.0F, FontStyle.Regular);
                l2.TextAlign = ContentAlignment.MiddleCenter;
                l3.Text = item.PlayerTime.ToString();
                l3.Font = new Font(FontFamily.GenericSerif, 12.0F, FontStyle.Regular);
                l3.TextAlign = ContentAlignment.MiddleCenter;
                l5.Text = item.PlayerLevel.ToString();
                l5.Font = new Font(FontFamily.GenericSerif, 12.0F, FontStyle.Regular);
                l5.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(l4, countX, countY);
                tableLayoutPanel1.Controls.Add(l1, ++countX, countY);
                tableLayoutPanel1.Controls.Add(l2, ++countX, countY);
                tableLayoutPanel1.Controls.Add(l3, ++countX, countY);
                tableLayoutPanel1.Controls.Add(l5, ++countX, countY);
                countX = 0;
                tableLayoutPanel1.RowCount = countY;
                tableLayoutPanel1.Refresh();
            }
        }
    }
}
