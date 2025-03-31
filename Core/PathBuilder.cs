using Core.Model;
using Core.Model.Users;

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
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SecurityAgencySystem/";
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
            not null when type == typeof(SecuredObject) => "secured_objects",
            _ => throw new NullReferenceException($"Can't find table for type: {type}")
        };
        return typeDir;
    }
}