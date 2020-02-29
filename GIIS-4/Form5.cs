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
    public partial class Form5 : Form
    {
        public static string PersonName { get; set; }
        public Form5()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PersonName = textBox1.Text;
            if (PersonName == String.Empty)
                PersonName = "user";
            Close();
        }
    }
}
