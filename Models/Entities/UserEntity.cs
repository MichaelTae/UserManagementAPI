using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationEntity? Location { get; set; }

        public UserInfoEntity? UserInfo { get; set; }

        public ICollection<UserTicketEntity>? UserTickets { get; set; }
    }
}
