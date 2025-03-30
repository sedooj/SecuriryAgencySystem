using Core.Model.Users;

namespace Core.Service.Interface;

public interface IClientService
{
    void CreateClient(CorporateClient corporateClient);
    void CreateClient(IndividualClient individualClient);
    void UpdateClient(CorporateClient corporateClient);
    void UpdateClient(IndividualClient individualClient);
}