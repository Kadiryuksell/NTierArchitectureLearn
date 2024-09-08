using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.Repositories;
using System.Security.Claims;

namespace NTierArchitecture.DataAccess.Authorization
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _roleName;
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleAttribute(string roleName, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userHasRole = _userRoleRepository
                .GetWhere(p => p.AppUserId.ToString() == userIdClaim.Value)
                .Include(p => p.AppRole)
                .Any(p => p.AppRole.Name == _roleName);

            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
                
        }
    }
}
