using Core.DTOs.AuthDtos;
using Core.Enums;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistributionCaseStudy.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectByRole(User.FindFirst(ClaimTypes.Role)?.Value);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (response, data) = await _authService.LoginAsync(model);

            if (!response.Status)
            {
                ViewBag.Error = response.Message;
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                new Claim("PersonnelId",             data.PersonnelId.ToString()),
                new Claim(ClaimTypes.Name,            data.FirstName),
                new Claim(ClaimTypes.Surname,         data.LastName),
                new Claim(ClaimTypes.Email,           data.Email),
                new Claim(ClaimTypes.Role,            data.Role),
                new Claim("RoleId",                   data.RoleId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectByRole(((UserRole)data.RoleId).ToString());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult PwdReset()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectByRole(User.FindFirst(ClaimTypes.Role)?.Value);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PwdReset(PasswordResetDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.PasswordResetAsync(model);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.ErrorMessage = result.Message;
            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        private IActionResult RedirectByRole(string role)
        {
            if (Enum.TryParse<UserRole>(role, out var userRole))
            {
                return userRole switch
                {
                    UserRole.Admin => RedirectToAction("Index", "Admin"),
                    UserRole.Opener => RedirectToAction("Index", "Opener"),
                    UserRole.Analyst => RedirectToAction("Index", "Analyst"),
                    UserRole.Developer => RedirectToAction("Index", "Developer"),
                    _ => RedirectToAction("Login", "Auth")
                };
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
