using System;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public static class Logger
    {
        public static void Add(string action)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Logs (UserLogin, Action, ActionDate)
                        VALUES (@login, @action, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", Session.CurrentLogin);
                        cmd.Parameters.AddWithValue("@action", action);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }
    }
}