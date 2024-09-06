using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products.RemoveProduct
{
    internal sealed class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(p=>p.Id == request.Id,cancellationToken);

            if (product is null) 
            {
                throw new ArgumentException("Ürün Bulunamadı.");
            }

            _productRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
