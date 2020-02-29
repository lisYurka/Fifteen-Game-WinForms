using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace GIIS_4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Form1 form1;
        Form3 form3;
        public static bool gameStyle;
        private bool flag = false;
        string fileName;
        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*if (!flag)
            {
                PictureCropper();
                flag = true;
            }*/
            form1 = new Form1();
            form1.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            /*if (!flag)
            {
                PictureCropper();
                flag = true;
            }*/
            //flag = false;
            using (form3 = new Form3())
            {
                if(form3.ShowDialog() == DialogResult.Cancel)
                {
                    gameStyle = Form3.GameStyle;
                }
            }
        }
        /*private void PictureCropper()//для нарезки картинки на равные части в поле игры
        {
            fileName = Form3.fileName;
            if(fileName == null)
            {
                fileName = "Penguins.jpg";
            }
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
                        g.DrawImage(startPicture, new Rectangle(0, 0, finalPicture.Width, finalPicture.Height), new Rectangle(j, i, finalPicture.Width, finalPicture.Height), GraphicsUnit.Pixel);
                        finalPicture.Save(dinfo.FullName + "\\" + (i / finalPicture.Height).ToString() + (j / finalPicture.Width).ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    }
                }
            }
            startPicture.Dispose();
            finalPicture.Dispose();
        }*/
        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowRecordTable();
            form4.ShowDialog();
        }
    }
}
