using Core.DTOs.AdminDtos;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DistributionCaseStudy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(
            string search = null,
            int? roleId = null,
            int pageIndex = 1)
        {
            var users = await _adminService.GetUsersAsync(search, roleId, pageIndex);
            var roles = await _adminService.GetRolesAsync();

            ViewBag.Search = search;
            ViewBag.RoleId = roleId;
            ViewBag.RoleList = new SelectList(roles.Select(r => new { r.Value, r.Name }), "Value", "Name", roleId);

            return View(users);
        }


        public async Task<IActionResult> Detail(int id)
        {
            var user = await _adminService.GetUserDetailAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRoleSelectList();
            return View(new UserCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                await LoadRoleSelectList();
                return View(model);
            }

            var result = await _adminService.CreateUserAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = result.Message;
            await LoadRoleSelectList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _adminService.GetUserDetailAsync(id);
            if (user == null) return NotFound();

            var dto = new UserUpdateDto
            {
                Id = user.Id,
                PersonnelId = user.PersonnelId,
                Email = user.Email,
                RoleId = user.RoleId,
                IsActive = user.IsActive,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status
            };

            await LoadRoleSelectList(dto.RoleId);
            await LoadStatusSelectList(dto.Status);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                await LoadRoleSelectList(model.RoleId);
                await LoadStatusSelectList(model.Status);
                return View(model);
            }

            var result = await _adminService.UpdateUserAsync(model);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = result.Message;
            await LoadRoleSelectList(model.RoleId);
            await LoadStatusSelectList(model.Status);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminService.DeleteUserAsync(id);
            if (result.Status)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadRoleSelectList(int? selectedRoleId = null)
        {
            var roles = await _adminService.GetRolesAsync();
            ViewBag.RoleList = new SelectList(
                roles.Select(r => new { r.Value, r.Name }),
                "Value", "Name", selectedRoleId);
        }

        private Task LoadStatusSelectList(PersonnelStatus? selected = null)
        {
            var statuses = EnumHelper.GetSelectList<PersonnelStatus>();
            ViewBag.StatusList = new SelectList(
                statuses.Select(s => new { s.Value, s.Name }),
                "Value", "Name", (int?)selected);
            return Task.CompletedTask;
        }
    }
}
