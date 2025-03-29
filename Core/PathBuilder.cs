using Core.Model;

namespace Core;

public class PathBuilder
{
    private readonly string _documentsDirectory;

    public PathBuilder()
    {
        _documentsDirectory = GetDocumentsDirectory();
    }

    public string GetTablePath(Type type)
    {
        return Path.Combine(_documentsDirectory, FindTableForType(type) + ".json");
    }

    private string GetDocumentsDirectory()
    {
        var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dbDir = Path.Combine(directoryPath, "SecurityAgencySystem");
        return dbDir;
    }

    private string FindTableForType(Type type)
    {
        var typeDir = type switch
        {
            not null when type == typeof(Contract) => "contracts",
            not null when type == typeof(CorporateClient) => "corporate_clients",
            not null when type == typeof(IndividualClient) => "individual_clients",
            not null when type == typeof(Employee) => "employees",
            not null when type == typeof(Person) => "persons",
            not null when type == typeof(Weapon) => "weapons",
            _ => throw new NullReferenceException($"Can't find table for type: {type}")
        };
        return typeDir;
    }
}