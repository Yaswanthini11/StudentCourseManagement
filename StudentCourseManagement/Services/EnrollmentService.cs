
using StudentCourseManagement.Models;
using StudentCourseManagement.Repository;

namespace StudentCourseManagement.Services;

public class EnrollmentService
{
    private readonly EnrollmentRepository _repository;
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;

    public EnrollmentService()
    {
        _repository = new EnrollmentRepository();
        _studentRepository = new StudentRepository();
        _courseRepository = new CourseRepository();
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        if (enrollment.StudentId <= 0)
            throw new ArgumentException("Invalid student ID.");

        if (enrollment.CourseId <= 0)
            throw new ArgumentException("Invalid course ID.");

        Student? student = _studentRepository.GetById(enrollment.StudentId);

        if (student == null)
            throw new ArgumentException("Student does not exist.");

        Course? course = _courseRepository.GetById(enrollment.CourseId);

        if (course == null)
            throw new ArgumentException("Course does not exist.");

        if (string.IsNullOrWhiteSpace(enrollment.Status))
            throw new ArgumentException("Enrollment status is required.");

        if (enrollment.EnrolledAt == default)
            enrollment.EnrolledAt = DateTime.Now;

        _repository.Add(enrollment);
    }

    public List<Enrollment> GetAllEnrollments()
    {
        return _repository.GetAll();
    }

    public Enrollment? GetEnrollmentById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid enrollment ID.");

        return _repository.GetById(id);
    }

    public void UpdateEnrollment(Enrollment enrollment)
    {
        if (enrollment.EnrollmentId <= 0)
            throw new ArgumentException("Invalid enrollment ID.");

        if (enrollment.StudentId <= 0 || enrollment.CourseId <= 0)
            throw new ArgumentException("Invalid student or course ID.");

        if (_studentRepository.GetById(enrollment.StudentId) == null)
            throw new ArgumentException("Student does not exist.");

        if (_courseRepository.GetById(enrollment.CourseId) == null)
            throw new ArgumentException("Course does not exist.");

        if (string.IsNullOrWhiteSpace(enrollment.Status))
            throw new ArgumentException("Enrollment status is required.");

        _repository.Update(enrollment);
    }

    public void DeleteEnrollment(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid enrollment ID.");

        _repository.Delete(id);
    }
}
