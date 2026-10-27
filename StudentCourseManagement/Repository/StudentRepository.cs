
using Microsoft.Data.SqlClient;
using StudentCourseManagement.Helpers;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Repository;

public class StudentRepository
{
    public void Add(Student student)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"INSERT INTO Students
            (Name, Email, Age, DepartmentId, AdmissionDate, IsActive)
            VALUES
            (@Name, @Email, @Age, @DepartmentId, @AdmissionDate, @IsActive)";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@Name", student.Name);
        cmd.Parameters.AddWithValue("@Email", student.Email);
        cmd.Parameters.AddWithValue("@Age", student.Age);
        cmd.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
        cmd.Parameters.AddWithValue("@AdmissionDate", student.AdmissionDate);
        cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

        cmd.ExecuteNonQuery();
    }

    public List<Student> GetAll()
    {
        List<Student> students = new();

        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Students";

        using SqlCommand cmd = new SqlCommand(query, con);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Student student = new Student
            {
                StudentId = Convert.ToInt32(reader["StudentId"]),
                Name = reader["Name"].ToString()!,
                Email = reader["Email"].ToString()!,
                Age = Convert.ToInt32(reader["Age"]),
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                AdmissionDate = Convert.ToDateTime(reader["AdmissionDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };

            students.Add(student);
        }

        return students;
    }

    public Student? GetById(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Students WHERE StudentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Student
            {
                StudentId = Convert.ToInt32(reader["StudentId"]),
                Name = reader["Name"].ToString()!,
                Email = reader["Email"].ToString()!,
                Age = Convert.ToInt32(reader["Age"]),
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                AdmissionDate = Convert.ToDateTime(reader["AdmissionDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }

        return null;
    }

    public void Update(Student student)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"UPDATE Students SET
            Name = @Name,
            Email = @Email,
            Age = @Age,
            DepartmentId = @DepartmentId,
            AdmissionDate = @AdmissionDate,
            IsActive = @IsActive
            WHERE StudentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@Id", student.StudentId);
        cmd.Parameters.AddWithValue("@Name", student.Name);
        cmd.Parameters.AddWithValue("@Email", student.Email);
        cmd.Parameters.AddWithValue("@Age", student.Age);
        cmd.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
        cmd.Parameters.AddWithValue("@AdmissionDate", student.AdmissionDate);
        cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "DELETE FROM Students WHERE StudentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}
