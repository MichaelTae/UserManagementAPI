using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models.Entities
{
    public class UserInfoEntity
    {
        [Key]
        public int UserInfoId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }
    }
}
