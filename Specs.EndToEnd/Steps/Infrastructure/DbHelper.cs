using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;

namespace Specs.EndToEnd.Steps.Infrastructure
{
    public static class DbHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["HairAndSolelessContext"].ConnectionString;

        public static void RemoveAllCustomersNamed(string customerName)
        {
            var commandText = string.Format("DELETE FROM Customers WHERE Name ='{0}'", customerName);
            ExecuteCommand(commandText);
        }

        private static void ExecuteCommand(string commandText)
        {
            using (var connection = new SqlCeConnection(connString))
            {
                var cmd = new SqlCeCommand(commandText);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = connection;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            
        }
    }
}
