using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistributionCaseStudy.Models.ViewComponents
{
    public class Sidebar : ViewComponent
    {

        public Sidebar()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = User as ClaimsPrincipal;
            var model = new SidebarViewModel
            {
                Role = user?.FindFirst(ClaimTypes.Role)?.Value ?? UserRole.Opener.ToString()
            };
            return View(model);
        }
    }
}
