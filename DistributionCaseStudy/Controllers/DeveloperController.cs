using Core.DTOs.DeveloperDtos;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DistributionCaseStudy.Controllers
{
    [Authorize(Roles = "Developer")]
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public async Task<IActionResult> Index(
            string search = null,
            int? status = null,
            int pageIndex = 1)
        {
            var tasks = await _developerService.GetMyTasksAsync(
                PersonnelId, search, status, pageIndex);

            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.StatusList = GetStatusSelectList(status);

            return View(tasks);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _developerService.GetTaskDetailAsync(id, PersonnelId);
            if (detail == null) return NotFound();

            ViewBag.StatusList = GetEditableStatusSelectList(detail.Status);
            return View(detail);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(DevelopmentUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen formu eksiksiz doldurunuz.";
                return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
            }

            var result = await _developerService.UpdateStatusAsync(model, PersonnelId);

            if (result.Status)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
        }

        [HttpPost]
        public async Task<IActionResult> Complete(DevelopmentCompleteDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Tamamlarken geliştirici notu zorunludur.";
                return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
            }

            var result = await _developerService.CompleteTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(DevelopmentCancelDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "İptal nedeni zorunludur.";
                return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
            }

            var result = await _developerService.CancelTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Detail), new { id = model.DevelopmentId });
        }
        private SelectList GetStatusSelectList(int? selected = null)
        {
            var items = Enum.GetValues(typeof(DevelopmentStatus))
                .Cast<DevelopmentStatus>()
                .Select(s => new { Value = (int)s, Name = s.GetDisplayName() })
                .ToList();
            return new SelectList(items, "Value", "Name", selected);
        }
        private SelectList GetEditableStatusSelectList(DevelopmentStatus current)
        {
            var items = new List<DevelopmentStatus>
                {
                    DevelopmentStatus.InProgress,
                    DevelopmentStatus.OnHold
                }
                .Select(s => new { Value = (int)s, Name = s.GetDisplayName() })
                .ToList();
            return new SelectList(items, "Value", "Name", (int)current);
        }
    }
}
