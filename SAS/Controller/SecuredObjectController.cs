using Core.Impl;
using Core.Interface;
using Core.Model;

namespace SAS.Controller;

public class SecuredObjectController
{
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();

    public void Setup()
    {
        var objects = GetObjects();
        if (objects.Count == 0)
        {
            CreateBaseObjects();
        }
    }
    
    private void CreateBaseObjects()
    {
        List<SecuredObject> newObjects = [
            new (Guid.NewGuid(),"Театр", "ул. Ленина 23", 378.9, SecurityLevel.Hard),
            new (Guid.NewGuid(),"Рынок", "ул. Титова 7", 678.2, SecurityLevel.High),
            new (Guid.NewGuid(),"Школа №31", "ул. Ольги Жилиной 31", 65.3, SecurityLevel.Low),
            new (Guid.NewGuid(),"Супермаркет Шестёрочка", "ул. Большая 7", 24.8, SecurityLevel.Low),
        ];
        foreach (var obj in newObjects)
        {
            _securedObjectDbService.SaveEntity(obj);
        }
    }

    public List<SecuredObject> GetObjects()
    {
        return _securedObjectDbService.LoadEntities();
    }
}