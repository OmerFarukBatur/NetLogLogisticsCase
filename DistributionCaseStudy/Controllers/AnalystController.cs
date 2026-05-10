using Core.DTOs.AnalystDtos;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DistributionCaseStudy.Controllers
{
    [Authorize(Roles = "Analyst")]
    public class AnalystController : BaseController
    {
        private readonly IAnalystService _analystService;

        public AnalystController(IAnalystService analystService)
        {
            _analystService = analystService;
        }

        public async Task<IActionResult> Index(
            string search = null,
            int? status = null,
            int pageIndex = 1)
        {
            var analyses = await _analystService.GetMyAnalysesAsync(
                PersonnelId, search, status, pageIndex);

            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.StatusList = GetAnalysisStatusSelectList(status);

            return View(analyses);
        }

        public async Task<IActionResult> Pending(
            string search = null,
            int pageIndex = 1)
        {
            var tasks = await _analystService.GetPendingTasksAsync(search, pageIndex);
            ViewBag.Search = search;
            return View(tasks);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _analystService.GetAnalysisDetailAsync(id, PersonnelId);
            if (detail == null) return NotFound();

            LoadDifficultySelectList(detail.DifficultyLevel);
            LoadPrioritySelectList(detail.Priority);

            return View(detail);
        }

        [HttpPost]
        public async Task<IActionResult> TakeTask(int taskId)
        {
            var result = await _analystService.TakeTaskAsync(taskId, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                var analysis = await GetAnalysisIdByTaskId(taskId);
                if (analysis > 0)
                    return RedirectToAction(nameof(Detail), new { id = analysis });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Pending));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnalysis(AnalysisUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen formu eksiksiz doldurunuz.";
                return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
            }

            var result = await _analystService.UpdateAnalysisAsync(model, PersonnelId);

            if (result.Status)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(AnalysisRejectDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Red nedeni zorunludur.";
                return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
            }

            var result = await _analystService.RejectTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
        }

        [HttpPost]
        public async Task<IActionResult> Approve(AnalysisApproveDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen zorunlu alanları doldurunuz.";
                return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
            }

            var result = await _analystService.ApproveTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Detail), new { id = model.AnalysisId });
        }

        private SelectList GetAnalysisStatusSelectList(int? selected = null)
        {
            var items = Enum.GetValues(typeof(AnalysisStatus))
                .Cast<AnalysisStatus>()
                .Select(s => new { Value = (int)s, Name = s.GetDisplayName() })
                .ToList();
            return new SelectList(items, "Value", "Name", selected);
        }

        private void LoadDifficultySelectList(DifficultyLevel selected)
        {
            var items = Enum.GetValues(typeof(DifficultyLevel))
                .Cast<DifficultyLevel>()
                .Select(d => new { Value = (int)d, Name = d.GetDisplayName() })
                .ToList();
            ViewBag.DifficultyList = new SelectList(items, "Value", "Name", (int)selected);
        }

        private void LoadPrioritySelectList(TaskPriority selected)
        {
            var items = Enum.GetValues(typeof(TaskPriority))
                .Cast<TaskPriority>()
                .Select(p => new { Value = (int)p, Name = p.GetDisplayName() })
                .ToList();
            ViewBag.PriorityList = new SelectList(items, "Value", "Name", (int)selected);
        }

        private async Task<int> GetAnalysisIdByTaskId(int taskId)
        {
            var analysis = await _analystService.GetMyAnalysesAsync(PersonnelId, pageSize: 100);
            return analysis.FirstOrDefault(a => a.Id == taskId)?.AnalysisId ?? 0;
        }
    }
}
