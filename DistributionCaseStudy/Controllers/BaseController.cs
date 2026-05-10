using Core.DTOs.AuthDtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistributionCaseStudy.Controllers
{
    public class BaseController : Controller
    {
        protected AuthenticationDto CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                return new AuthenticationDto
                {
                    Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                    PersonnelId = int.Parse(User.FindFirst("PersonnelId")?.Value ?? "0"),
                    FirstName = User.FindFirst(ClaimTypes.Name)?.Value ?? "",
                    LastName = User.FindFirst(ClaimTypes.Surname)?.Value ?? "",
                    Email = User.FindFirst(ClaimTypes.Email)?.Value ?? "",
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? "",
                    RoleId = int.Parse(User.FindFirst("RoleId")?.Value ?? "0"),
                    IsActive = true
                };
            }
        }

        protected bool IsAdmin => CurrentUser?.Role == "Admin";
        protected bool IsOpener => CurrentUser?.Role == "Opener";
        protected bool IsAnalyst => CurrentUser?.Role == "Analyst";
        protected bool IsDeveloper => CurrentUser?.Role == "Developer";

        protected int UserId => CurrentUser?.Id ?? 0;
        protected int PersonnelId => CurrentUser?.PersonnelId ?? 0;
        protected string UserEmail => CurrentUser?.Email ?? "";
        protected string FullName => $"{CurrentUser?.FirstName} {CurrentUser?.LastName}".Trim();
    }
}
