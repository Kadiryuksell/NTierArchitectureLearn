using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products.Createproduct
{
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            bool isProductNameExists = await _repository.AnyAsync(p=>p.Name == request.Name,cancellationToken);
            if (isProductNameExists)
            {
                throw new ArgumentException("Bu ürün Adı Kullanılmış");
            }

            Product product = new()
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId
            };

            await _repository.AddAsync(product,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
