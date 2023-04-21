namespace UserManagementAPI.Models.UpdateModels
{
    public class UpdateCompleteProfile
    {
        public UpdateCompleteProfile(string firstName, string surname,  string address,
            string zipcode, string state, string country, string gender, int age)
        {
            FirstName = firstName;
            Surname = surname;
            Address = address;
            Zipcode = zipcode;
            State = state;
            Country = country;
            Gender = gender;
            Age = age;
        }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }



    }
}
