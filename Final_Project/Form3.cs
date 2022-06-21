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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
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

                    String querySearch = "SELECT username FROM Logins WHERE username = @uname";

                    SqlCommand cmdSearch = new SqlCommand(querySearch, con);

                    cmdSearch.Parameters.AddWithValue("@uname", textUsername);

                    SqlDataReader reader = cmdSearch.ExecuteReader();

                    if (reader.HasRows)
                    {

                        reader.Dispose();

                        MessageBox.Show("Already Exsists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {

                        reader.Dispose();

                        String query = "INSERT INTO Logins(username,password) VALUES(@username,@password)";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@username", textUsername);
                        cmd.Parameters.AddWithValue("@password", textPassword);

                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {

                            SqlConnection con1 = new SqlConnection();
                            SqlCommand cmd1 = new SqlCommand();

                            con1 = new SqlConnection(@"Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True");
                            cmd1 = new SqlCommand("SELECT * FROM Logins", con1);

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            DataTable table = new DataTable();

                            adapter.SelectCommand = cmd1;
                            adapter.Fill(table);
                            BindingSource bind = new BindingSource();

                            bind.DataSource = table;
                            dataGridView1.DataSource = bind;
                            adapter.Update(table);

                            MessageBox.Show("Successfully Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            con1.Close();

                        }
                        else
                        {
                            MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

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

        private void Form3_Load(object sender, EventArgs e)
        {

            this.loginsTableAdapter.Fill(this.studentDataSet.Logins);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form2 form2 = new Form2();
            form2.Show();

        }

    }
}
