using Core.Model.Users;
    
    namespace SAS.Pages.Clients;
    
    public partial class AddCorporateClientPage : ContentPage
    {
        public AddCorporateClientPage()
        {
            InitializeComponent();
        }
    
        public event EventHandler<CorporateClient>? ClientAdded;
    
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var newClient = new CorporateClient(
                    Guid.NewGuid(),
                    CompanyNameEntry.Text,
                    null
                );
    
                ClientAdded?.Invoke(this, newClient);
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
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }