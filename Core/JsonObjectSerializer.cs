using System.Text.Json;

namespace Core;

public class JsonObjectSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true
    };

    public T? Deserialize<T>(string data)
    {
        return JsonSerializer.Deserialize<T>(data, Options);
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}