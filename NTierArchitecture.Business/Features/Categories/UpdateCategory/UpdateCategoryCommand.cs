using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Features.Categories.UpdateCategory
{
    public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest;


    internal sealed class UpdateCategoryCommandhandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandhandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAsync(p =>p.Id == request.Id, cancellationToken);
            if(category is null)
            {
               
            }
            if(category.Name != request.Name)
            {
                var isCategoryNameExists = await _categoryRepository.AnyAsync(p =>p.Name == request.Name, cancellationToken);
                if (isCategoryNameExists)
                {
                    throw new ArgumentException("Bu kategori daha önce oluşturulmuş");
                }

                category.Name = request.Name;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
