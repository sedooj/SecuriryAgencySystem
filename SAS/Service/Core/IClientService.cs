using SAS.Model;

namespace SAS.Service.Core;

public interface IClientService
{
    void CreateClient(CorporateClient corporateClient);
    void CreateClient(IndividualClient individualClient);
    void UpdateClient(CorporateClient corporateClient);
    void UpdateClient(IndividualClient individualClient);
}