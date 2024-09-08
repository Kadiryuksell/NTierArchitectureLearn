using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.UserRoles.SetUserRole
{
    internal sealed class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
    {
        private readonly IUserRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public SetUserRoleCommandHandler(IUserRoleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var checkIsRoleSetExists = await _repository
                .AnyAsync(p =>p.AppUserId == request.UserId && p.AppRoleId == request.RoleId,cancellationToken);

            if (checkIsRoleSetExists) 
            {
                throw new ArgumentException("Kullanıcı bu role zaten sahip");
            }

            UserRole userRole = new UserRole()
            {
                AppRoleId = request.RoleId,
                AppUserId = request.UserId,
            };

            await _repository.AddAsync(userRole,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
}
