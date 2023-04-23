namespace UserManagementAPI.Models.ViewModels
{
    public class TicketViewModel
    {
        public TicketViewModel(int ticketId,string ticketName, string location, DateTime dateBooked, decimal price)
        {
            TicketId = ticketId;
            TicketName = ticketName;
            Location = location;
            DateBooked = dateBooked;
            Price = price;


        }

        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public string Location { get; set; }
        public DateTime DateBooked { get; set; }
        public decimal Price { get; set; }
        

    }
}
