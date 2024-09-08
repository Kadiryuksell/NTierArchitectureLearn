using MediatR;
using Microsoft.AspNetCore.Identity;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.Business.Features.Auth.Register
{
    internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

            return Unit.Value;
        }
    }
}
