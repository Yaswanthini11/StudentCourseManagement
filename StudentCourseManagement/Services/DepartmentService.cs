
using StudentCourseManagement.Models;
using StudentCourseManagement.Repository;

namespace StudentCourseManagement.Services;

public class DepartmentService
{
    private readonly DepartmentRepository _repository;

    public DepartmentService()
    {
        _repository = new DepartmentRepository();
    }

    public void AddDepartment(Department department)
    {
        if (string.IsNullOrWhiteSpace(department.DepartmentName))
        {
            throw new ArgumentException("Department name is required.");
        }

        _repository.Add(department);
    }

    public List<Department> GetAllDepartments()
    {
        return _repository.GetAll();
    }

    public Department? GetDepartmentById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid department ID.");

        return _repository.GetById(id);
    }

    public void UpdateDepartment(Department department)
    {
        if (department.DepartmentId <= 0)
            throw new ArgumentException("Invalid department ID.");

        if (string.IsNullOrWhiteSpace(department.DepartmentName))
            throw new ArgumentException("Department name is required.");

        _repository.Update(department);
    }

    public void DeleteDepartment(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid department ID.");

        _repository.Delete(id);
    }
}
