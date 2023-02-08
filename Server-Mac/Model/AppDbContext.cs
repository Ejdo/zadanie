using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Models;
public class AppDbContext : DbContext
{
    public DbSet<Employee>? Employees { get; set; }

    public DbSet<Position>? Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Position>().HasData(
        new Position() { Id = 1, Name = "Tester" },
        new Position() { Id = 2, Name = "Programátor" },
        new Position() { Id = 3, Name = "Support" },
        new Position() { Id = 4, Name = "Analytik" },
        new Position() { Id = 5, Name = "Obchodník" },
        new Position() { Id = 6, Name = "Iné" }        
        );
        modelBuilder.Entity<Employee>()
            .HasMany<EmployeePosition>(employee => employee.EmployeePositions)
            .WithOne(employeePosition => employeePosition.Employee)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Position>()
            .HasMany<EmployeePosition>(employee => employee.EmployeePositions)
            .WithOne(employeePosition => employeePosition.Position)
            .OnDelete(DeleteBehavior.Cascade);
    }  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=postgres");
    }
}