using MediatR;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products.GetProduct
{
    internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<Product>>
    {
        private readonly IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().OrderBy(p=>p.Name).ToListAsync(cancellationToken);
        }
    }
}
