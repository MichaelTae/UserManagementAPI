using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace UserManagementAPI.Models.Entities
{
    public class UserTicketEntity
    {
        [Key]
        public int BookingId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public UserEntity User { get; set; }

        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public TicketEntity Ticket { get; set; }

        public DateTime DateBooked { get; set; } = DateTime.Now;
    }
}
