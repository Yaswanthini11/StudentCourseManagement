
using Microsoft.Data.SqlClient;
using StudentCourseManagement.Helpers;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Repository;

public class AuditLogRepository
{
    public void Add(AuditLog auditLog)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"INSERT INTO AuditLogs
            (EntityName, EntityId, Action, Details, CreatedAt)
            VALUES
            (@EntityName, @EntityId, @Action, @Details, @CreatedAt)";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@EntityName", auditLog.EntityName);
        cmd.Parameters.AddWithValue("@EntityId", auditLog.EntityId);
        cmd.Parameters.AddWithValue("@Action", auditLog.Action);
        cmd.Parameters.AddWithValue("@Details",
            (object?)auditLog.Details ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CreatedAt", auditLog.CreatedAt);

        cmd.ExecuteNonQuery();
    }

    public List<AuditLog> GetAll()
    {
        List<AuditLog> auditLogs = new();

        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM AuditLogs ORDER BY AuditId DESC";

        using SqlCommand cmd = new SqlCommand(query, con);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            AuditLog auditLog = new AuditLog
            {
                AuditId = Convert.ToInt64(reader["AuditId"]),
                EntityName = reader["EntityName"].ToString()!,
                EntityId = Convert.ToInt32(reader["EntityId"]),
                Action = reader["Action"].ToString()!,
                Details = reader["Details"] == DBNull.Value
                    ? null : reader["Details"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };

            auditLogs.Add(auditLog);
        }

        return auditLogs;
    }

    public AuditLog? GetById(long id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM AuditLogs WHERE AuditId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new AuditLog
            {
                AuditId = Convert.ToInt64(reader["AuditId"]),
                EntityName = reader["EntityName"].ToString()!,
                EntityId = Convert.ToInt32(reader["EntityId"]),
                Action = reader["Action"].ToString()!,
                Details = reader["Details"] == DBNull.Value
                    ? null : reader["Details"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }

        return null;
    }
}
