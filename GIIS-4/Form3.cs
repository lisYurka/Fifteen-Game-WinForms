using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GIIS_4
{
    public partial class Form3 : Form
    {
        static Image img , image, image2;
        static FileStream fs;
        public static bool GameStyle = false;
        public static string fileName;
        public static bool isMusicNeed = true;
        int count = 0;
        string f1, f2;
        public static int gameLevel; 
        public Form3()
        {
            InitializeComponent();
            groupBox2.Hide();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (img != null)
                pictureBox1.Image = img;
            else
            {
                fileName = "Penguins.jpg";
                PictureCropper();
            }
            f1 = "playMusic.jpg";
            f2 = "notPlayMusic.png";
            FileStream fs1 = new FileStream(f1, FileMode.Open);
            image = Image.FromStream(fs1);
            fs1.Close();
            FileStream fs2 = new FileStream(f2, FileMode.Open);
            image2 = Image.FromStream(fs2);
            fs2.Close();
            if (isMusicNeed)
            {
                button2.BackgroundImage = image;
                button2.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                button2.BackgroundImage = image2;
                button2.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GameStyle = false;
            groupBox2.Hide();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GameStyle = true;
            fs = new FileStream(fileName, FileMode.Open);
            img = Image.FromStream(fs);
            fs.Close();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (img != null)
                pictureBox1.Image = img;
            groupBox2.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            /*
            {
                Filter = "Рисунок JPEG(*.jpg)|*.jpg"
            };
            */
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                /*DirectoryInfo dirInfo = new DirectoryInfo(@"H:\GIIS-4\GIIS-4\bin\Debug\Photos");
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }*/
                fileName = openFileDialog1.FileName;
            }

            /*
            DirectoryInfo dirInfo = new DirectoryInfo(@"H:\GIIS-4\GIIS-4\bin\Debug\Photos");
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }*/
            PictureCropper();
            fs = new FileStream(fileName, FileMode.Open);
            img = Image.FromStream(fs);
            fs.Close();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (img != null)
                pictureBox1.Image = img;
        }
        private void PictureCropper()//для нарезки картинки на равные части в поле игры
        {
            if (fileName == null)
            {
                fileName = "Penguins.jpg";
            }
            /*DirectoryInfo dirInfo = new DirectoryInfo(@"J:\GIIS-4\GIIS-4\bin\Debug\Photos");
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }*/
            FileStream fs = new FileStream(fileName, FileMode.Open);
            Image img = Image.FromStream(fs);
            fs.Close();
            Bitmap startPicture = new Bitmap(img);
            Bitmap finalPicture = new Bitmap(startPicture.Width / 4, startPicture.Height / 4);
            var dinfo = Directory.CreateDirectory("Photos");
            using (var g = Graphics.FromImage(finalPicture))
            {
                for (int i = 0; i < startPicture.Height; i += finalPicture.Height)
                {
                    for (int j = 0; j < startPicture.Width; j += finalPicture.Width)
                    {
                        //MessageBox.Show(dinfo.FullName + "\\" + (i / finalPicture.Height).ToString() + (j / finalPicture.Width).ToString() + ".jpg");
                        g.DrawImage(startPicture, new Rectangle(0, 0, finalPicture.Width, finalPicture.Height), new Rectangle(j, i, finalPicture.Width, finalPicture.Height), GraphicsUnit.Pixel);
                        finalPicture.Save(dinfo.FullName + "\\" + (i / finalPicture.Height).ToString() + (j / finalPicture.Width).ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    }
                }
            }
            startPicture.Dispose();
            finalPicture.Dispose();
            GC.Collect();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 0)
                gameLevel = 0;
            if (trackBar1.Value == 1)
                gameLevel = 1;
            if (trackBar1.Value == 2)
                gameLevel = 2;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ++count;
            if (count % 2 == 0)
            {
                button2.BackgroundImage = image;
                button2.BackgroundImageLayout = ImageLayout.Stretch;
                isMusicNeed = true;
            }
            else {
                isMusicNeed = false;
                button2.BackgroundImage = image2;
                button2.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
    }
}
