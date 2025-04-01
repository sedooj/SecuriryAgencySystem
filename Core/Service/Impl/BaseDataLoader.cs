using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;

namespace Core.Service.Impl;

public class BaseDataLoader
{
    private readonly IDbService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();
    private readonly IDbService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();
    public bool IsNeedToLoadBaseData { get; private set; } = false;

    public void ProcessLoadBaseData()
    {
        if (CheckIsNeedToLoadBaseData())
        {
            LoadBaseData();
        }
    }

    private bool CheckIsNeedToLoadBaseData()
    {
        var corporateClients = _corporateClientDbService.LoadEntities();
        var individualClients = _individualClientDbService.LoadEntities();
        var employees = _employeeDbService.LoadEntities();
        var securedObjects = _securedObjectDbService.LoadEntities();
        return corporateClients.Count == 0 || individualClients.Count == 0 || employees.Count == 0 ||
               securedObjects.Count == 0;
    }

    private void LoadBaseData()
    {
        var corporateClients = LoadBaseCorporateClients();
        var individualClients = LoadBaseIndividualClients();
        var employees = LoadBaseEmployees();
        var securedObjects = LoadBaseSecuredObjects(individualClients, corporateClients);

        foreach (var client in corporateClients)
        {
            _corporateClientDbService.SaveEntity(client);
        }

        foreach (var client in individualClients)
        {
            _individualClientDbService.SaveEntity(client);
        }

        foreach (var employee in employees)
        {
            _employeeDbService.SaveEntity(employee);
        }

        foreach (var securedObject in securedObjects)
        {
            _securedObjectDbService.SaveEntity(securedObject);
        }

        IsNeedToLoadBaseData = false;
    }

    private List<CorporateClient> LoadBaseCorporateClients()
    {
        return new List<CorporateClient>
        {
            new CorporateClient(Guid.NewGuid(), "АО Арт-пространство Цоколь", null),
            new CorporateClient(Guid.NewGuid(), "ООО Шестерочка", null),
            new CorporateClient(Guid.NewGuid(), "ПАО Сбебранк", null),
        };
    }

    private List<IndividualClient> LoadBaseIndividualClients()
    {
        return new List<IndividualClient>
        {
            new IndividualClient(Guid.NewGuid(),
                new Passport("192394", "5011", DateTime.Now, "Шмелев", "Василий", "Владимирович", "Мужской",
                    "Казахстан"), null),
            new IndividualClient(Guid.NewGuid(),
                new Passport("192394", "5001", DateTime.Now, "Сио", "Ши", "Мей", "Женский", "Китай"),
                null),
            new IndividualClient(Guid.NewGuid(),
                new Passport("192394", "5001", DateTime.Now, "Руков", "Дмитрий", "Васильевич", "Мужской",
                    "Российская Федерация"), null),
        };
    }

    private List<Employee> LoadBaseEmployees()
    {
        return new List<Employee>
        {
            new Employee(
                new Passport("1238151123", "5021", DateTime.Now, "Суворов", "Игорь", "Николаевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Старший менеджер", Role.Manager), new Documents("Улица Суворовская 24", "192355350912"),
                null, null, null, null, null),
            new Employee(
                new Passport("1238151123", "5021", DateTime.Now, "Антошин", "Александр", "Алексеевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Уборщик", Role.Cleaner), new Documents("Улица Александров 24", "95334850912"), null, null,
                null, null, null),
            new Employee(
                new Passport("1238151123", "5021", DateTime.Now, "Шишкин", "Павел", "Сергеевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer), new Documents("Улица Павлов 1", "711234850912"), null,
                null, null, null, null),
            new Employee(
                new Passport("1238151123", "5021", DateTime.Now, "Маков", "Григорий", "Анатольевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer), new Documents("Улица Григориев 33", "78234850912"), null,
                null
                , null, null, null),
            new Employee(
                new Passport("1238151123", "5021", DateTime.Now, "Жигхенбев", "Макулбек", "Макулбекович", "Мужской",
                    "Иностранный специалист"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer),
                new Documents("Улица Расколотых Чурок 201", "21234850912"), null, null, null, null, null),
        };
    }

    private List<SecuredObject> LoadBaseSecuredObjects(List<IndividualClient> individualClients,
        List<CorporateClient> corporateClients)
    {
        return new List<SecuredObject>
        {
            new SecuredObject(Guid.NewGuid(), "Концертная площадка Цоколь", "Улица Цокольная 777", 70.2,
                SecurityLevel.Medium, corporateClients[0].Id, OwnerType.Corp),
            new SecuredObject(Guid.NewGuid(), "Супермаркет Шестёрочка", "Улица Шестёрочная 666", 105.1,
                SecurityLevel.Low, corporateClients[1].Id, OwnerType.Corp),
            new SecuredObject(Guid.NewGuid(), "Филиал Сбебранк", "Улица Сбебра 14", 45.1, SecurityLevel.Medium,
                corporateClients[2].Id, OwnerType.Corp),
            new SecuredObject(Guid.NewGuid(), "Магазин одежды и обуви", "Улица Шмоток 1", 34.2, SecurityLevel.Low,
                individualClients[0].Id, OwnerType.Individual),
            new SecuredObject(Guid.NewGuid(), "Ларёк китайского хлама", "Улица Хлама 12", 14.2, SecurityLevel.Low,
                individualClients[1].Id, OwnerType.Individual),
            new SecuredObject(Guid.NewGuid(), "Рынок", "Улица Рыночная 9", 85.2, SecurityLevel.Medium,
                individualClients[2].Id, OwnerType.Individual),
        };
    }
}