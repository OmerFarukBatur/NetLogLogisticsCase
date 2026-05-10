using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistributionCaseStudy.Models.ViewComponents
{
    public class Header : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = HttpContext.User;
            var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            var roleName = role switch
            {
                "Admin" => "Yönetici",
                "Opener" => "Kullanıcı",
                "Analyst" => "Analist",
                "Developer" => "Geliştirici",
                _ => role
            };

            var model = new HeaderViewModel
            {
                FullName = $"{claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value} {claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value}".Trim(),
                Role = role,
                RoleName = roleName
            };
            return View(model);
        }
    }
}
