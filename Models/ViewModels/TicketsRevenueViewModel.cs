namespace UserManagementAPI.Models.ViewModels
{
    public class TicketsRevenueViewModel
    {

        public TicketsRevenueViewModel()
        {
            
        }

        public TicketsRevenueViewModel(string ticketName, decimal revenue)
        {
            TicketName = ticketName;
            Revenue = revenue;
            
        }

        public string TicketName
        {
            get; set;

        }

        public decimal Revenue
        {
            get; set;
        }
    }
}
