using MediatR;
using Microsoft.AspNetCore.Identity;
using NTierArchitecture.Entities.Events;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.Business.Features.Auth.Register
{
    internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var checkUserNameExists = await _userManager.FindByNameAsync(request.UserName);
            var checkEmailExists = await _userManager.FindByEmailAsync(request.Email);

            if(checkUserNameExists is not null)
            {
                throw new ArgumentException("Bu UserName zaten kullanılıyor.");
            }

            if (checkEmailExists is not null)
            {
                throw new ArgumentException("Bu Email zaten kullanılıyor.");
            }

            AppUser appUser = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                LastName = request.LastName,
                Name = request.Name
            };

            await _userManager.CreateAsync(appUser, request.Password);
            await _mediator.Publish(new UserDomainEvent(appUser));
            return Unit.Value;
        }
    }
}
