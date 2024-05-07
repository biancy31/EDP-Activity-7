using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hospital_EDP
{
    public partial class forgot_pass : Form
    {
        private databaseConnection conn;
        public static string email;
        string sendCode;
        string to = "";
        public forgot_pass()
        {
            InitializeComponent();
            conn = new databaseConnection();
            panel_otp.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input_email = textBox1_email.Text;
            bool verify = false;


            if (string.IsNullOrEmpty(input_email))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (conn.OpenConnection())
            {
                string sql = "SELECT Email_Address FROM hospital_bills.doctors WHERE Email_Address = @input_email";
                MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                cmd.Parameters.AddWithValue("@input_email", input_email);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = reader["Email_Address"].ToString();
                        verify = true;
                    }
                }

                if (verify == true)
                {
                    string from, pass, messageBody;
                    Random rand = new Random();
                    MailMessage message = new MailMessage();
                    sendCode = (rand.Next(999999)).ToString();
                    to = email;
                    from = "biancacanotal26@gmail.com";
                    pass = "rmuamxybbwddyciu";
                    messageBody = "Your password recovery code: " + sendCode;

                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "Password Recovery Code";

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code send successfully");
                        panel_otp.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    panel_otp.Hide();
                }

                conn.CloseConnection();

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string varify_code = textBox_verifyOTP.Text;

            if(string.IsNullOrEmpty(varify_code))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(varify_code == sendCode)
            {
                MessageBox.Show("Code match!");
                var update_Password = new update_password();
                this.Hide();
                update_Password.Show();
            }
            else
            {
                MessageBox.Show("Code not match!");
            }
            conn.CloseConnection();

            
        }

        private void forgot_pass_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
