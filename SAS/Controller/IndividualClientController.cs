using System.Collections.ObjectModel;
using Core.Model.Users;
using Core.Service.Impl;

namespace SAS.Controller;

public class IndividualClientController : ITableController
{
    private readonly ClientService _clientService = new();
    public ObservableCollection<IndividualClient> IndividualClients { get; private set; } = new();

    public IndividualClientController()
    {
        UpdateTable();
    }

    public void AddClient(IndividualClient client)
    {
        _clientService.CreateClient(client);
        IndividualClients.Add(client);
    }

    public void UpdateClient(IndividualClient client)
    {
        _clientService.UpdateClient(client);
    }

    public void RemoveClient(IndividualClient client)
    {
        _clientService.UpdateClient(client);
        IndividualClients.Remove(client);
    }
    
    private ObservableCollection<IndividualClient> GetIndividualClients()
    {
        return new ObservableCollection<IndividualClient>(_clientService.LoadIndividualClients());
    }

    public void UpdateTable()
    {
        var clients = GetIndividualClients();
        IndividualClients.Clear();
        foreach (var client in clients)
        {
            IndividualClients.Add(client);
        }
    }
}
