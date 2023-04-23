namespace UserManagementAPI.Models.ViewModels
{
    public class UserTicketViewModel
    {

        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public string UserName { get; set; }
        public string TicketName { get; set; }
        public string Location { get; set; }
        public DateTime DateBooked { get; set; }
        public decimal Price { get; set; }

    }
}
