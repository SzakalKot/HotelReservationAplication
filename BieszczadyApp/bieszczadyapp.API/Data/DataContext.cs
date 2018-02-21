using bieszczadyapp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace bieszczadyapp.API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)  {}

        public DbSet<User> Users {get; set;}

        public DbSet<Photo> Photos {get; set;}
        public DbSet<Room> Rooms {get; set;}

        public DbSet<Reservation> Reservation {get; set;}
    }
}

