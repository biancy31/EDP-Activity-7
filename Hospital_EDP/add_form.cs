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
    public partial class add_form : Form
    {
        private databaseConnection conn;
        public add_form()
        {
            InitializeComponent();
            conn = new databaseConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var login = new Form1();
            this.Hide();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string patientID = textBox_patientID.Text;
            string name = textBox_name.Text;
            string address = textBox_address.Text;
            string dateofbirth = textBox_dateofbirth.Text;
            string phonenumber = textBox_phonenumber.Text;
            string insuranceid = textBox_insuranceid.Text;
            string admitdate = textBox_admitdate.Text;
            string dischargedate = textBox_dischargedate.Text;
            string departmentid = textBox_departmentid.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(dateofbirth))
            {
                MessageBox.Show("Make sure the name, address and date of birth has value.");
            }

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "INSERT INTO hospital_bills.patients (`PatientID`,`Name`, `DateOfBirth`, `Address`, `PhoneNumber`, `InsuranceID`, `AdmitDate`, `DischargeDate`, `DepartmentID`) " +
                              "VALUES (@patientID,@name, @dateofbirth, @address, @phonenumber, @insuranceid, @admitdate, @dischargedate, @departmentid);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@dateofbirth", dateofbirth);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
                    cmd.Parameters.AddWithValue("@insuranceid", insuranceid);
                    cmd.Parameters.AddWithValue("@admitdate", admitdate);
                    cmd.Parameters.AddWithValue("@dischargedate", dischargedate);
                    cmd.Parameters.AddWithValue("@departmentid", departmentid);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Add successful.");
                        var add_patient = new patients();
                        this.Hide();
                        add_patient.Show();
                    }
                    else
                    {
                        MessageBox.Show("No rows were added.");
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

        private void add_form_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox_insuranceid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
