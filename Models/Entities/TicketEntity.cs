﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.Entities
{
    public class TicketEntity
    {
        [Key]
        public int TicketId { get; set; }

        public string TicketName { get; set; }

        public string Location { get; set; }

        public DateTime DateBooked { get; set; }

        public decimal Price { get; set; }

       
    }
}
