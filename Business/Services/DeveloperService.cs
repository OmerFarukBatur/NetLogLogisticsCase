using AutoMapper;
using Core.DTOs;
using Core.DTOs.DeveloperDtos;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<TaskDevelopment> _developmentRepository;
        private readonly IRepository<Core.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public DeveloperService(
            IRepository<TaskDevelopment> developmentRepository,
            IRepository<Core.Entities.Task> taskRepository,
            IMapper mapper)
        {
            _developmentRepository = developmentRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DeveloperTaskListDto>> GetMyTasksAsync(
            int personnelId,
            string search = null,
            int? status = null,
            int pageIndex = 1,
            int pageSize = 15)
        {
            var query = _developmentRepository
                .GetWhere(d => d.DeveloperPersonnelId == personnelId)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.Task)
                        .ThenInclude(t => t.CreatedByPersonnel)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.AnalystPersonnel)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(d =>
                    d.TaskAnalysis.Task.Title.ToLower().Contains(search));
            }

            if (status.HasValue)
                query = query.Where(d => (int)d.Status == status.Value);

            query = query.OrderByDescending(d => d.CreatedAt);

            var paged = await PaginatedList<TaskDevelopment>.CreateAsync(query, pageIndex, pageSize);
            var dtos = _mapper.Map<List<DeveloperTaskListDto>>(paged.ToList());
            return new PaginatedList<DeveloperTaskListDto>(dtos, paged.TotalCount, pageIndex, pageSize);
        }

        public async Task<DeveloperTaskDetailDto> GetTaskDetailAsync(
            int developmentId,
            int personnelId)
        {
            var development = await _developmentRepository
                .GetWhere(d => d.Id == developmentId && d.DeveloperPersonnelId == personnelId)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.Task)
                        .ThenInclude(t => t.CreatedByPersonnel)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.AnalystPersonnel)
                .FirstOrDefaultAsync();

            return development == null ? null : _mapper.Map<DeveloperTaskDetailDto>(development);
        }

        public async Task<ResponseMessageDto> UpdateStatusAsync(
            DevelopmentUpdateDto dto,
            int personnelId)
        {
            var development = await _developmentRepository
                .GetWhere(d => d.Id == dto.DevelopmentId && d.DeveloperPersonnelId == personnelId,
                          tracking: true)
                .FirstOrDefaultAsync();

            if (development == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Geliştirme kaydı bulunamadı."
                };

            if (development.Status == DevelopmentStatus.Done ||
                development.Status == DevelopmentStatus.Cancelled)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Tamamlanmış veya iptal edilmiş task güncellenemez."
                };

            development.Status = dto.Status;
            development.DeveloperNotes = dto.DeveloperNotes;

            _developmentRepository.Update(development);
            await _developmentRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Durum güncellendi."
            };
        }

        public async Task<ResponseMessageDto> CompleteTaskAsync(
            DevelopmentCompleteDto dto,
            int personnelId)
        {
            var development = await _developmentRepository
                .GetWhere(d => d.Id == dto.DevelopmentId && d.DeveloperPersonnelId == personnelId,
                          tracking: true)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.Task)
                .FirstOrDefaultAsync();

            if (development == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Geliştirme kaydı bulunamadı."
                };

            if (development.Status == DevelopmentStatus.Done)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Bu task zaten tamamlanmış."
                };

            if (development.Status == DevelopmentStatus.Cancelled)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "İptal edilmiş task tamamlanamaz."
                };

            development.Status = DevelopmentStatus.Done;
            development.DeveloperNotes = dto.DeveloperNotes;
            development.CompletedAt = DateTime.UtcNow;

            _developmentRepository.Update(development);

            var task = development.TaskAnalysis.Task;
            task.Stage = TaskStage.Completed;
            _taskRepository.Update(task);

            await _developmentRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task başarıyla tamamlandı."
            };
        }

        public async Task<ResponseMessageDto> CancelTaskAsync(
            DevelopmentCancelDto dto,
            int personnelId)
        {
            var development = await _developmentRepository
                .GetWhere(d => d.Id == dto.DevelopmentId && d.DeveloperPersonnelId == personnelId,
                          tracking: true)
                .Include(d => d.TaskAnalysis)
                    .ThenInclude(a => a.Task)
                .FirstOrDefaultAsync();

            if (development == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Geliştirme kaydı bulunamadı."
                };

            if (development.Status == DevelopmentStatus.Done)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Tamamlanmış task iptal edilemez."
                };

            if (development.Status == DevelopmentStatus.Cancelled)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Bu task zaten iptal edilmiş."
                };

            development.Status = DevelopmentStatus.Cancelled;
            development.CancellationReason = dto.CancellationReason;

            _developmentRepository.Update(development);

            var task = development.TaskAnalysis.Task;
            task.Stage = TaskStage.Rejected;
            _taskRepository.Update(task);

            await _developmentRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task iptal edildi."
            };
        }
    }
}
