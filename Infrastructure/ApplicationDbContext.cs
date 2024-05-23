using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    private readonly IPublisher _publisher;

    public DbSet<Order> Orders { get ; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

}
