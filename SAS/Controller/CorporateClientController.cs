using System.Collections.ObjectModel;
using Core.Model.Users;
using Core.Service.Impl;

namespace SAS.Controller;

public class CorporateClientController
{
    private readonly ClientService _clientService = new();

    public CorporateClientController()
    {
        UpdateTable();
    }

    public ObservableCollection<CorporateClient> CorporateClients { get; } = new();

    public void AddClient(CorporateClient client)
    {
        _clientService.CreateClient(client);
        CorporateClients.Add(client);
    }

    public void UpdateClient(CorporateClient client)
    {
        _clientService.UpdateClient(client);
    }

    public void RemoveClient(CorporateClient client)
    {
        _clientService.UpdateClient(client);
        CorporateClients.Remove(client);
    }

    private ObservableCollection<CorporateClient> GetCorporateClients()
    {
        return new ObservableCollection<CorporateClient>(_clientService.LoadCorporateClients());
    }

    public void UpdateTable()
    {
        var clients = GetCorporateClients();
        CorporateClients.Clear();
        foreach (var client in clients) CorporateClients.Add(client);
    }
}