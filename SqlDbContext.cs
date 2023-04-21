using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models.Entities;

namespace UserManagementAPI
{
    public class SqlDbContext : DbContext
    {

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {


        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<UserInfoEntity> UserInfos { get; set; }
        public virtual DbSet<UserTicketEntity> UserTickets { get; set; }
        public virtual DbSet<TicketEntity> Tickets { get; set; }
        public virtual DbSet<LocationEntity> Locations { get; set; }


    }
}
