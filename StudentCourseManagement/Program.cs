
using StudentCourseManagement.Models;
using StudentCourseManagement.Services;

namespace StudentCourseManagement;

internal class Program
{
    static readonly DepartmentService departmentService = new();
    static readonly StudentService studentService = new();
    static readonly CourseService courseService = new();
    static readonly EnrollmentService enrollmentService = new();
    static readonly AuditLogService auditLogService = new();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== STUDENT COURSE MANAGEMENT =====");
            Console.WriteLine("1. Manage Departments");
            Console.WriteLine("2. Manage Students");
            Console.WriteLine("3. Manage Courses");
            Console.WriteLine("4. Manage Enrollments");
            Console.WriteLine("5. Manage Audit Logs");
            Console.WriteLine("6. Exit");

            int choice = ReadInt("Enter your choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        DepartmentMenu();
                        break;

                    case 2:
                        StudentMenu();
                        break;

                    case 3:
                        CourseMenu();
                        break;

                    case 4:
                        EnrollmentMenu();
                        break;

                    case 5:
                        AuditLogMenu();
                        break;

                    case 6:
                        Console.WriteLine("Application closed.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // DEPARTMENT MENU

    static void DepartmentMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Department Menu ---");
            Console.WriteLine("1. Add Department");
            Console.WriteLine("2. View All Departments");
            Console.WriteLine("3. Search Department");
            Console.WriteLine("4. Update Department");
            Console.WriteLine("5. Delete Department");
            Console.WriteLine("6. Back");

            int choice = ReadInt("Enter choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        Department department = new()
                        {
                            DepartmentName = ReadText("Department name: "),
                            Location = ReadOptionalText("Location: "),
                            IsActive = ReadBool("Is active (true/false): ")
                        };

                        departmentService.AddDepartment(department);
                        Console.WriteLine("Department added.");
                        break;

                    case 2:
                        var departments =
                            departmentService.GetAllDepartments();

                        foreach (Department d in departments)
                            PrintDepartment(d);

                        if (departments.Count == 0)
                            Console.WriteLine("No departments found.");

                        break;

                    case 3:
                        int departmentId = ReadInt("Department ID: ");
                        Department? foundDepartment =
                            departmentService.GetDepartmentById(departmentId);

                        if (foundDepartment == null)
                            Console.WriteLine("Department not found.");
                        else
                            PrintDepartment(foundDepartment);

                        break;

                    case 4:
                        int updateDepartmentId =
                            ReadInt("Department ID to update: ");

                        Department? existingDepartment =
                            departmentService.GetDepartmentById(
                                updateDepartmentId);

                        if (existingDepartment == null)
                        {
                            Console.WriteLine("Department not found.");
                            break;
                        }

                        existingDepartment.DepartmentName =
                            ReadText("New department name: ");
                        existingDepartment.Location =
                            ReadOptionalText("New location: ");
                        existingDepartment.IsActive =
                            ReadBool("Is active (true/false): ");

                        departmentService.UpdateDepartment(
                            existingDepartment);

                        Console.WriteLine("Department updated.");
                        break;

                    case 5:
                        departmentService.DeleteDepartment(
                            ReadInt("Department ID to delete: "));

                        Console.WriteLine("Delete command executed.");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintDepartment(Department d)
    {
        Console.WriteLine(
            $"{d.DepartmentId} | {d.DepartmentName} | " +
            $"{d.Location ?? "N/A"} | Active: {d.IsActive}");
    }

    // STUDENT MENU

    static void StudentMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Student Menu ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Back");

            int choice = ReadInt("Enter choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        Student student = new()
                        {
                            Name = ReadText("Student name: "),
                            Email = ReadText("Email: "),
                            Age = ReadInt("Age: "),
                            DepartmentId = ReadInt("Department ID: "),
                            AdmissionDate = ReadDate("Admission date"),
                            IsActive = ReadBool("Is active (true/false): ")
                        };

                        studentService.AddStudent(student);
                        Console.WriteLine("Student added.");
                        break;

                    case 2:
                        var students = studentService.GetAllStudents();

                        foreach (Student s in students)
                            PrintStudent(s);

                        if (students.Count == 0)
                            Console.WriteLine("No students found.");

                        break;

                    case 3:
                        int studentId = ReadInt("Student ID: ");
                        Student? foundStudent =
                            studentService.GetStudentById(studentId);

                        if (foundStudent == null)
                            Console.WriteLine("Student not found.");
                        else
                            PrintStudent(foundStudent);

                        break;

                    case 4:
                        int updateStudentId =
                            ReadInt("Student ID to update: ");

                        Student? existingStudent =
                            studentService.GetStudentById(updateStudentId);

                        if (existingStudent == null)
                        {
                            Console.WriteLine("Student not found.");
                            break;
                        }

                        existingStudent.Name = ReadText("New name: ");
                        existingStudent.Email = ReadText("New email: ");
                        existingStudent.Age = ReadInt("New age: ");
                        existingStudent.DepartmentId =
                            ReadInt("New department ID: ");
                        existingStudent.AdmissionDate =
                            ReadDate("Admission date");
                        existingStudent.IsActive =
                            ReadBool("Is active (true/false): ");

                        studentService.UpdateStudent(existingStudent);
                        Console.WriteLine("Student updated.");
                        break;

                    case 5:
                        studentService.DeleteStudent(
                            ReadInt("Student ID to delete: "));

                        Console.WriteLine("Delete command executed.");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintStudent(Student s)
    {
        Console.WriteLine(
            $"{s.StudentId} | {s.Name} | {s.Email} | Age: {s.Age} | " +
            $"Department: {s.DepartmentId} | " +
            $"Admission: {s.AdmissionDate:yyyy-MM-dd} | " +
            $"Active: {s.IsActive}");
    }

    // COURSE MENU

    static void CourseMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Course Menu ---");
            Console.WriteLine("1. Add Course");
            Console.WriteLine("2. View All Courses");
            Console.WriteLine("3. Search Course");
            Console.WriteLine("4. Update Course");
            Console.WriteLine("5. Delete Course");
            Console.WriteLine("6. Back");

            int choice = ReadInt("Enter choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        Course course = new()
                        {
                            CourseName = ReadText("Course name: "),
                            DurationMonths = ReadInt("Duration in months: "),
                            Fee = ReadDecimal("Course fee: "),
                            DepartmentId = ReadInt("Department ID: "),
                            IsActive = ReadBool("Is active (true/false): ")
                        };

                        courseService.AddCourse(course);
                        Console.WriteLine("Course added.");
                        break;

                    case 2:
                        var courses = courseService.GetAllCourses();

                        foreach (Course c in courses)
                            PrintCourse(c);

                        if (courses.Count == 0)
                            Console.WriteLine("No courses found.");

                        break;

                    case 3:
                        int courseId = ReadInt("Course ID: ");
                        Course? foundCourse =
                            courseService.GetCourseById(courseId);

                        if (foundCourse == null)
                            Console.WriteLine("Course not found.");
                        else
                            PrintCourse(foundCourse);

                        break;

                    case 4:
                        int updateCourseId =
                            ReadInt("Course ID to update: ");

                        Course? existingCourse =
                            courseService.GetCourseById(updateCourseId);

                        if (existingCourse == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }

                        existingCourse.CourseName =
                            ReadText("New course name: ");
                        existingCourse.DurationMonths =
                            ReadInt("New duration in months: ");
                        existingCourse.Fee =
                            ReadDecimal("New course fee: ");
                        existingCourse.DepartmentId =
                            ReadInt("New department ID: ");
                        existingCourse.IsActive =
                            ReadBool("Is active (true/false): ");

                        courseService.UpdateCourse(existingCourse);
                        Console.WriteLine("Course updated.");
                        break;

                    case 5:
                        courseService.DeleteCourse(
                            ReadInt("Course ID to delete: "));

                        Console.WriteLine("Delete command executed.");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintCourse(Course c)
    {
        Console.WriteLine(
            $"{c.CourseId} | {c.CourseName} | " +
            $"Duration: {c.DurationMonths} months | " +
            $"Fee: {c.Fee:F2} | Department: {c.DepartmentId} | " +
            $"Active: {c.IsActive}");
    }

    // ENROLLMENT MENU

    static void EnrollmentMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Enrollment Menu ---");
            Console.WriteLine("1. Add Enrollment");
            Console.WriteLine("2. View All Enrollments");
            Console.WriteLine("3. Search Enrollment");
            Console.WriteLine("4. Update Enrollment");
            Console.WriteLine("5. Delete Enrollment");
            Console.WriteLine("6. Back");

            int choice = ReadInt("Enter choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        Enrollment enrollment = new()
                        {
                            StudentId = ReadInt("Student ID: "),
                            CourseId = ReadInt("Course ID: "),
                            EnrolledAt = DateTime.Now,
                            Status = ReadText(
                                "Status (Enrolled/Completed/Cancelled): ")
                        };

                        enrollmentService.AddEnrollment(enrollment);
                        Console.WriteLine("Enrollment added.");
                        break;

                    case 2:
                        var enrollments =
                            enrollmentService.GetAllEnrollments();

                        foreach (Enrollment e in enrollments)
                            PrintEnrollment(e);

                        if (enrollments.Count == 0)
                            Console.WriteLine("No enrollments found.");

                        break;

                    case 3:
                        int enrollmentId = ReadInt("Enrollment ID: ");
                        Enrollment? foundEnrollment =
                            enrollmentService.GetEnrollmentById(enrollmentId);

                        if (foundEnrollment == null)
                            Console.WriteLine("Enrollment not found.");
                        else
                            PrintEnrollment(foundEnrollment);

                        break;

                    case 4:
                        int updateEnrollmentId =
                            ReadInt("Enrollment ID to update: ");

                        Enrollment? existingEnrollment =
                            enrollmentService.GetEnrollmentById(
                                updateEnrollmentId);

                        if (existingEnrollment == null)
                        {
                            Console.WriteLine("Enrollment not found.");
                            break;
                        }

                        existingEnrollment.StudentId =
                            ReadInt("New student ID: ");
                        existingEnrollment.CourseId =
                            ReadInt("New course ID: ");
                        existingEnrollment.Status =
                            ReadText("New status: ");

                        enrollmentService.UpdateEnrollment(
                            existingEnrollment);

                        Console.WriteLine("Enrollment updated.");
                        break;

                    case 5:
                        enrollmentService.DeleteEnrollment(
                            ReadInt("Enrollment ID to delete: "));

                        Console.WriteLine("Delete command executed.");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintEnrollment(Enrollment e)
    {
        Console.WriteLine(
            $"{e.EnrollmentId} | Student: {e.StudentId} | " +
            $"Course: {e.CourseId} | " +
            $"Enrolled: {e.EnrolledAt:yyyy-MM-dd HH:mm} | " +
            $"Status: {e.Status}");
    }

    // AUDIT LOG MENU

    static void AuditLogMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Audit Log Menu ---");
            Console.WriteLine("1. Add Audit Log");
            Console.WriteLine("2. View All Audit Logs");
            Console.WriteLine("3. Search Audit Log");
            Console.WriteLine("4. Back");

            int choice = ReadInt("Enter choice: ");

            try
            {
                switch (choice)
                {
                    case 1:
                        AuditLog auditLog = new()
                        {
                            EntityName = ReadText("Entity name: "),
                            EntityId = ReadInt("Entity ID: "),
                            Action = ReadText("Action: "),
                            Details = ReadOptionalText("Details: "),
                            CreatedAt = DateTime.Now
                        };

                        auditLogService.AddAuditLog(auditLog);
                        Console.WriteLine("Audit log added.");
                        break;

                    case 2:
                        var logs = auditLogService.GetAllAuditLogs();

                        foreach (AuditLog log in logs)
                            PrintAuditLog(log);

                        if (logs.Count == 0)
                            Console.WriteLine("No audit logs found.");

                        break;

                    case 3:
                        long auditId = ReadLong("Audit ID: ");
                        AuditLog? foundLog =
                            auditLogService.GetAuditLogById(auditId);

                        if (foundLog == null)
                            Console.WriteLine("Audit log not found.");
                        else
                            PrintAuditLog(foundLog);

                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintAuditLog(AuditLog log)
    {
        Console.WriteLine(
            $"{log.AuditId} | {log.EntityName} | " +
            $"Entity ID: {log.EntityId} | Action: {log.Action} | " +
            $"Details: {log.Details ?? "N/A"} | " +
            $"Created: {log.CreatedAt:yyyy-MM-dd HH:mm}");
    }

    // INPUT HELPERS

    static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Please enter a valid integer.");
        }
    }

    static long ReadLong(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (long.TryParse(Console.ReadLine(), out long value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (decimal.TryParse(Console.ReadLine(), out decimal value))
                return value;

            Console.WriteLine("Please enter a valid amount.");
        }
    }

    static string ReadText(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
                return value.Trim();

            Console.WriteLine("This field cannot be empty.");
        }
    }

    static string? ReadOptionalText(string message)
    {
        Console.Write(message);
        string? value = Console.ReadLine();

        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (bool.TryParse(Console.ReadLine(), out bool value))
                return value;

            Console.WriteLine("Enter true or false.");
        }
    }

    static DateTime ReadDate(string message)
    {
        while (true)
        {
            Console.Write($"{message} (yyyy-MM-dd): ");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                return date;

            Console.WriteLine("Enter a valid date.");
        }
    }
}
