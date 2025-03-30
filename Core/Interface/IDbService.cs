namespace Core.Interface;

public interface IDbService<T> where T : class
{
    List<T> LoadEntities();
    T? LoadEntity(Guid id);
    void SaveEntity(T entity);
    void UpdateEntity(Guid id, T updatedEntity);
    void DeleteEntity(Guid id);
}