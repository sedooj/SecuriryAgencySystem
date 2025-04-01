using System.Diagnostics;
using System.IO;
using Core.Exception;
using Core.Interface;

namespace Core.Impl;

public class JsonDbService<T> : IDbService<T> where T : class
{
    private readonly string _dbDir = new PathBuilder().GetTablePath(typeof(T));
    private readonly JsonObjectSerializer _serializer = new();

    public JsonDbService()
    {
        var directory = Path.GetDirectoryName(_dbDir);
        if (directory == null) throw new UnsupportedDirectory();
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    public List<T> LoadEntities()
    {
        try
        {
            if (!File.Exists(_dbDir)) return new List<T>();
            var jsonString = File.ReadAllText(_dbDir);
            var entities = _serializer.Deserialize<List<T>>(jsonString);
            return entities ?? new List<T>();
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine($"Error loading entities: {ex.Message}");
            return new List<T>();
        }
    }

    public T? LoadEntity(Guid id)
    {
        var entities = LoadEntities();
        return entities.FirstOrDefault(e => GetEntityId(e) == id);
    }

    public void SaveEntity(T entity)
    {
        try
        {
            var entities = LoadEntities();
            entities.Add(entity);
            SaveEntitiesToFile(entities);
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine($"Error saving entity: {ex.Message}");
        }
    }

    public void UpdateEntity(Guid id, T updatedEntity)
    {
        try
        {
            var entities = LoadEntities();
            var index = entities.FindIndex(e => GetEntityId(e) == id);
            if (index != -1)
                entities[index] = updatedEntity;
            else
                entities.Add(updatedEntity);
            SaveEntitiesToFile(entities);
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine($"Error updating entity: {ex.Message}");
        }
    }

    public void DeleteEntity(Guid id)
    {
        try
        {
            var entities = LoadEntities();
            var entityToRemove = entities.FirstOrDefault(e => GetEntityId(e) == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                SaveEntitiesToFile(entities);
            }
            else
            {
                throw new InvalidOperationException($"Entity with id {id} not found.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine($"Error deleting entity: {ex.Message}");
        }
    }

    private void SaveEntitiesToFile(List<T> entities)
    {
        try
        {
            var jsonString = _serializer.Serialize(entities);
            File.WriteAllText(_dbDir, jsonString);
            Debug.WriteLine($"Entities saved successfully to {_dbDir}.");
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine($"Error saving entities to file: {ex.Message}");
        }
    }

    private Guid GetEntityId(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        if (property != null && property.PropertyType == typeof(Guid))
            return (Guid)(property.GetValue(entity) ?? Guid.Empty);
        throw new InvalidOperationException("Entity does not have a Guid Id property.");
    }
}