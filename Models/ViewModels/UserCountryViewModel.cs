namespace UserManagementAPI.Models.ViewModels
{
    public class UserCountryViewModel
    {
        public UserCountryViewModel()
        {
            
        }

        public UserCountryViewModel(string country, int quantity)
        {
            Country = country;
            Quantity = quantity;
        }
        public string Country { get; set; }
        public int Quantity { get; set; }

    }
}
