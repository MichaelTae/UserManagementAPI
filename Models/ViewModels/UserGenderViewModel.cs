namespace UserManagementAPI.Models.ViewModels
{
    public class UserGenderViewModel
    {
        public UserGenderViewModel()
        {
            
        }

        public UserGenderViewModel(string gender,int quantity)
        {
            Gender = gender;
            Quantity = quantity;
            
        }

        public string Gender { get; set; }
        public int Quantity { get; set; }
    }
}
