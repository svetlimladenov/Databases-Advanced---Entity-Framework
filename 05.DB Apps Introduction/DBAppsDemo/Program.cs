using System;
using System.Data.SqlClient;
namespace DBAppsDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = new SqlConnection(
                "Server=.;" +
                "Database=SoftUni;" +
                "Integrated Security=True"
            );

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand(
                    "SELECT COUNT(*) FROM Employees",
                    connection);
                var empCount = (int)command.ExecuteScalar();
                Console.WriteLine($"employees count :{empCount}");
            }
        }
    }

}