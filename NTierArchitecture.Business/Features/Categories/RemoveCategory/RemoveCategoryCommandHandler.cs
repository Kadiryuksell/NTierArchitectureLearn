using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Categories.RemoveCategory
{
    internal sealed class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAsync(p =>p.Id == request.Id,cancellationToken);
            
            if (category is null)
            {
                throw new ArgumentException("Kategori Bulunamadı");
            }

            _categoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
