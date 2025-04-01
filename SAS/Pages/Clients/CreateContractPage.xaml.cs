using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Pages.Clients;

public partial class CreateContractPage : ContentPage
{
    private readonly IContractService _contractService = new ContractService();
    private readonly CorporateClient? _corporateClient;
    private readonly IndividualClient? _individualClient;
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();

    public CreateContractPage(IndividualClient client)
    {
        InitializeComponent();
        _individualClient = client;

        LoadSecuredObjectsIndividualClients(client);
        SetObjectToSecurePickerItemsSource();
        ObjectToSecurePicker.SelectedIndexChanged += OnObjectToSecurePickerSelectedIndexChanged;
    }

    public CreateContractPage(CorporateClient client)
    {
        InitializeComponent();
        _corporateClient = client;

        LoadSecuredObjectsCorporateClients(client);
        SetObjectToSecurePickerItemsSource();
        ObjectToSecurePicker.SelectedIndexChanged += OnObjectToSecurePickerSelectedIndexChanged;
    }

    public event EventHandler<Contract>? ContractConcluded;

    private void SetObjectToSecurePickerItemsSource()
    {
        if (_individualClient != null)
            SetItemsSourceForIndividualClient(_individualClient.Id);
        else if (_corporateClient != null) SetItemsSourceForCorporateClient(_corporateClient.Id);
    }

    private void SetItemsSourceForIndividualClient(Guid ownerId)
    {
        try
        {
            var securedObjects = _securedObjectDbService.LoadEntities()
                .Where(obj => obj.OwnerId == ownerId && obj.OwnerType == OwnerType.Individual)
                .ToList();

            ObjectToSecurePicker.ItemsSource = securedObjects;
            ObjectToSecurePicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private void SetItemsSourceForCorporateClient(Guid ownerId)
    {
        try
        {
            var securedObjects = _securedObjectDbService.LoadEntities()
                .Where(obj => obj.OwnerId == ownerId && obj.OwnerType == OwnerType.Corp)
                .ToList();

            ObjectToSecurePicker.ItemsSource = securedObjects;
            ObjectToSecurePicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private void LoadSecuredObjectsIndividualClients(IndividualClient individualClient)
    {
        try
        {
            var securedObjects = _securedObjectDbService.LoadEntities()
                .Where(obj => obj.OwnerId == individualClient.Id && obj.OwnerType == OwnerType.Individual)
                .ToList();

            ObjectToSecurePicker.ItemsSource = securedObjects;
            ObjectToSecurePicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private void LoadSecuredObjectsCorporateClients(CorporateClient corporateClient)
    {
        try
        {
            var securedObjects = _securedObjectDbService.LoadEntities()
                .Where(obj => obj.OwnerId == corporateClient.Id && obj.OwnerType == OwnerType.Corp)
                .ToList();

            ObjectToSecurePicker.ItemsSource = securedObjects;
            ObjectToSecurePicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private async void OnSaveContractButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var selectedObject = (SecuredObject)ObjectToSecurePicker.SelectedItem;
            var startDateTime = StartDatePicker.Date.Add(StartTimePicker.Time);
            var endDateTime = EndDatePicker.Date.Add(EndTimePicker.Time);
            var schedule = new Schedule(startDateTime, endDateTime);
            var contractAmount = CalculateContractAmount(selectedObject);
            if (_corporateClient != null)
            {
                var contract = new Contract(
                    Guid.NewGuid(),
                    new List<Guid>(),
                    selectedObject.Id,
                    schedule,
                    null,
                    _corporateClient.Id,
                    contractAmount
                );
                ContractConcluded?.Invoke(this, contract);
            }
            else if (_individualClient != null)
            {
                var contract = new Contract(
                    Guid.NewGuid(),
                    new List<Guid>(),
                    selectedObject.Id,
                    schedule,
                    null,
                    _individualClient.Id,
                    contractAmount
                );
                ContractConcluded?.Invoke(this, contract);
            }
            else
            {
                throw new InvalidOperationException("Neither IndividualClient nor CorporateClient is set.");
            }

            await Navigation.PopAsync();
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync();
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private void OnObjectToSecurePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var selectedObject = (SecuredObject)ObjectToSecurePicker.SelectedItem;
            GuardiansCountLabel.Text = $"Необходимое количество охранников: {selectedObject.GuardiansCount}";
            ContractAmountLabel.Text = $"Сумма договора: {CalculateContractAmount(selectedObject)} ₽";
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }

    private decimal CalculateContractAmount(SecuredObject securedObject)
    {
        try
        {
            return _contractService.CalculateContractAmount(securedObject);
        }
        catch (InvalidOperationException ex)
        {
            DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
            return 0;
        }
        catch (ArgumentNullException ex)
        {
            DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
            return 0;
        }
    }
}