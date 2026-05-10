using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistributionCaseStudy.Models.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = HttpContext.User;
            var model = new SidebarViewModel
            {
                Role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                FullName = $"{claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value} {claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value}".Trim(),
                Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty
            };
            return View(model);
        }
    }
}
