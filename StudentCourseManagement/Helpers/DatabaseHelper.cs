using Microsoft.Data.SqlClient;

namespace StudentCourseManagement.Helpers;

public static class DatabaseHelper
{
    private static readonly string connectionString =
        @"Server=.\SQLEXPRESS;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}