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

//INSERT INTO Registrations(firstName,lastName,dateOfBirth,gender,address,email,mobilePhone,homePhone,parentName,nic,contactNo) VALUES('Chanithya','Amarassoriya','03-02-1999','Male','Kegalle','chanithya@gmail.com','0771234566','0351234566','Thilak Amarasooriya','123456789099','0721234566');

namespace Final_Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            loadComboBox();
        }

        private void loadComboBox()
        {

            String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
            SqlConnection con = new SqlConnection(connetionString);

            try
            {
                con.Open();

                String querySearch = "SELECT regNo FROM Registrations";

                SqlCommand cmdSearch = new SqlCommand(querySearch, con);

                SqlDataReader reader = cmdSearch.ExecuteReader();

                while (reader.Read())
                {

                    comboBox1.Items.Add(reader[0].ToString());

                }

                reader.Dispose();

                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, Do you really want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            this.registrationsTableAdapter.Fill(this.studentDataSet.Registrations);

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String firstName = textBox1.Text;
            String lastName = textBox2.Text;
            String dbo = dateTimePicker1.Text;
            String gender = "";
            bool isChecked = radioButton1.Checked;
            if (isChecked)
            {
                gender = radioButton1.Text;
            }
            else
            {
                gender = radioButton2.Text;
            }
            String address = textBox3.Text;
            String email = textBox4.Text;
            String mobilePhone = textBox5.Text;
            String homePhone = textBox6.Text;
            String parentName = textBox7.Text;
            String nic = textBox8.Text;
            String contactNumber = textBox9.Text;

            if (!firstName.Equals("") && !lastName.Equals("") && !dbo.Equals("") && !gender.Equals("") && !address.Equals("") && !email.Equals("") && !mobilePhone.Equals("") && !homePhone.Equals("") && !parentName.Equals("") && !nic.Equals("") && !contactNumber.Equals(""))
            {

                String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
                SqlConnection con = new SqlConnection(connetionString);

                try
                {
                    con.Open();

                    String query = "INSERT INTO Registrations(firstName,lastName,dateOfBirth,gender,address,email,mobilePhone,homePhone,parentName,nic,contactNo) VALUES(@firstName,@lastName,@dob,@gender,@address,@email,@mobilePhone,@homePhone,@parentName,@nic,@contactNo)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@dob", dbo);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@mobilePhone", mobilePhone);
                    cmd.Parameters.AddWithValue("@homePhone", homePhone);
                    cmd.Parameters.AddWithValue("@parentName", parentName);
                    cmd.Parameters.AddWithValue("@nic", nic);
                    cmd.Parameters.AddWithValue("@contactNo", contactNumber);

                    int i = cmd.ExecuteNonQuery();

                    if (i != 0)
                    {

                        SqlConnection con1 = new SqlConnection();
                        SqlCommand cmd1 = new SqlCommand();

                        con1 = new SqlConnection(@"Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True");
                        cmd1 = new SqlCommand("SELECT * FROM Registrations", con1);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        DataTable table = new DataTable();

                        adapter.SelectCommand = cmd1;
                        adapter.Fill(table);
                        BindingSource bind = new BindingSource();

                        bind.DataSource = table;
                        dataGridView1.DataSource = bind;
                        adapter.Update(table);

                        MessageBox.Show("Record Added Successfully!", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        comboBox1.SelectedIndex = 0;
                        textBox1.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        dateTimePicker1.ResetText();
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";

                        comboBox1.Items.Clear();

                        loadComboBox();

                        con1.Close();


                    }
                    else
                    {
                        MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {

                String selectedValue = comboBox1.SelectedItem.ToString();

                String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
                SqlConnection con = new SqlConnection(connetionString);

                try
                {
                    con.Open();

                    String querySearch = "SELECT * FROM Registrations WHERE regNo = @no";

                    SqlCommand cmdSearch = new SqlCommand(querySearch, con);

                    cmdSearch.Parameters.AddWithValue("@no", selectedValue);

                    SqlDataReader reader = cmdSearch.ExecuteReader();

                    if (reader.Read())
                    {

                        textBox1.Text = reader.GetString(1);
                        textBox2.Text = reader.GetString(2);
                        dateTimePicker1.Value = reader.GetDateTime(3);
                        String gender = reader.GetString(4);
                        if (gender.Equals("Male"))
                        {
                            radioButton1.Select();
                        }
                        else
                        {
                            radioButton2.Select();
                        }
                        textBox3.Text = reader.GetString(5);
                        textBox4.Text = reader.GetString(6);
                        textBox5.Text = String.Concat(reader.GetInt32(7));
                        textBox6.Text = String.Concat(reader.GetInt32(8));
                        textBox7.Text = reader.GetString(9);
                        textBox8.Text = reader.GetString(10);
                        textBox9.Text = String.Concat(reader.GetInt32(11));



                    }

                    reader.Dispose();
                    cmdSearch.Dispose();
                    con.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {

                String selectedValue = comboBox1.SelectedItem.ToString();

                String firstName = textBox1.Text;
                String lastName = textBox2.Text;
                String dbo = dateTimePicker1.Text;
                String gender = "";
                bool isChecked = radioButton1.Checked;
                if (isChecked)
                {
                    gender = radioButton1.Text;
                }
                else
                {
                    gender = radioButton2.Text;
                }
                String address = textBox3.Text;
                String email = textBox4.Text;
                String mobilePhone = textBox5.Text;
                String homePhone = textBox6.Text;
                String parentName = textBox7.Text;
                String nic = textBox8.Text;
                String contactNumber = textBox9.Text;

                if (!firstName.Equals("") && !lastName.Equals("") && !dbo.Equals("") && !gender.Equals("") && !address.Equals("") && !email.Equals("") && !mobilePhone.Equals("") && !homePhone.Equals("") && !parentName.Equals("") && !nic.Equals("") && !contactNumber.Equals(""))
                {

                    String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
                    SqlConnection con = new SqlConnection(connetionString);

                    try
                    {
                        con.Open();

                        String query = "UPDATE Registrations SET firstname = @firstName, lastName = @lastName, dateOfBirth = @dob, gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo WHERE regNo = @regNo";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@dob", dbo);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@mobilePhone", mobilePhone);
                        cmd.Parameters.AddWithValue("@homePhone", homePhone);
                        cmd.Parameters.AddWithValue("@parentName", parentName);
                        cmd.Parameters.AddWithValue("@nic", nic);
                        cmd.Parameters.AddWithValue("@contactNo", contactNumber);
                        cmd.Parameters.AddWithValue("@regNo", selectedValue);

                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {

                            SqlConnection con1 = new SqlConnection();
                            SqlCommand cmd1 = new SqlCommand();

                            con1 = new SqlConnection(@"Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True");
                            cmd1 = new SqlCommand("SELECT * FROM Registrations", con1);

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            DataTable table = new DataTable();

                            adapter.SelectCommand = cmd1;
                            adapter.Fill(table);
                            BindingSource bind = new BindingSource();

                            bind.DataSource = table;
                            dataGridView1.DataSource = bind;
                            adapter.Update(table);

                            MessageBox.Show("Record Updated Successfully!", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            comboBox1.SelectedIndex = 0;
                            textBox1.Text = "";
                            textBox1.Text = "";
                            textBox2.Text = "";
                            dateTimePicker1.ResetText();
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                            textBox9.Text = "";

                            comboBox1.Items.Clear();

                            loadComboBox();

                            con1.Close();


                        }
                        else
                        {
                            MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are you sure, Do you really want to Delete this Record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (comboBox1.SelectedItem != null)
                {

                    String selectedValue = comboBox1.SelectedItem.ToString();

                    String connetionString = "Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True";
                    SqlConnection con = new SqlConnection(connetionString);

                    try
                    {
                        con.Open();

                        String query = "DELETE FROM Registrations WHERE regNo = @regNo";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@regNo", selectedValue);

                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {

                            SqlConnection con1 = new SqlConnection();
                            SqlCommand cmd1 = new SqlCommand();

                            con1 = new SqlConnection(@"Data Source=LAPTOP-LIBLUATS;Initial Catalog=Student;Integrated Security=True");
                            cmd1 = new SqlCommand("SELECT * FROM Registrations", con1);

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            DataTable table = new DataTable();

                            adapter.SelectCommand = cmd1;
                            adapter.Fill(table);
                            BindingSource bind = new BindingSource();

                            bind.DataSource = table;
                            dataGridView1.DataSource = bind;
                            adapter.Update(table);

                            MessageBox.Show("Record Deleted Successfully!", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            comboBox1.SelectedIndex = 0;
                            textBox1.Text = "";
                            textBox1.Text = "";
                            textBox2.Text = "";
                            dateTimePicker1.ResetText();
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                            textBox9.Text = "";

                            comboBox1.Items.Clear();

                            loadComboBox();

                            con1.Close();


                        }
                        else
                        {
                            MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }


                }
            }

        }

    }
}
