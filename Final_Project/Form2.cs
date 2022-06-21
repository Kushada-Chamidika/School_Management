using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.Hide();

            Form3 form3 = new Form3();
            form3.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            this.Hide();

            Form4 form4 = new Form4();
            form4.Show();

        }
    }
}
