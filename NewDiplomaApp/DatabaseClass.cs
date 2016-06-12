using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DiplomaApp
{
    public class DatabaseClass : ConnectionClass
    {

        private string connectionString;

        public MySqlConnection conn;
        public MySqlCommand cmd;
        public MySqlDataReader rdr;
        public string databaseName { get; set; }
       

        public DatabaseClass()
        {
            //pusty konstruktor
        }
        public DatabaseClass(string databaseName, string serverName, string username, string password)
        {
            this.databaseName = databaseName;
            this.serverName = serverName;
            this.username = username;
            this.password = password;
           
            connectionString = "Server=" + serverName +
                    ";Database=" + databaseName +
                    ";Uid=" + username +
                    ";Pwd=" + password + ";";

            Console.WriteLine(connectionString);
            connect(connectionString);
        }

        public void connect(string connStr)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();           
        }

        public override int getConnectionStatus()
        {
            return (int)conn.State;
        }

        public bool isConnected()
        {
            if (conn.State == System.Data.ConnectionState.Open) return true;
            return false;
        }

        public override string getConnectionInfo()
        {
            throw new NotImplementedException();
        }
        public bool selectFromDatabase()
        {
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM ttr_dyplomy";

            if(rdr == null || rdr.IsClosed) rdr = cmd.ExecuteReader();
            

            return true;
        }
        public void closeConnection()
        {
            conn.Close();
        }



    }
}
