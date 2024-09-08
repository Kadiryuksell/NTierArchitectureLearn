using ErrorOr;
using MediatR;
using NTierArchitecture.Business.Features.Categories.CreateCategory;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

internal sealed class CreateCategoryCommandhandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Unit>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandhandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryNameExists = await _categoryRepository.AnyAsync(p => p.Name == request.name, cancellationToken);

        if (isCategoryNameExists)
        {
            return Error.Conflict("NameIsExists","Bu Kategori Daha Önce oluşturulmuş");
        }

        Category category = new()
        {
            Name = request.name,
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
