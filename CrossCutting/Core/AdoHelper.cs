using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CrossCutting.Core
{
    public class AdoHelper
    {
        public AdoHelper(string server, string database, string userName = null, string password = null)
        {
            Server = server;
            Database = database;
            UserName = userName;
            Password = password;

            if ((!string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
              || (string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)))
            {
                throw new InvalidOperationException("Username and password , one of them is filled but the other is empty");
            }

            GenerateConnectionString();
        }

        string Server { get; }
        string Database { get; }
        string UserName { get; }
        string Password { get; }

        string ConnectionString { set; get; } = string.Empty;
        private void GenerateConnectionString()
        {
            ConnectionString = $"Server={Server};Database={Database};Trusted_Connection=True;MultipleActiveResultSets=True;";

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                ConnectionString += $"Integrated Security=False;User Id={UserName};Password={Password};";
            else
                ConnectionString += $"Integrated Security=True;";
        }

        public async Task<List<T>> ExecuteQueryAsync<T>(string query)
        {
            List<T> result = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    result = reader.MapToList<T>();
                }
            }

            return result;
        }

        public List<T> ExecuteQuery<T>(List<string> queryList)
        {
            List<T> result = new List<T>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                queryList.ForEach(query =>
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    using (var reader = command.ExecuteReader())
                    {
                        result.AddRange(reader.MapToList<T>());
                    }
                });

            }

            return result;
        }
    }
}
