
using StudentCourseManagement.Models;
using StudentCourseManagement.Repository;

namespace StudentCourseManagement.Services;

public class CourseService
{
    private readonly CourseRepository _repository;

    public CourseService()
    {
        _repository = new CourseRepository();
    }

    public void AddCourse(Course course)
    {
        if (string.IsNullOrWhiteSpace(course.CourseName))
            throw new ArgumentException("Course name is required.");

        if (course.DurationMonths <= 0)
            throw new ArgumentException(
                "Course duration must be greater than zero.");

        if (course.Fee < 0)
            throw new ArgumentException("Course fee cannot be negative.");

        if (course.DepartmentId <= 0)
            throw new ArgumentException("Enter a valid department ID.");

        _repository.Add(course);
    }

    public List<Course> GetAllCourses()
    {
        return _repository.GetAll();
    }

    public Course? GetCourseById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid course ID.");

        return _repository.GetById(id);
    }

    public void UpdateCourse(Course course)
    {
        if (course.CourseId <= 0)
            throw new ArgumentException("Invalid course ID.");

        if (string.IsNullOrWhiteSpace(course.CourseName))
            throw new ArgumentException("Course name is required.");

        if (course.DurationMonths <= 0)
            throw new ArgumentException(
                "Course duration must be greater than zero.");

        if (course.Fee < 0)
            throw new ArgumentException("Course fee cannot be negative.");

        if (course.DepartmentId <= 0)
            throw new ArgumentException("Enter a valid department ID.");

        _repository.Update(course);
    }

    public void DeleteCourse(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid course ID.");

        _repository.Delete(id);
    }
}
