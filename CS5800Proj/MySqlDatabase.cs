using System;
using MySql.Data.MySqlClient;

namespace CS5800Proj
{
    public class MySqlDatabase:IDisposable
    {
        public MySqlConnection Connection;

        public MySqlDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            
        }
    }
}
