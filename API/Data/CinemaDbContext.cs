using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using API.Entities;
using Microsoft.Extensions.Hosting;
using System;

namespace API.Data
{
    public class CinemaDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCombo> FoodCombos { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<FoodBill> FoodBills { get; set; }
        public DbSet<ComboBill> ComboBills { get; set; }
        public DbSet<TicketBill> TicketBills { get; set; }
        public DbSet<RoomFilm> RoomFilms { get; set; }

        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Combo>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Film>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<FoodCombo>()
                .HasKey(c => new { c.FoodID, c.ComboID });
            modelBuilder.Entity<Food>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Room>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<RoomFilm>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Ticket>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<FoodBill>()
                .HasKey(c => new { c.FoodID, c.BillID });
            modelBuilder.Entity<ComboBill>()
                .HasKey(c => new { c.ComboID, c.BillID });
            modelBuilder.Entity<TicketBill>()
                .HasKey(c => new { c.TicketID, c.BillID });

            modelBuilder
                .Entity<Film>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder
                .Entity<RoomFilm>()
                .HasIndex(x => new {x.RoomID, x.FilmID, x.DayPremiered, x.TimePremiered})
                .IsUnique();

            modelBuilder
                .Entity<Combo>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .Property(r => r.ID)
                .ValueGeneratedNever();

            modelBuilder
                .Entity<Food>()
                .HasIndex(x => new {x.Name, x.Size})
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.RoomFilm)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.RoomFilmID);

            modelBuilder.Entity<RoomFilm>()
                .HasOne(t => t.Room)
                .WithMany(r => r.RoomFilms)
                .HasForeignKey(t => t.RoomID);

            modelBuilder.Entity<RoomFilm>()
                .HasOne(t => t.Film)
                .WithMany(f => f.RoomFilms)
                .HasForeignKey(t => t.FilmID);

            modelBuilder.Entity<FoodCombo>()
                .HasOne(cf => cf.Combo)
                .WithMany(c => c.FoodCombos)
                .HasForeignKey(cf => cf.ComboID);

            modelBuilder.Entity<FoodCombo>()
                .HasOne(cf => cf.Food)
                .WithMany(f => f.FoodCombos)
                .HasForeignKey(cf => cf.FoodID);

            modelBuilder.Entity<FoodBill>()
                .HasOne(fb => fb.Bill)
                .WithMany(b => b.FoodBills)
                .HasForeignKey(fb => fb.BillID);

            modelBuilder.Entity<FoodBill>()
                .HasOne(fb => fb.Food)
                .WithMany(f => f.FoodBills)
                .HasForeignKey(fb => fb.FoodID);

            modelBuilder.Entity<ComboBill>()
                .HasOne(cb => cb.Bill)
                .WithMany(b => b.ComboBills)
                .HasForeignKey(cb => cb.BillID);

            modelBuilder.Entity<ComboBill>()
                .HasOne(cb => cb.Combo)
                .WithMany(c => c.ComboBills)
                .HasForeignKey(cb => cb.ComboID);

            modelBuilder.Entity<TicketBill>()
                .HasOne(tb => tb.Bill)
                .WithMany(b => b.TicketBills)
                .HasForeignKey(tb => tb.BillID);

            modelBuilder.Entity<TicketBill>()
                .HasOne(tb => tb.Ticket)
                .WithOne(t => t.TicketBill)
                .HasForeignKey<TicketBill>(tb => tb.TicketID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
