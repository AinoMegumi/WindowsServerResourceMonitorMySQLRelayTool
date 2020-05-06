using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    class MySQLExecution
    {
        private MySqlConnection connect;
        public MySQLExecution(string HostName, string User, string Password, string Database)
            : this("Server=" + HostName + ";User ID=" + User + ";Password=" + Password + ";Database=" + Database) { }
        public MySQLExecution(string ConnectInformation)
        {
            connect = new MySqlConnection(ConnectInformation);
        }

        public void Open()
        {
            connect.Open();
        }

        public void Close()
        {
            connect.Close();
        }

        public MySqlCommand CreateCommand(string ExecCommand)
        {
            return new MySqlCommand(ExecCommand, connect);
        }

        public static void Execute(MySqlCommand cmd)
        {
            cmd.ExecuteNonQuery();
        }

        public static MySqlDataReader ExecuteQuery(MySqlCommand cmd)
        {
            return cmd.ExecuteReader();
        }
    }
}
