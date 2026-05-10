using AutoMapper;
using Core.DTOs;
using Core.DTOs.OpenerDtos;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class OpenerService : IOpenerService
    {
        private readonly IRepository<Core.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public OpenerService(
            IRepository<Core.Entities.Task> taskRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TaskListDto>> GetMyTasksAsync(
            int personnelId,
            string search = null,
            int? stage = null,
            int pageIndex = 1,
            int pageSize = 25)
        {
            var query = _taskRepository
                .GetWhere(t => t.CreatedByPersonnelId == personnelId)
                .Include(t => t.TaskAnalysis)
                    .ThenInclude(a => a.AnalystPersonnel)
                .Include(t => t.TaskAnalysis)
                    .ThenInclude(a => a.TaskDevelopment)
                        .ThenInclude(d => d.DeveloperPersonnel)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(search) ||
                                          t.Description.ToLower().Contains(search));
            }

            if (stage.HasValue)
                query = query.Where(t => (int)t.Stage == stage.Value);

            query = query.OrderByDescending(t => t.CreatedAt);

            var paged = await PaginatedList<Core.Entities.Task>.CreateAsync(query, pageIndex, pageSize);
            var dtos = _mapper.Map<List<TaskListDto>>(paged.ToList());
            return new PaginatedList<TaskListDto>(dtos, paged.TotalCount, pageIndex, pageSize);
        }

        public async Task<TaskDetailDto> GetTaskDetailAsync(int taskId, int personnelId)
        {
            var task = await _taskRepository
                .GetWhere(t => t.Id == taskId && t.CreatedByPersonnelId == personnelId)
                .Include(t => t.TaskAnalysis)
                    .ThenInclude(a => a.AnalystPersonnel)
                .Include(t => t.TaskAnalysis)
                    .ThenInclude(a => a.TaskDevelopment)
                        .ThenInclude(d => d.DeveloperPersonnel)
                .FirstOrDefaultAsync();

            return task == null ? null : _mapper.Map<TaskDetailDto>(task);
        }

        public async Task<ResponseMessageDto> CreateTaskAsync(TaskCreateDto dto, int personnelId)
        {
            var task = _mapper.Map<Core.Entities.Task>(dto);
            task.CreatedByPersonnelId = personnelId;
            task.Stage = TaskStage.Open;

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task başarıyla oluşturuldu."
            };
        }

        public async Task<ResponseMessageDto> UpdateTaskAsync(TaskUpdateDto dto, int personnelId)
        {
            var task = await _taskRepository
                .GetWhere(t => t.Id == dto.Id && t.CreatedByPersonnelId == personnelId,
                          tracking: true)
                .FirstOrDefaultAsync();

            if (task == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Task bulunamadı."
                };

            if (task.Stage != TaskStage.Open)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Yalnızca 'Açıldı' durumundaki task'lar düzenlenebilir."
                };

            _mapper.Map(dto, task);
            _taskRepository.Update(task);
            await _taskRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task başarıyla güncellendi."
            };
        }

        public async Task<ResponseMessageDto> DeleteTaskAsync(int taskId, int personnelId)
        {
            var task = await _taskRepository
                .GetWhere(t => t.Id == taskId && t.CreatedByPersonnelId == personnelId,
                          tracking: true)
                .FirstOrDefaultAsync();

            if (task == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Task bulunamadı."
                };
            if (task.Stage != TaskStage.Open)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Yalnızca 'Açıldı' durumundaki task'lar silinebilir."
                };

            _taskRepository.Remove(task);
            await _taskRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task başarıyla silindi."
            };
        }
    }
}
