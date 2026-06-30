using System;
using CinemaBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace CinemaBooking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerModelBuilderExtensions
                .UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaBooking.Models.Entities.AppUser", b =>
            {
                b.Property<string>("Id").HasColumnType("nvarchar(450)");
                b.Property<int>("AccessFailedCount").HasColumnType("int");
                b.Property<string>("ConcurrencyStamp").HasColumnType("nvarchar(max)");
                b.Property<string>("Email").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<bool>("EmailConfirmed").HasColumnType("bit");
                b.Property<string>("FullName").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<bool>("LockoutEnabled").HasColumnType("bit");
                b.Property<DateTimeOffset?>("LockoutEnd").HasColumnType("datetimeoffset");
                b.Property<string>("NormalizedEmail").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<string>("NormalizedUserName").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<string>("PasswordHash").HasColumnType("nvarchar(max)");
                b.Property<string>("PhoneNumber").HasColumnType("nvarchar(max)");
                b.Property<bool>("PhoneNumberConfirmed").HasColumnType("bit");
                b.Property<string>("SecurityStamp").HasColumnType("nvarchar(max)");
                b.Property<bool>("TwoFactorEnabled").HasColumnType("bit");
                b.Property<string>("UserName").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.HasKey("Id");
                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("CinemaBooking.Models.Entities.Booking", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<DateTime>("BookedAt").HasColumnType("datetime2");
                b.Property<int>("NumberOfSeats").HasColumnType("int");
                b.Property<int>("ShowtimeId").HasColumnType("int");
                b.Property<decimal>("TotalPrice").HasColumnType("decimal(18,2)");
                b.Property<string>("UserId").IsRequired().HasColumnType("nvarchar(450)");
                b.HasKey("Id");
                b.HasIndex("ShowtimeId");
                b.HasIndex("UserId");
                b.ToTable("Bookings");
            });

            modelBuilder.Entity("CinemaBooking.Models.Entities.Cinema", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Location").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("Name").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("Cinemas");
            });

            modelBuilder.Entity("CinemaBooking.Models.Entities.Hall", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<int>("Capacity").HasColumnType("int");
                b.Property<int>("CinemaId").HasColumnType("int");
                b.Property<string>("Name").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.HasIndex("CinemaId");
                b.ToTable("Halls");
            });

            modelBuilder.Entity("CinemaBooking.Models.Entities.Movie", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Description").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<int>("DurationMinutes").HasColumnType("int");
                b.Property<string>("Genre").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("PosterPath").HasColumnType("nvarchar(max)");
                b.Property<DateTime>("ReleaseDate").HasColumnType("datetime2");
                b.Property<string>("Title").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("Movies");
            });

            modelBuilder.Entity("CinemaBooking.Models.Entities.Showtime", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<int>("HallId").HasColumnType("int");
                b.Property<int>("MovieId").HasColumnType("int");
                b.Property<DateTime>("StartTime").HasColumnType("datetime2");
                b.Property<decimal>("TicketPrice").HasColumnType("decimal(18,2)");
                b.HasKey("Id");
                b.HasIndex("HallId");
                b.HasIndex("MovieId");
                b.ToTable("Showtimes");
            });
#pragma warning restore 612, 618
        }
    }
}
