using Domain.Products;

namespace Infrastructure.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context)
        : base(context)
    {
    }

}
