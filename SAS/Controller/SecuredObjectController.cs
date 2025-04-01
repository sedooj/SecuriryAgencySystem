using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;

namespace SAS.Controller;

public class SecuredObjectController
{
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();
    private readonly IDbService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    private readonly IDbService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();

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
        var individualClients = GetIndividualClients();
        var corporateClients = GetCorporateClients();

        if (individualClients.Count < 2) throw new IndexOutOfRangeException("Insufficient individual clients to create secured objects");
        if (corporateClients.Count < 2) throw new IndexOutOfRangeException("Insufficient corporate clients to create secured objects");

        List<SecuredObject> newObjects = new()
        {
            new SecuredObject(Guid.NewGuid(), "Театр", "ул. Ленина 23", 378.9, SecurityLevel.Hard, individualClients[0].Id, OwnerType.Individual),
            new SecuredObject(Guid.NewGuid(), "Рынок", "ул. Титова 7", 678.2, SecurityLevel.High, corporateClients[0].Id, OwnerType.Corp),
            new SecuredObject(Guid.NewGuid(), "Школа №31", "ул. Ольги Жилиной 31", 65.3, SecurityLevel.Low, individualClients[1].Id, OwnerType.Individual),
            new SecuredObject(Guid.NewGuid(), "Супермаркет Шестёрочка", "ул. Большая 7", 24.8, SecurityLevel.Low, corporateClients[1].Id, OwnerType.Corp),
        };

        foreach (var obj in newObjects)
        {
            _securedObjectDbService.SaveEntity(obj);
        }
    }

    private List<IndividualClient> GetIndividualClients()
    {
        return _individualClientDbService.LoadEntities();
    }

    private List<CorporateClient> GetCorporateClients()
    {
        return _corporateClientDbService.LoadEntities();
    }

    public List<SecuredObject> GetObjects()
    {
        return _securedObjectDbService.LoadEntities();
    }
}