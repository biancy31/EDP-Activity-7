using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hospital_EDP
{
    internal class databaseConnection
    {
        public MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;

        public databaseConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "hospital_bills";
            uid = "root";
            password = "hellobiancy";
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
    }
}

