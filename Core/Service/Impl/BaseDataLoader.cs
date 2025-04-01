using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;

namespace Core.Service.Impl;

public class BaseDataLoader
{
    private readonly IDbService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();
    public bool IsNeedToLoadBaseData { get; private set; }

    public void ProcessLoadBaseData()
    {
        if (CheckIsNeedToLoadBaseData()) LoadBaseData();
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

        foreach (var client in corporateClients) _corporateClientDbService.SaveEntity(client);

        foreach (var client in individualClients) _individualClientDbService.SaveEntity(client);

        foreach (var employee in employees) _employeeDbService.SaveEntity(employee);

        foreach (var securedObject in securedObjects) _securedObjectDbService.SaveEntity(securedObject);

        IsNeedToLoadBaseData = false;
    }

    private List<CorporateClient> LoadBaseCorporateClients()
    {
        return new List<CorporateClient>
        {
            new(Guid.NewGuid(), "АО Арт-пространство Цоколь", null),
            new(Guid.NewGuid(), "ООО Шестерочка", null),
            new(Guid.NewGuid(), "ПАО Сбебранк", null)
        };
    }

    private List<IndividualClient> LoadBaseIndividualClients()
    {
        return new List<IndividualClient>
        {
            new(Guid.NewGuid(),
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Шмелев", "Василий", "Владимирович", "Мужской",
                    "Казахстан"), null),
            new(Guid.NewGuid(),
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Сио", "Ши", "Мей", "Женский", "Китай"),
                null),
            new(Guid.NewGuid(),
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Руков", "Дмитрий", "Васильевич", "Мужской",
                    "Российская Федерация"), null)
        };
    }

    private List<Employee> LoadBaseEmployees()
    {
        var emps = new List<Employee>
        {
            new(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Суворов", "Игорь", "Николаевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Старший менеджер", Role.Manager), new Documents("Улица Суворовская 24", "1234567890"),
                null, null, null, null, null),
            new(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Антошин", "Александр", "Алексеевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Уборщик", Role.Cleaner), new Documents("Улица Александров 24", "2345678901"), null, null,
                null, null, null),
            new(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Шишкин", "Павел", "Сергеевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer), new Documents("Улица Павлов 1", "3456789012"), null,
                null, null, null, null),
            new(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Маков", "Григорий", "Анатольевич", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer), new Documents("Улица Григориев 33", "4567890123"), null,
                null
                , null, null, null),
            new(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now, "Жигхенбев", "Макулбек", "Макулбекович", "Мужской",
                    "Иностранный специалист"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer),
                new Documents("Улица Расколотых Чурок 201", "5678901234"), null, null, null, null, null)
        };
        for (var i = 0; i < 50; i++)
            emps.Add(new Employee(
                new Passport($"{new Random().Next(5000, 5025)}", $"{new Random().Next(100000, 999999)}", DateTime.Now,
                    "Охранов", $"Охранник-{i + 1}-й", "Охранникович", "Мужской",
                    "Российская Федерация"), Guid.NewGuid(), Guid.NewGuid(),
                new JobRole("Охранник", Role.SecurityOfficer), new Documents($"Улица Охранная {i + 1}", "1234567890"),
                null, null,
                null, null, null));

        return emps;
    }

    private List<SecuredObject> LoadBaseSecuredObjects(List<IndividualClient> individualClients,
        List<CorporateClient> corporateClients)
    {
        return new List<SecuredObject>
        {
            new(Guid.NewGuid(), "Концертная площадка Цоколь", "Улица Цокольная 777", 70.2,
                SecurityLevel.Medium, corporateClients[0].Id, OwnerType.Corp),
            new(Guid.NewGuid(), "Супермаркет Шестёрочка", "Улица Шестёрочная 666", 105.1,
                SecurityLevel.Low, corporateClients[1].Id, OwnerType.Corp),
            new(Guid.NewGuid(), "Филиал Сбебранк", "Улица Сбебра 14", 45.1, SecurityLevel.Medium,
                corporateClients[2].Id, OwnerType.Corp),
            new(Guid.NewGuid(), "Магазин одежды и обуви", "Улица Шмоток 1", 34.2, SecurityLevel.Low,
                individualClients[0].Id, OwnerType.Individual),
            new(Guid.NewGuid(), "Ларёк китайского хлама", "Улица Хлама 12", 14.2, SecurityLevel.Low,
                individualClients[1].Id, OwnerType.Individual),
            new(Guid.NewGuid(), "Рынок", "Улица Рыночная 9", 85.2, SecurityLevel.Medium,
                individualClients[2].Id, OwnerType.Individual)
        };
    }
}