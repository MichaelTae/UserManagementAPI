namespace UserManagementAPI.Models.CreateModels
{
    public class CreateUserModel
    {
        public CreateUserModel(string email, string password, string username )
        {
            Email = email;
            Password = password;
            Username = username;
            
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

    }
}
