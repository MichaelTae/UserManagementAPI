using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.Entities
{
    public class LocationEntity
    {
        [Key]
        public int LocationId { get; set; }

        public string Address { get; set; }

        public string Zipcode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        
    }
}
