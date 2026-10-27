
using Microsoft.Data.SqlClient;
using StudentCourseManagement.Helpers;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Repository;

public class EnrollmentRepository
{
    public void Add(Enrollment enrollment)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"INSERT INTO Enrollments
            (StudentId, CourseId, EnrolledAt, Status)
            VALUES
            (@StudentId, @CourseId, @EnrolledAt, @Status)";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
        cmd.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
        cmd.Parameters.AddWithValue("@EnrolledAt", enrollment.EnrolledAt);
        cmd.Parameters.AddWithValue("@Status", enrollment.Status);

        cmd.ExecuteNonQuery();
    }

    public List<Enrollment> GetAll()
    {
        List<Enrollment> enrollments = new();

        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Enrollments";

        using SqlCommand cmd = new SqlCommand(query, con);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Enrollment enrollment = new Enrollment
            {
                EnrollmentId = Convert.ToInt32(reader["EnrollmentId"]),
                StudentId = Convert.ToInt32(reader["StudentId"]),
                CourseId = Convert.ToInt32(reader["CourseId"]),
                EnrolledAt = Convert.ToDateTime(reader["EnrolledAt"]),
                Status = reader["Status"].ToString()!
            };

            enrollments.Add(enrollment);
        }

        return enrollments;
    }

    public Enrollment? GetById(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query =
            "SELECT * FROM Enrollments WHERE EnrollmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Enrollment
            {
                EnrollmentId = Convert.ToInt32(reader["EnrollmentId"]),
                StudentId = Convert.ToInt32(reader["StudentId"]),
                CourseId = Convert.ToInt32(reader["CourseId"]),
                EnrolledAt = Convert.ToDateTime(reader["EnrolledAt"]),
                Status = reader["Status"].ToString()!
            };
        }

        return null;
    }

    public void Update(Enrollment enrollment)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"UPDATE Enrollments SET
            StudentId = @StudentId,
            CourseId = @CourseId,
            EnrolledAt = @EnrolledAt,
            Status = @Status
            WHERE EnrollmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@Id", enrollment.EnrollmentId);
        cmd.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
        cmd.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
        cmd.Parameters.AddWithValue("@EnrolledAt", enrollment.EnrolledAt);
        cmd.Parameters.AddWithValue("@Status", enrollment.Status);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query =
            "DELETE FROM Enrollments WHERE EnrollmentId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}
