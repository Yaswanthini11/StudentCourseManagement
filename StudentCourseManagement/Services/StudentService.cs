
using StudentCourseManagement.Models;
using StudentCourseManagement.Repository;

namespace StudentCourseManagement.Services;

public class StudentService
{
    private readonly StudentRepository _repository;

    public StudentService()
    {
        _repository = new StudentRepository();
    }

    public void AddStudent(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Student name is required.");

        if (string.IsNullOrWhiteSpace(student.Email) ||
            !student.Email.Contains('@'))
            throw new ArgumentException("Enter a valid email.");

        if (student.Age <= 0)
            throw new ArgumentException("Age must be greater than zero.");

        if (student.DepartmentId <= 0)
            throw new ArgumentException("Enter a valid department ID.");

        _repository.Add(student);
    }

    public List<Student> GetAllStudents()
    {
        return _repository.GetAll();
    }

    public Student? GetStudentById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid student ID.");

        return _repository.GetById(id);
    }

    public void UpdateStudent(Student student)
    {
        if (student.StudentId <= 0)
            throw new ArgumentException("Invalid student ID.");

        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Student name is required.");

        if (string.IsNullOrWhiteSpace(student.Email) ||
            !student.Email.Contains('@'))
            throw new ArgumentException("Enter a valid email.");

        if (student.Age <= 0)
            throw new ArgumentException("Age must be greater than zero.");

        if (student.DepartmentId <= 0)
            throw new ArgumentException("Enter a valid department ID.");

        _repository.Update(student);
    }

    public void DeleteStudent(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid student ID.");

        _repository.Delete(id);
    }
}
