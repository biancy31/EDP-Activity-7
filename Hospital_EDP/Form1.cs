using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_EDP
{
    public partial class Form1 : Form
    {

        private databaseConnection conn;
        public static string id;
        public Form1()
        {
            InitializeComponent();
            conn = new databaseConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var forget_pass = new forgot_pass();
            this.Hide();
            forget_pass.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox_username.Text;
            string password = textBox_password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "SELECT DoctorID FROM hospital_bills.doctors WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader["DoctorID"].ToString();
                            var dashboard = new dashboard();
                            this.Hide();
                            dashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password or username");
                        }
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
