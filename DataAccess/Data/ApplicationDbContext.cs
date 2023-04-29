using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){}

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        
        
    }
}
