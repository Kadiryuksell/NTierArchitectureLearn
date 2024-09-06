using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Repositories;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.DataAccess.Repositories
{
    internal sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
