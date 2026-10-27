
using StudentCourseManagement.Models;
using StudentCourseManagement.Repository;

namespace StudentCourseManagement.Services;

public class AuditLogService
{
    private readonly AuditLogRepository _repository;

    public AuditLogService()
    {
        _repository = new AuditLogRepository();
    }

    public void AddAuditLog(AuditLog auditLog)
    {
        if (string.IsNullOrWhiteSpace(auditLog.EntityName))
            throw new ArgumentException("Entity name is required.");

        if (auditLog.EntityId <= 0)
            throw new ArgumentException("Invalid entity ID.");

        if (string.IsNullOrWhiteSpace(auditLog.Action))
            throw new ArgumentException("Action is required.");

        if (auditLog.CreatedAt == default)
            auditLog.CreatedAt = DateTime.Now;

        _repository.Add(auditLog);
    }

    public List<AuditLog> GetAllAuditLogs()
    {
        return _repository.GetAll();
    }

    public AuditLog? GetAuditLogById(long id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid audit ID.");

        return _repository.GetById(id);
    }
}
