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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hospital_EDP
{
    public partial class patient_update : Form
    {
        private databaseConnection conn;
        string update_id = patients.update_id;
        string name = "";
        string dateofbirth = "";
        string address = "";
        string phonenumber = "";
        string insuranceid = "";
        string admitdate = "";
        string dischargeid = "";
        string departmentid = "";

        public patient_update()
        {
            InitializeComponent();
            conn = new databaseConnection();

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "SELECT * FROM hospital_bills.patients WHERE PatientID = @update_id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@update_id", update_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader["Name"].ToString();
                            dateofbirth = reader["DateOfBirth"].ToString();
                            address = reader["Address"].ToString();
                            phonenumber = reader["PhoneNumber"].ToString();
                            insuranceid = reader["InsuranceID"].ToString();
                            admitdate = reader["AdmitDate"].ToString();
                            dischargeid = reader["DischargeDate"].ToString();
                            departmentid = reader["DepartmentID"].ToString();

                            textBox_name.Text = name;
                            textBox_address.Text = address;
                            textBox_dateofbirth.Text = dateofbirth;
                            textBox_phonenumber.Text = phonenumber;
                            textBox_insuranceid.Text = insuranceid;
                            textBox_admitdate.Text = admitdate;
                            textBox_dischargedate.Text = dischargeid;
                            textBox_departmentid.Text = departmentid;
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

        }

        private void patient_update_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox_name.Text;
            string address =textBox_address.Text;
            string dateofbirth = textBox_dateofbirth.Text;
            string phonenumber = textBox_phonenumber.Text;
            string insuranceid = textBox_insuranceid.Text;
            string admitdate = textBox_admitdate.Text;
            string dischargedate = textBox_dischargedate.Text;
            string departmentid = textBox_departmentid.Text;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(dateofbirth))
            {
                MessageBox.Show("Make sure the name, address and date of birth has value.");
            }

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "UPDATE `hospital_bills`.`patients` SET `Name` = @name, `DateOfBirth` = @dateofbirth, `Address` = @address, `PhoneNumber` = @phonenumber, `InsuranceID` = @insuranceid," +
                        " `AdmitDate` = @admitdate, `DischargeDate` = @dischargedate, `DepartmentID` = @departmentid WHERE (`PatientID` = @update_id);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@update_id", update_id);
                    cmd.Parameters.AddWithValue("@departmentid", departmentid);
                    cmd.Parameters.AddWithValue("@dischargedate", dischargedate);
                    cmd.Parameters.AddWithValue("@admitdate", admitdate);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@dateofbirth", dateofbirth);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
                    cmd.Parameters.AddWithValue("@insuranceid", insuranceid);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update successful.");
                        var patient = new patients();
                        this.Hide();
                        patient.Show();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
