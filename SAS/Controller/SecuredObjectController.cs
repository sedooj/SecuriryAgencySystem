using System.Collections.ObjectModel;
using Core.Impl;
using Core.Interface;
using Core.Model;

namespace SAS.Controller;

public class SecuredObjectController : ITableController
{
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();

    public SecuredObjectController()
    {
        UpdateTable();
    }

    public ObservableCollection<SecuredObject> SecuredObjects { get; set; } = [];

    public void UpdateTable()
    {
        var securedObjects = GetSecuredObjects();
        SecuredObjects.Clear();
        foreach (var securedObject in securedObjects)
        {
            SecuredObjects.Add(securedObject);
        }
    }

    public ObservableCollection<SecuredObject> GetSecuredObjects()
    {
        return new ObservableCollection<SecuredObject>(_securedObjectDbService.LoadEntities());
    }

    private void AddRecord(SecuredObject securedObject)
    {
        SecuredObjects.Add(securedObject);
    }

    private void RemoveRecord(SecuredObject securedObject)
    {
        SecuredObjects.Remove(securedObject);
    }

    private void UpdateRecord(SecuredObject securedObject)
    {
        var index = SecuredObjects.IndexOf(securedObject);
        if (index == -1) return;
        SecuredObjects[index] = securedObject;
    }

    public void AddSecuredObject(SecuredObject securedObject)
    {
        _securedObjectDbService.SaveEntity(securedObject);
        AddRecord(securedObject);
    }

    public void RemoveSecuredObject(SecuredObject securedObject)
    {
        _securedObjectDbService.DeleteEntity(securedObject.Id);
        RemoveRecord(securedObject);
    }

    public void UpdateSecuredObject(SecuredObject securedObject)
    {
        _securedObjectDbService.UpdateEntity(securedObject.Id, securedObject);
        UpdateRecord(securedObject);
    }
}