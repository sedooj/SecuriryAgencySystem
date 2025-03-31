using Core.Model.Users;

namespace Core.Impl;

public class EmployeesDbService : DbService<Employee>
{
    public override Guid GetEntityId(Employee entity)
    {
        var property = typeof(Employee).GetProperty("EmployeeId");
        if (property != null && property.PropertyType == typeof(Guid))
        {
            return (Guid)(property.GetValue(entity) ?? Guid.Empty);
        }
        throw new InvalidOperationException("Entity does not have a Guid EmployeeId property.");
    }
}