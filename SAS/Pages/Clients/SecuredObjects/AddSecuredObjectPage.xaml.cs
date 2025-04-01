using System;
using Core.Model;
using SAS.Controller;
using Core.Service.Impl;

namespace SAS.Pages.Clients.SecuredObjects;

public partial class AddSecuredObjectPage : ContentPage
{
    private readonly SecuredObjectController _controller = new ();
    private readonly ReportService _reportService = new();

    public AddSecuredObjectPage()
    {
        InitializeComponent();
        LoadClients();
    }

    private void LoadClients()
    {
        var clients = _reportService.LoadClients();
        ClientPicker.ItemsSource = clients;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(AddressEntry.Text) ||
            string.IsNullOrWhiteSpace(AreaEntry.Text) || SecurityLevelPicker.SelectedItem.ToString() == null || ClientPicker.SelectedItem == null)
        {
            await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
            return;
        }
        try
        {
            var selectedClient = (ReportService.MergedClient)ClientPicker.SelectedItem;
            var securityLevel =
                (SecurityLevel)Enum.Parse(typeof(SecurityLevel), SecurityLevelPicker.SelectedItem.ToString());
            var securedObject = new SecuredObject(
                Guid.NewGuid(),
                NameEntry.Text,
                AddressEntry.Text,
                double.Parse(AreaEntry.Text),
                securityLevel,
                selectedClient.Id,
                selectedClient.ClientType
            );

            _controller.AddSecuredObject(securedObject);
            _controller.UpdateTable();
            await Navigation.PopAsync();
        }
        catch (Exception exception)
        {
            await DisplayAlert("Ошибка", exception.Message, "OK");
        }
    }
}