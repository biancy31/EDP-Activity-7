using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hospital_EDP
{
    public partial class update_password : Form
    {

        private databaseConnection conn;
        string email_pass = forgot_pass.email;
        public update_password()
        {
            InitializeComponent();
            conn = new databaseConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newpass = textBox2_newpass.Text;
            string confirmpass = textBox1_confirmpass.Text;

            if(string.IsNullOrEmpty(newpass) || string.IsNullOrEmpty(confirmpass))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newpass == confirmpass)
            {
                if (conn.OpenConnection())
                {
                    try
                    {
                        string sql = "UPDATE `hospital_bills`.`doctors` SET `password` = @confirmpass WHERE (`Email_Address` = @email_pass);";
                        MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                        cmd.Parameters.AddWithValue("@email_pass", email_pass);
                        cmd.Parameters.AddWithValue("@confirmpass", confirmpass);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("You've updated your password");
                            var form1 = new Form1();
                            this.Hide();
                            form1.Show();
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        conn.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Password not match!");
            }
            conn.CloseConnection();

            
        }

        private void update_password_Load(object sender, EventArgs e)
        {

        }
    }
}
