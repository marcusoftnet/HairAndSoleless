using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;

namespace Specs.EndToEnd.Steps.Infrastructure
{
    public static class DbHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["HairAndSolelessContext"].ConnectionString;
        
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

        public static void RemoveCoachesByName(string coachName)
        {
            var commandText = string.Format("DELETE FROM Coaches WHERE Name ='{0}'", coachName);
            ExecuteCommand(commandText);
        }

        public static void RemoveCustomersByName(string customerName)
        {
            var commandText = string.Format("DELETE FROM Customers WHERE Name ='{0}'", customerName);
            ExecuteCommand(commandText);
        }

        public static void RemoveActivitiesForCoachByName(string coachToDeleteActivitesFor)
        {
            var commandText = string.Format("DELETE FROM Activities WHERE (CoachId IN (SELECT CoachId FROM Coaches WHERE Name = '{0}'))", coachToDeleteActivitesFor);
            ExecuteCommand(commandText);
        }

        public static void CreateCoach(string name, string email, string testTeam)
        {
            var commandText = string.Format("INSERT INTO Coaches (Name, Email, Team) VALUES ('{0}', '{1}', '{2}')",
                                            name, email, testTeam);
            ExecuteCommand(commandText);
        }

        public static void CreateCustomer(string name, string contact)
        {
            var commandText = string.Format("INSERT INTO Customers (Name, Contact) VALUES ('{0}', '{1}')",
                                           name, contact);
            ExecuteCommand(commandText);
        }
    }
}
