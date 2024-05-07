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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OfficeOpenXml;
using System.IO;
using LicenseContext = OfficeOpenXml.LicenseContext; //EPP packages to manipulate the excel file
using OfficeOpenXml.Drawing.Chart;// for charts and graphs
using OfficeOpenXml.Style; // changes fonts, style, etc


namespace Hospital_EDP                 //Hospi Update
{
    public partial class patients : Form
    {
        private databaseConnection conn;
        public static string update_id;

        public patients()
        {
            InitializeComponent();
            conn = new databaseConnection();
            panel_delete_patient.Hide();
            panel_update_patient.Hide();
            panel_dashboard_about.Hide();
            panel_doctor_profile.Hide();

            EventHandler dataGridView_SelectionChanged = null;
            dataGridView1.SelectionChanged += dataGridView_SelectionChanged;

            DateTime currTime = DateTime.Now;
            patient_time.Text = currTime.ToString("dddd, dd/MM/yyyy hh:mm tt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var add_patient = new add_form();
            this.Hide();
            add_patient.Show();
        }

        private void patients_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            try
            {
                if (conn.OpenConnection())
                {
                    string sqlQuery = "SELECT * FROM hospital_bills.patients";
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, conn.connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable originalDataTable = new DataTable();
                    adapter.Fill(originalDataTable);
                    dataGridView1.DataSource = originalDataTable;
                    conn.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Failed to connect to database");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            panel_delete_patient.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string delete_passedID = textBox_delete.Text;

            if (string.IsNullOrEmpty(delete_passedID))
            {
                MessageBox.Show("Enter preferred ID to delete");
            }
            else
            {
                if (conn.OpenConnection())
                {
                    try
                    {
                        string selectSql = "SELECT COUNT(*) FROM hospital_bills.patients WHERE PatientID = @delete_passedID;";
                        MySqlCommand selectCmd = new MySqlCommand(selectSql, conn.connection);
                        selectCmd.Parameters.AddWithValue("@delete_passedID", delete_passedID);

                        int count = Convert.ToInt32(selectCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            string deleteSql = "DELETE FROM hospital_bills.patients WHERE PatientID = @delete_passedID;";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteSql, conn.connection);
                            deleteCmd.Parameters.AddWithValue("@delete_passedID", delete_passedID);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Patient Deleted");
                                panel_delete_patient.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the Patient.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Patient with the specified ID does not exist.");
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

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel_update_patient.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel_update_patient.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel_delete_patient.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string update_passedID = textBox_update.Text;
            if (string.IsNullOrEmpty(update_passedID))
            {
                MessageBox.Show("Enter preferred ID to update");
            }
            else
            {
                if (conn.OpenConnection())
                {
                    try
                    {
                        string selectSql = "SELECT COUNT(*) FROM hospital_bills.patients WHERE PatientID = @update_passedID;";
                        MySqlCommand selectCmd = new MySqlCommand(selectSql, conn.connection);
                        selectCmd.Parameters.AddWithValue("@update_passedID", update_passedID);

                        int count = Convert.ToInt32(selectCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            update_id = update_passedID.ToString();
                            var patient_Updated = new patient_update();
                            this.Hide();
                            patient_Updated.Show();

                        }
                        else
                        {
                            MessageBox.Show("Patient with the specified ID does not exist.");
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var logout = new Form1();
            this.Hide();
            logout.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dashboard = new dashboard();
            this.Hide();
            dashboard.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel_dashboard_about.Show();
            panel_doctor_profile.Hide();
            button_abou.BackColor = Color.DodgerBlue;
            button_abou.ForeColor = Color.White;
            button_doctors.BackColor = Color.AliceBlue;
            button_doctors.ForeColor = Color.Black;
            button_dash.BackColor = Color.AliceBlue;
            button_dash.ForeColor = Color.Black;
            button_pat.BackColor = Color.AliceBlue;
            button_pat.ForeColor = Color.Black;

        }

        private void button_doctors_Click(object sender, EventArgs e)
        {

            panel_doctor_profile.Show();
            panel_dashboard_about.Hide();
            button_abou.BackColor = Color.AliceBlue;
            button_abou.ForeColor = Color.Black;
            button_doctors.BackColor = Color.DodgerBlue;
            button_doctors.ForeColor = Color.White;
            button_dash.BackColor = Color.AliceBlue;
            button_dash.ForeColor = Color.Black;
            button_pat.BackColor = Color.AliceBlue;
            button_pat.ForeColor = Color.Black;
        }

        private void button_pat_Click(object sender, EventArgs e)
        {
            panel_dashboard_about.Hide();
            panel_doctor_profile.Hide();
            button_abou.BackColor = Color.AliceBlue;
            button_abou.ForeColor = Color.Black;
            button_doctors.BackColor = Color.AliceBlue;
            button_doctors.ForeColor = Color.Black;
            button_dash.BackColor = Color.AliceBlue;
            button_dash.ForeColor = Color.Black;
            button_pat.BackColor = Color.DodgerBlue;
            button_pat.ForeColor = Color.White;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\Kenn Clinton\Documents\Hospital Management System.xlsx"; // file path to be saved
            try
            {
                conn.OpenConnection();

                string reportquery = "SELECT Name, AdmitDate, DischargeDate FROM hospital_bills.patients"; //Query to get Name, Admit and Discharge Date
                string sqlQuery = "SELECT d.DepartmentName, COUNT(p.PatientID) AS TotalPatients FROM hospital_bills.departments d LEFT JOIN hospital_bills.patients p ON d.DepartmentID = p.DepartmentID GROUP BY d.DepartmentName;";

                using (MySqlCommand Command = new MySqlCommand(reportquery, conn.connection))
                {
                    using (MySqlDataReader Reader = Command.ExecuteReader())
                     {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        // Create a new Excel package
                        ExcelPackage excelPackage = new ExcelPackage(); //using the excel package

                        // Add a worksheet to the Excel package
                        ExcelWorksheet worksheet_1 = excelPackage.Workbook.Worksheets.Add("Patients"); //creating first sheet
                        ExcelWorksheet worksheet_2 = excelPackage.Workbook.Worksheets.Add("Graphs"); // second sheet

                        //for logo
                        var picture = worksheet_1.Drawings.AddPicture("Logo", new FileInfo("C:\\Users\\Kenn Clinton\\Documents\\Hospital_EDP\\Hospital_EDP\\Hospital_EDP\\Resources\\Hospital-removebg-preview.png"));
                        // Set the size of the picture
                        picture.SetSize(70, 70); // Set the picture size in pixels

                        // Set the width of column 1 and height of row 1
                        worksheet_1.Column(1).Width = 13.71;
                        worksheet_1.Row(1).Height = 52.50;
                        worksheet_1.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet_1.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet_1.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#6495ED"));

                        // Calculate the offsets to center the picture in cell A1
                        double cellWidth = worksheet_1.Column(1).Width;
                        double cellHeight = worksheet_1.Row(1).Height;
                        double xOffset = cellWidth / 13.71; // Calculate horizontal offset
                        double yOffset = cellHeight / 4; // Calculate vertical offset

                        // Set the position of the picture to center it in cell A1
                        picture.SetPosition(0, (int)xOffset, 0, (int)yOffset);


                        //Merging and adding the company name
                        ExcelRange cellsToMerge = worksheet_1.Cells["B1:F1"];
                        cellsToMerge.Merge = true;
                        cellsToMerge.Value = "Hospital Management System";
                        cellsToMerge.Style.Font.Size = 20;
                        cellsToMerge.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cellsToMerge.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#6495ED"));
                        cellsToMerge.Style.Font.Name = "Impact";
                        cellsToMerge.Style.Font.Color.SetColor(Color.White);
                        cellsToMerge.Style.Font.Bold = true;
                        cellsToMerge.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cellsToMerge.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        ExcelRange cellsToMerge4 = worksheet_1.Cells["C4:E4"];
                        cellsToMerge4.Merge = true;
                        cellsToMerge4.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cellsToMerge4.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#6495ED"));
                        cellsToMerge4.Style.Font.Name = "Impact";
                        cellsToMerge4.Style.Font.Color.SetColor(Color.White);
                        cellsToMerge4.Value = "Patients Admission & Discharges Date";
                        cellsToMerge4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cellsToMerge4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        // Add column headers
                        worksheet_1.Cells[5, 3].Value = "Patient Name";
                        worksheet_1.Cells[5, 4].Value = "Admission Date";
                        worksheet_1.Cells[5, 5].Value = "Discharge Date";


                        int row = 6;
                        while (Reader.Read())
                        {
                            worksheet_1.Cells[row, 3].Value = Reader["Name"];
                            worksheet_1.Cells[row, 4].Value = DateTime.Parse(Reader["AdmitDate"].ToString()).ToString("dd/MM/yyyy");
                            worksheet_1.Cells[row, 5].Value = DateTime.Parse(Reader["DischargeDate"].ToString()).ToString("dd/MM/yyyy");
                            row++;
                        }


                        worksheet_1.Cells[16, 6].Value = "_________________________";
                        worksheet_1.Cells[17, 6].Value = "Doctor";

                        worksheet_1.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet_1.Cells.AutoFitColumns();

                        var picture1 = worksheet_2.Drawings.AddPicture("Logo", new FileInfo("C:\\Users\\Kenn Clinton\\Documents\\Hospital_EDP\\Hospital_EDP\\Hospital_EDP\\Resources\\Hospital-removebg-preview.png"));
                        // Set the size of the picture
                        picture1.SetSize(70, 70); // Set the picture size in pixels

                        // Set the width of column 1 and height of row 1
                        worksheet_2.Column(1).Width = 13.71;
                        worksheet_2.Row(1).Height = 52.50;
                        worksheet_2.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet_2.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet_2.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#6495ED"));

                        // Calculate the offsets to center the picture in cell A1
                        double cellWidths = worksheet_2.Column(1).Width;
                        double cellHeights = worksheet_2.Row(1).Height;
                        double xOffsets = cellWidths / 13.71; // Calculate horizontal offset
                        double yOffsets = cellHeights / 4; // Calculate vertical offset

                        // Set the position of the picture to center it in cell A1
                        picture.SetPosition(0, (int)xOffsets, 0, (int)yOffsets);


                        //Merging and adding the company name
                        ExcelRange cellsToMerge1 = worksheet_2.Cells["B1:H1"];
                        cellsToMerge1.Merge = true;
                        cellsToMerge1.Value = "Hospital Management System";
                        cellsToMerge1.Style.Font.Size = 20;
                        cellsToMerge1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cellsToMerge1.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#6495ED"));
                        cellsToMerge1.Style.Font.Name = "Impact";
                        cellsToMerge1.Style.Font.Color.SetColor(Color.White);
                        cellsToMerge1.Style.Font.Bold = true;
                        cellsToMerge1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cellsToMerge1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet_2.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet_2.Cells.AutoFitColumns();

                        Reader.Close();

                        using (MySqlCommand graphCommand = new MySqlCommand(sqlQuery, conn.connection))
                        {
                            using (MySqlDataReader graphReader = graphCommand.ExecuteReader())
                            {
                                // Create lists to store department names and corresponding total patients
                                List<string> departmentNames = new List<string>();
                                List<int> totalPatients = new List<int>();

                                // Read data from the database and populate the lists
                                while (graphReader.Read())
                                {
                                    departmentNames.Add(graphReader.GetString("DepartmentName")); // Assuming "DepartmentName" is the correct column name
                                    totalPatients.Add(graphReader.GetInt32("TotalPatients"));
                                }

                                    // Add data to the Excel worksheet
                                    for (int i = 0; i < departmentNames.Count; i++)
                                    {
                                        worksheet_2.Cells[i + 3, 3].Value = departmentNames[i];
                                        worksheet_2.Cells[i + 3, 4].Value = totalPatients[i];
                                    }

                                    // Create the chart
                                    var chart = worksheet_2.Drawings.AddChart("ChartName", eChartType.PieExploded3D);

                                    // Set the position of the chart
                                    chart.SetPosition(2, 0, 1, 0);

                                    chart.SetSize(385, 300);

                                    // Add data to the chart
                                    chart.Series.Add(worksheet_2.Cells["D3:D" + (totalPatients.Count + 2)], worksheet_2.Cells["C3:C" + (totalPatients.Count + 2)]);

                                    // Add custom labels to the data points for the first chart
                                    for (int i = 0; i < chart.Series.Count; i++)
                                    {
                                        ExcelPieChartSerie pieSeries = (ExcelPieChartSerie)chart.Series[i];
                                        pieSeries.DataLabel.ShowValue = true;
                                        pieSeries.DataLabel.ShowLeaderLines = true;
                                    }
                                    // Set the chart title
                                    chart.Title.Text = "Department-wise Distribution of Patients";

                                    // Set the legend position
                                    chart.Legend.Position = eLegendPosition.Bottom;

                                    // Set the chart style
                                    chart.Style = eChartStyle.Style18;

                                // Add a signature line
                                worksheet_2.Cells[19, 7].Value = "_________________________";
                                worksheet_2.Cells[20, 7].Value = "Doctor";
                                // Save the Excel file
                                excelPackage.SaveAs(new FileInfo(filePath));

                                // Dispose the ExcelPackage object
                                excelPackage.Dispose();

                                MessageBox.Show("Excel file saved successfully!");
                            }
                        }
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
