
using Microsoft.Data.SqlClient;
using StudentCourseManagement.Helpers;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Repository;

public class CourseRepository
{
    public void Add(Course course)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"INSERT INTO Courses
            (CourseName, DurationMonths, Fee, DepartmentId, IsActive)
            VALUES
            (@CourseName, @DurationMonths, @Fee, @DepartmentId, @IsActive)";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
        cmd.Parameters.AddWithValue("@DurationMonths", course.DurationMonths);
        cmd.Parameters.AddWithValue("@Fee", course.Fee);
        cmd.Parameters.AddWithValue("@DepartmentId", course.DepartmentId);
        cmd.Parameters.AddWithValue("@IsActive", course.IsActive);

        cmd.ExecuteNonQuery();
    }

    public List<Course> GetAll()
    {
        List<Course> courses = new();

        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Courses";

        using SqlCommand cmd = new SqlCommand(query, con);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Course course = new Course
            {
                CourseId = Convert.ToInt32(reader["CourseId"]),
                CourseName = reader["CourseName"].ToString()!,
                DurationMonths = Convert.ToInt32(reader["DurationMonths"]),
                Fee = Convert.ToDecimal(reader["Fee"]),
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };

            courses.Add(course);
        }

        return courses;
    }

    public Course? GetById(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "SELECT * FROM Courses WHERE CourseId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Course
            {
                CourseId = Convert.ToInt32(reader["CourseId"]),
                CourseName = reader["CourseName"].ToString()!,
                DurationMonths = Convert.ToInt32(reader["DurationMonths"]),
                Fee = Convert.ToDecimal(reader["Fee"]),
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }

        return null;
    }

    public void Update(Course course)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"UPDATE Courses SET
            CourseName = @CourseName,
            DurationMonths = @DurationMonths,
            Fee = @Fee,
            DepartmentId = @DepartmentId,
            IsActive = @IsActive
            WHERE CourseId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@Id", course.CourseId);
        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
        cmd.Parameters.AddWithValue("@DurationMonths", course.DurationMonths);
        cmd.Parameters.AddWithValue("@Fee", course.Fee);
        cmd.Parameters.AddWithValue("@DepartmentId", course.DepartmentId);
        cmd.Parameters.AddWithValue("@IsActive", course.IsActive);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection con = DatabaseHelper.GetConnection();
        con.Open();

        string query = "DELETE FROM Courses WHERE CourseId = @Id";

        using SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}
