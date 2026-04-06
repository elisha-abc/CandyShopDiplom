using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString =
            @"Server=DESKTOP-BO1UKIT\SQLEXPRESS;Database=CandyShopDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}