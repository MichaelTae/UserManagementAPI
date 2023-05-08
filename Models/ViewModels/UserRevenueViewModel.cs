namespace UserManagementAPI.Models.ViewModels
{
    public class UserRevenueViewModel
    {
        public UserRevenueViewModel()
        {
            
        }

        public UserRevenueViewModel(string email, decimal revenue)
        {
            Email = email;
            Revenue = revenue;
            
        }

        public string Email
        {
            get; set;
        }

        public decimal Revenue
        {
            get; set;
        }
    }
}
