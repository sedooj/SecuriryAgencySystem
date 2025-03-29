using Core.Impl;
using Core.Interface;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class ClientService : IClientService
{
    private readonly IDBService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();
    private readonly IDBService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    
    public void CreateClient(CorporateClient corporateClient)
    {
        _corporateClientDbService.SaveEntity(corporateClient);
    }

    public void CreateClient(IndividualClient individualClient)
    {
        _individualClientDbService.SaveEntity(individualClient);
    }

    public void UpdateClient(CorporateClient corporateClient)
    {
        _corporateClientDbService.UpdateEntity(corporateClient.Id, corporateClient);
    }

    public void UpdateClient(IndividualClient individualClient)
    {
        _individualClientDbService.UpdateEntity(individualClient.Id, individualClient);
    }
}