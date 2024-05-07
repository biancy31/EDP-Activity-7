using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hospital_EDP
{
    public partial class dashboard : Form
    {
        private databaseConnection conn;
        string user_id = Form1.id;
        string username = "";
        public dashboard()
        {
            InitializeComponent();
            conn = new databaseConnection();
            panel_doctor_profile.Hide();
            panel_dashboard_about.Hide();

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "SELECT * FROM hospital_bills.doctors WHERE DoctorID = @user_id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader["Name"].ToString();
                            label_username.Text = username;
                        }
                        else
                        {
                            MessageBox.Show("User not found in the database!");
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

            DateTime currTime = DateTime.Now;
            dashboard_time.Text = currTime.ToString("dddd, dd/MM/yyyy hh:mm tt");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var to_patient = new patients();
            this.Hide();
            to_patient.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var logout = new Form1();
            this.Hide();
            logout.Show();
        }

        private void button_doctors_Click(object sender, EventArgs e)
        {
            panel_dashboard_about.Hide();
            panel_doctor_profile.Show();
            button_doctors.BackColor = Color.DodgerBlue;
            button_doctors.ForeColor = Color.AliceBlue;
            button_dashboard.BackColor = Color.AliceBlue;
            button_dashboard.ForeColor = Color.Black;
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button_about_Click(object sender, EventArgs e)
        {
            panel_dashboard_about.Show();
            panel_doctor_profile.Hide();
            button_dashboard.BackColor = Color.AliceBlue;
            button_dashboard.ForeColor = Color.Black;
            button_doctors.BackColor = Color.AliceBlue;
            button_doctors.ForeColor = Color.Black;
            button_about.BackColor = Color.DodgerBlue;
            button_about.ForeColor = Color.AliceBlue;
        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            panel_dashboard_about.Hide();
            panel_doctor_profile.Hide();
            button_dashboard.BackColor = Color.DodgerBlue;
            button_dashboard.ForeColor = Color.AliceBlue;
            button_doctors.BackColor = Color.AliceBlue;
            button_doctors.ForeColor = Color.Black;
            button_about.BackColor = Color.AliceBlue;
            button_about.ForeColor = Color.Black;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
