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
namespace Hospital_Management_System
{
    
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader dreader;
        string connstring = "server=LAPTOP-OKSTU165;initial catalog = lab; integrated security = true";
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            listBox1.SelectedItem = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection("server=LAPTOP-OKSTU165;initial catalog=lab;integrated security=true");
            sql.Open();
            SqlCommand cmd = new SqlCommand("Select name from doctor", sql);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString());
            }
            sql.Close();
            using (SqlConnection sqlCon = new SqlConnection(connstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from patient", sqlCon);
                DataTable dtb1 = new DataTable();
                sqlDa.Fill(dtb1);
                dataGridView1.DataSource = dtb1;
            }
            sql.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Adding the Patient Record to the Database
            conn = new SqlConnection(connstring);
            
            conn.Open();
            String doctorName = "";
            int tm = 1;
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Doctor First");
                tm = 1;
                //doctorName = listBox1.SelectedItem.ToString();
            }
            else
            {

                if (tm == 1)
                {
                    doctorName = listBox1.SelectedItem.ToString();
                }

                String gender = null;
                if (checkBox1.Checked)
                {
                    gender = checkBox1.Text.ToString();
                }
                else
                {
                    gender = checkBox2.Text.ToString();
                }

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || gender == null || doctorName == "")
                {
                    MessageBox.Show("Fill All the Fields Properly");
                }
                else
                {


                    String final = "Insert into patient values(" + "'" + textBox1.Text + "'," + "'" + textBox2.Text + "'," + "'" + doctorName + "'," + textBox3.Text + "," + "'" + textBox4.Text + "'," + "'" + gender + "'," + "'" + textBox5.Text + "'," + "'" + textBox6.Text + "'," + "'" + textBox7.Text + "'" + ")";
                    Console.WriteLine(final);
                    comm = new SqlCommand("Insert into patient values(" + "'" + textBox1.Text + "'," + "'" + textBox2.Text + "'," + "'" + doctorName + "'," + textBox3.Text + "," + "'" + textBox4.Text + "'," + "'" + gender + "'," + "'" + textBox5.Text + "'," + "'" + textBox6.Text + "'," + "'" + textBox7.Text + "'" + ")", conn);

                    //comm = new SqlCommand("Insert into patient values(" + textBox1.Text + ",'" + textBox2.Text + "'," + textBox3.Text + ",'" + textBox4.Text + "')", conn);


                    try
                    {
                        comm.ExecuteNonQuery();
                        Console.Write(final);
                        MessageBox.Show("Details Saved");
                    }
                    catch (Exception ex)
                    {
                        Console.Write(final);
                        MessageBox.Show("Details Not Saved");
                    }
                    finally
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        listBox1.SelectedItem = null;
                        conn.Close();
                    }

                    using (SqlConnection sqlCon = new SqlConnection(connstring))
                    {
                        sqlCon.Open();
                        SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from patient", sqlCon);
                        DataTable dtb1 = new DataTable();
                        sqlDa.Fill(dtb1);
                        dataGridView1.DataSource = dtb1;
                    }
                }
            }
            //sql.Close();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the Patient ID whose Record You Want to Delete");
            }
            else
            {
                conn = new SqlConnection(connstring);
                conn.Open();
                String s = "delete from patient where id = " + "'" + textBox1.Text + "'" + " ";
                Console.WriteLine(s);
                comm = new SqlCommand("delete from patient where id = " + "'" + textBox1.Text + "'" + " ", conn);
                try
                {
                    comm.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    listBox1.SelectedItem = null;

                    MessageBox.Show("Record Deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Record Not Deleted");
                }
                finally
                {
                    conn.Close();
                }

                using (SqlConnection sqlCon = new SqlConnection(connstring))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from patient", sqlCon);
                    DataTable dtb1 = new DataTable();
                    sqlDa.Fill(dtb1);
                    dataGridView1.DataSource = dtb1;
                }
            }
            //sql.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("First Search the Record to be Updated");
            }
            else
            {


                conn = new SqlConnection(connstring);
                conn.Open();
                String doctorName = listBox1.SelectedItem.ToString(); //Dcotors name
                String z = "Update patient set name =" + "'" + textBox2.Text + "'," + "age = " + textBox3.Text + ",symptoms = " + "'" + textBox4.Text + "'," + "contactnumber = " + "'" + textBox6.Text + "'," + "address = " + "'" + textBox7.Text + "'," + "doctorname = " + "'" + doctorName + "'" + " where id = " + "'" + textBox1.Text + "'" + ";";
                Console.WriteLine(z);
                comm = new SqlCommand("Update patient set name =" + "'" + textBox2.Text + "'," + "age = " + textBox3.Text + ",symptoms = " + "'" + textBox4.Text + "'," + "contactnumber = " + "'" + textBox6.Text + "'," + "addresss = " + "'" + textBox7.Text + "'," + "doctorname = " + "'" + doctorName + "'" + " where id = " + "'" + textBox1.Text + "'" + ";", conn);
                try
                {
                    comm.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    listBox1.SelectedItem = null;
                    MessageBox.Show("Details Updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Details Not Updated");
                }
                finally
                {
                    conn.Close();
                }

                using (SqlConnection sqlCon = new SqlConnection(connstring))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from patient", sqlCon);
                    DataTable dtb1 = new DataTable();
                    sqlDa.Fill(dtb1);
                    dataGridView1.DataSource = dtb1;
                }
            }
            //sql.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the Patient ID whose Record you want to search");
            }
            else
            {
                conn = new SqlConnection(connstring);
                conn.Open();
                comm = new SqlCommand("Select * from patient where id = " + "'" + textBox1.Text + "'" + "", conn);
                try
                {
                    dreader = comm.ExecuteReader();
                    if (dreader.Read())
                    {
                        textBox2.Text = dreader[1].ToString();
                        textBox3.Text = dreader[3].ToString();
                        textBox4.Text = dreader[4].ToString();
                        textBox5.Text = dreader[6].ToString();
                        textBox6.Text = dreader[7].ToString();
                        textBox7.Text = dreader[8].ToString();
                        String DocName = dreader[2].ToString();
                        String Gender = dreader[5].ToString();

                        if (checkBox1.Text == Gender)
                        {
                            checkBox1.Checked = true;
                        }
                        else
                        {
                            checkBox2.Checked = true;
                        }

                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            if (listBox1.Items[i].ToString() == DocName)
                            {
                                listBox1.SetSelected(i, true);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found");
                    }
                    dreader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Record Not Found");
                }
                finally
                {
                    conn.Close();
                }

                using (SqlConnection sqlCon = new SqlConnection(connstring))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from patient", sqlCon);
                    DataTable dtb1 = new DataTable();
                    sqlDa.Fill(dtb1);
                    dataGridView1.DataSource = dtb1;
                }
            }
           // sql.Close();
        }
    }
}
