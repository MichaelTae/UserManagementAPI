namespace UserManagementAPI.Models.ViewModels
{
    public class CompleteUserViewModel
    {
        public CompleteUserViewModel()
        {
        }

        public CompleteUserViewModel(string email, string username, string firstname, string surname,
            string address, string zipcode, string state, string country, string gender, int age, int userId)
        {
            Email = email;
            Username = username;
            FirstName = firstname;
            Surname = surname;
            Gender = gender;
            Age = age;
            Address = address;
            Zipcode = zipcode;
            State = state;
            Country = country;
            UserId = userId;
        }

       

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }

        public string? Address { get; set; }
        public string? Zipcode { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
