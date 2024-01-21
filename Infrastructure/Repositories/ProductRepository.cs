using Domain.Products;

namespace Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Add(product);
    }
}
