using System.Diagnostics;
using Core.Interface;

namespace Core.Impl;

public class JsonDbService<T> : IDBService<T> where T : class
{
    private readonly JsonObjectSerializer _serializer = new();
    private readonly string _dbDir = new PathBuilder().GetTablePath(typeof(T));
    
    public List<T> LoadEntities()
    {
        if (!File.Exists(_dbDir)) return new List<T>();
        var jsonString = File.ReadAllText(_dbDir);
        var entities = _serializer.Deserialize<List<T>>(jsonString);
        return entities ?? [];
    }

    public T? LoadEntity(Guid id)
    {
        var entities = LoadEntities();
        return entities.FirstOrDefault(e => GetEntityId(e) == id);
    }

    public void SaveEntity(T entity)
    {
        var entities = LoadEntities().ToList();
        entities.Add(entity);
        SaveEntitiesToFile(entities);
    }

    public void UpdateEntity(Guid id, T updatedEntity)
    {
        var entities = LoadEntities().ToList();
        var index = entities.FindIndex(e => GetEntityId(e) == id);
        if (index != -1)
        {
            entities[index] = updatedEntity;
        }
        else
        {
            entities.Add(updatedEntity);
        }
        SaveEntitiesToFile(entities);
    }

    private void SaveEntitiesToFile(List<T> entities)
    {
        var jsonString = _serializer.Serialize(entities);
        File.WriteAllText(_dbDir, jsonString);
        Debug.WriteLine($"Entities saved successfully to {_dbDir}.");
    }

    private Guid GetEntityId(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        if (property != null && property.PropertyType == typeof(Guid))
        {
            return (Guid)(property.GetValue(entity) ?? Guid.Empty);
        }
        throw new InvalidOperationException("Entity does not have a Guid Id property.");
    }
}