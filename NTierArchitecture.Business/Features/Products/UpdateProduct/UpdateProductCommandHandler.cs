using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null) 
            {
                throw new ArgumentException("Ürün Bulunamadı");
            }

            if (product.Name != request.Name)
            {
                bool isProductNameExists = await _productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
                if (isProductNameExists)
                {
                    throw new ArgumentException("Ürün adı zaten kullanılıyor.");
                }
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.CategoryId = request.CategoryId;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            
        }
    }
}
