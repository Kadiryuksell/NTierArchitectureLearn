using MediatR;
using NTierArchitecture.Business.Features.Categories.CreateCategory;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

internal sealed class CreateCategoryCommandhandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandhandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryNameExists = await _categoryRepository.AnyAsync(p => p.Name == request.name, cancellationToken);

        if (isCategoryNameExists)
        {
            throw new ArgumentException("Bu Kategori Daha Önce oluşturulmuş");
        }

        Category category = new()
        {
            Name = request.name,
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
