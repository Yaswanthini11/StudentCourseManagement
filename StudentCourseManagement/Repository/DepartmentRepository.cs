
using Microsoft.Data.SqlClient;
using StudentCourseManagement.Helpers;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Repository;

public class DepartmentRepository
{
    public void Add(Department department)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"INSERT INTO Departments
                         (DepartmentName, Location, IsActive)
                         VALUES (@Name, @Location, @IsActive)";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Name", department.DepartmentName);
        cmd.Parameters.AddWithValue("@Location",
            (object?)department.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@IsActive", department.IsActive);

        cmd.ExecuteNonQuery();
    }

    public List<Department> GetAll()
    {
        List<Department> departments = new();

        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Departments";

        using SqlCommand cmd = new SqlCommand(query, con);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Department department = new Department
            {
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                DepartmentName = reader["DepartmentName"].ToString()!,
                Location = reader["Location"] == DBNull.Value
                    ? null : reader["Location"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };

            departments.Add(department);
        }

        return departments;
    }

    public Department? GetById(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query =
            "SELECT * FROM Departments WHERE DepartmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Department
            {
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                DepartmentName = reader["DepartmentName"].ToString()!,
                Location = reader["Location"] == DBNull.Value
                    ? null : reader["Location"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }

        return null;
    }

    public void Update(Department department)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"UPDATE Departments
                         SET DepartmentName = @Name,
                             Location = @Location,
                             IsActive = @IsActive
                         WHERE DepartmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", department.DepartmentId);
        cmd.Parameters.AddWithValue("@Name", department.DepartmentName);
        cmd.Parameters.AddWithValue("@Location",
            (object?)department.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@IsActive", department.IsActive);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query =
            "DELETE FROM Departments WHERE DepartmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}