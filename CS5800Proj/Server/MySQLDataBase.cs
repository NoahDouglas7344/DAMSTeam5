using System;
using MySql.Data.MySqlClient;

namespace CS5800Proj
{
    public class MySQLDataBase:IDisposable
    {
        public MySqlConnection Connection;

        public MySQLDataBase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            
        }
    }
}
