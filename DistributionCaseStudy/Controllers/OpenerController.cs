using Core.DTOs.OpenerDtos;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DistributionCaseStudy.Controllers
{
    [Authorize(Roles = "Opener")]
    public class OpenerController : BaseController
    {
        private readonly IOpenerService _openerService;

        public OpenerController(IOpenerService openerService)
        {
            _openerService = openerService;
        }

        public async Task<IActionResult> Index(
            string search = null,
            int? stage = null,
            int pageIndex = 1)
        {
            var tasks = await _openerService.GetMyTasksAsync(
                PersonnelId, search, stage, pageIndex);

            ViewBag.Search = search;
            ViewBag.Stage = stage;
            ViewBag.StageList = GetStageSelectList(stage);

            return View(tasks);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var task = await _openerService.GetTaskDetailAsync(id, PersonnelId);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TaskCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _openerService.CreateTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = result.Message;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var task = await _openerService.GetTaskDetailAsync(id, PersonnelId);
            if (task == null) return NotFound();

            if (task.Stage != TaskStage.Open)
            {
                TempData["ErrorMessage"] = "Yalnızca 'Açıldı' durumundaki task'lar düzenlenebilir.";
                return RedirectToAction(nameof(Detail), new { id });
            }

            var dto = new TaskUpdateDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                ExpectationNotes = task.ExpectationNotes,
                DueDate = task.DueDate
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TaskUpdateDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _openerService.UpdateTaskAsync(model, PersonnelId);

            if (result.Status)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = result.Message;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _openerService.DeleteTaskAsync(id, PersonnelId);

            if (result.Status)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        private SelectList GetStageSelectList(int? selected = null)
        {
            var stages = Enum.GetValues(typeof(TaskStage))
                .Cast<TaskStage>()
                .Select(s => new
                {
                    Value = (int)s,
                    Name = s.GetDisplayName()
                })
                .ToList();

            return new SelectList(stages, "Value", "Name", selected);
        }
    }
}
