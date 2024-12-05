using Microsoft.EntityFrameworkCore;
using HotelSol.Data.Models;
using HotelSol.Data.ViewModels;

namespace HotelSol.Data
{
    public class HotelSolDbContext : DbContext
    {
        public HotelSolDbContext(DbContextOptions<HotelSolDbContext> options) : base(options) { }

        // DbSets para cada tabla
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<Payments> Payments { get; set; }

        // Configuración adicional del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para relaciones uno a muchos (ejemplo)
            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Reservations)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.ReservationID);

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Rooms)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.RoomID);

            modelBuilder.Entity<Reservations>()
                .HasOne(r => r.Guests)
                .WithMany()
                .HasForeignKey(r => r.GuestID);

            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Reservation)
                .WithMany()
                .HasForeignKey(p => p.ReservationID);
            modelBuilder.Entity<Reservations>()
            .HasKey(p => p.ReservationID);
            modelBuilder.Entity<Payments>()
            .HasKey(p => p.PaymentID);
            modelBuilder.Entity<Guests>()
            .HasKey(g => g.GuestID);
            modelBuilder.Entity<Rooms>()
            .HasKey(p => p.RoomID);
        }
    }
}
