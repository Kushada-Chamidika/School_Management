using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are you sure, Do you really want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            String textUsername = textBox1.Text;
            String textPassword = textBox2.Text;

            if (!textUsername.Equals("") && !textPassword.Equals(""))
            {

                String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
                SqlConnection con = new SqlConnection(connetionString);

                try
                {
                    con.Open();

                    String query = "SELECT * FROM Logins WHERE username = @username AND password = @password";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@username", textUsername);
                    cmd.Parameters.AddWithValue("@password", textPassword);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        this.Hide();

                        Form2 form2 = new Form2();
                        form2.Show();

                    }
                    else
                    {

                        MessageBox.Show("Invalid login credentials, Please check Username and Password and try again!", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    reader.Dispose();

                    cmd.Dispose();

                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }
            else
            {

                MessageBox.Show("Missing Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            textBox1.Clear();
            textBox2.Clear();

            textBox1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox2.Clear();

            textBox1.Focus();

        }
    }
}
