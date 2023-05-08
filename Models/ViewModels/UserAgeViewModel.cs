namespace UserManagementAPI.Models.ViewModels
{
    public class UserAgeViewModel
    {
        public UserAgeViewModel()
        {
            
        }
        public UserAgeViewModel(string ageSpan, int quantity)
        {
            AgeSpan = ageSpan;
            Quantity = quantity;

        }



        public string AgeSpan { get; set; }
        public int Quantity { get; set; }
    }
}
