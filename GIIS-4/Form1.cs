using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace GIIS_4
{
    public partial class Form1 : Form
    {
        Logic logic;
        Dictionary<int, string> files;
        static List<Record> personRecord = new List<Record>();
        public bool styleOfGame;
        Timer timer = new Timer();
        TimeSpan time;
        int but15;
        string PlayerLevel = "Новичок";
        private SoundPlayer sound;
        public Form1()
        {
            InitializeComponent();
            logic = new Logic(4);
            string dir = @"J:\GIIS-4\GIIS-4\bin\Debug\Photos";
            but15 = Convert.ToInt16(button15.Tag);
            timer.Interval = 1000;
            timer.Tick += new EventHandler(OnTimer);
            if (Directory.Exists(dir))
            {
                files = new Dictionary<int, string>();
                string[] f = Directory.GetFiles(dir);
                int c = 1;
                foreach (var item in f)
                {
                    files.Add(c, item);
                    if (c == 16)
                        files.Add(0, item);
                    c++;
                }
            }
        }
        private void playMusic(string nameOfSound)
        {
            String fullAppName = Application.ExecutablePath;
            String fullAppPath = Path.GetDirectoryName(fullAppName);
            String fullFileName = Path.Combine(fullAppPath, nameOfSound);
            sound = new SoundPlayer(fullFileName);
        }
        private Button but(int pos)
        {
            switch (pos)
            {
                case 0:
                    return button0;
                case 1:
                    return button1;
                case 2:
                    return button2;
                case 3:
                    return button3;
                case 4:
                    return button4;
                case 5:
                    return button5;
                case 6:
                    return button6;
                case 7:
                    return button7;
                case 8:
                    return button8;
                case 9:
                    return button9;
                case 10:
                    return button10;
                case 11:
                    return button11;
                case 12:
                    return button12;
                case 13:
                    return button13;
                case 14:
                    return button14;
                case 15:
                    return button15;
                default:
                    return null;
            }
        }
        private void button0_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt32(((Button)sender).Tag);
            if (Form3.isMusicNeed)
            {
                playMusic("button.wav");
                sound.Play();
            }
            logic.Moving(position);
            label1.Text = logic.CounterOfMoves(true).ToString();
            Refresh();
            if (logic.gameFinish())
            {
                timer.Stop();
                var result = MessageBox.Show($"EZ PZ!!\n Количество ходов - {label1.Text}\nВаше время - {label4.Text}", "Win!", 
                    MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    string name = "";
                    Record person;
                    using (Form5 form5 = new Form5())
                    {
                        if (form5.ShowDialog() == DialogResult.Cancel)
                        {
                            name = Form5.PersonName;
                        }
                        person = new Record(name, Convert.ToInt16(label1.Text), label4.Text,PlayerLevel);
                        personRecord.Add(person);
                    }
                    var a = Serializator.Deserialize<List<Record>>("data.dat");
                    foreach (var item in a)
                    {
                        personRecord.Add(item);
                    }
                    Serializator.Serialize(personRecord, "data.dat");
                    personRecord.Clear();
                }
                tableLayoutPanel1.Enabled = false;
                label1.Text = logic.CounterOfMoves(false).ToString();
                label4.Text = "00:00:00";
            }
            label5.Focus();
        }
        public override void Refresh()
        {
            if (styleOfGame)
            {
                for (int pos = 0; pos < 16; pos++)
                {
                    int number = logic.getNum(pos);
                    Bitmap bm = new Bitmap(files[number]);
                    but(pos).BackgroundImage = bm;
                    but(pos).BackgroundImageLayout = ImageLayout.Stretch;
                    but(pos).Visible = number > 0;
                }
            }
            else
            {
                for (int pos = 0; pos < 16; pos++)
                {
                    int number = logic.getNum(pos);
                    but(pos).Text = number.ToString();
                    but(pos).Visible = number > 0;
                }
            }
        }
        void OnTimer(object obj, EventArgs ea)
        {
            time += new TimeSpan(0, 0, 1);
            label4.Text = time.ToString();
        }
        private void StartTheGame()
        {
            tableLayoutPanel1.Enabled = true;
            label1.Text = logic.CounterOfMoves(false).ToString();
            logic.Start();
            int GameLevel = 20;
            Random rnd = new Random();
            if (Form3.gameLevel == 0)
            {
                GameLevel = rnd.Next(20, 60);
                PlayerLevel = "Новичок";
            }
            if (Form3.gameLevel == 1)
            {
                GameLevel = rnd.Next(80, 150);
                PlayerLevel = "Любитель";
            }
            if (Form3.gameLevel == 2)
            {
                GameLevel = rnd.Next(200, 350);
                PlayerLevel = "Профи";
            }
            for (int i = 0; i < GameLevel; i++)
            {
                logic.MovingForRandom();
            }
            styleOfGame = Form2.gameStyle;
            time = new TimeSpan(0, 0, 0);
            label4.Text = "00:00:00";
            timer.Start();
            Refresh();
        }
        private void Menu_StartGame_Click(object sender, EventArgs e)
        {
            StartTheGame();
            //this.ActiveControl = null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            StartTheGame();
        }
        private int positionOfSpace()
        {
            Control b = new Control();
            int posOfSpace = 0;
            for (int i = 0; i < 16; i++)
            {
                b = tableLayoutPanel1.Controls["button" + i.ToString()];
                if (!styleOfGame)
                {
                    if (b.Text == "0")
                        posOfSpace = i;
                }
                if (styleOfGame)
                {
                    if (b.Visible == false)
                        posOfSpace = i;
                }
            }
            return posOfSpace;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer.Start();
            if (Form3.isMusicNeed)
            {
                playMusic("keyboard.wav");
                sound.Play();
            }
            label1.Text = logic.CounterOfMoves(true).ToString();
            if (e.KeyCode == Keys.Down)
            {
                int d = positionOfSpace() - 4;
                if (d > 16 || d < 0)
                    d = positionOfSpace();
                else logic.Moving(d);
                Refresh();
            }
            if (e.KeyCode == Keys.Up)
            {
                int d = positionOfSpace() + 4;
                if (d > 16)
                    d = positionOfSpace();
                else logic.Moving(d);
                Refresh();
            }
            if (e.KeyCode == Keys.Left)
            {
                int d = positionOfSpace() + 1;
                if (d > 16 || d < 0)
                    d = positionOfSpace();
                else logic.Moving(d);
                Refresh();
            }
            if (e.KeyCode == Keys.Right)
            {
                int d = positionOfSpace() - 1;
                if (d > 16 || d < 0)
                    d = positionOfSpace();
                else logic.Moving(d);
                Refresh();
            }
            if (logic.gameFinish())
            {
                timer.Stop();
                var result = MessageBox.Show($"EZ PZ!!\n Количество ходов - {label1.Text}\nВаше время - {label4.Text}", "Win!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    string name = "";
                    Record person;
                    using (Form5 form5 = new Form5())
                    {
                        if (form5.ShowDialog() == DialogResult.Cancel)
                        {
                            name = Form5.PersonName;
                        }
                        person = new Record(name, Convert.ToInt16(label1.Text), label4.Text, PlayerLevel);
                        personRecord.Add(person);
                    }
                    var a = Serializator.Deserialize<List<Record>>("data.dat");
                    foreach (var item in a)
                    {
                        personRecord.Add(item);
                    }
                    Serializator.Serialize(personRecord, "data.dat");
                    personRecord.Clear();
                }
                tableLayoutPanel1.Enabled = false;

                label1.Text = logic.CounterOfMoves(false).ToString();
                label4.Text = "00:00:00";
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
    }
}
