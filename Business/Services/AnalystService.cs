using AutoMapper;
using Core.DTOs;
using Core.DTOs.AnalystDtos;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AnalystService : IAnalystService
    {
        private readonly IRepository<Core.Entities.Task> _taskRepository;
        private readonly IRepository<TaskAnalysis> _analysisRepository;
        private readonly IRepository<TaskDevelopment> _developmentRepository;
        private readonly IRepository<Personnel> _personnelRepository;
        private readonly IRepository<AssignmentHistory> _historyRepository;
        private readonly IMapper _mapper;

        public AnalystService(
            IRepository<Core.Entities.Task> taskRepository,
            IRepository<TaskAnalysis> analysisRepository,
            IRepository<TaskDevelopment> developmentRepository,
            IRepository<Personnel> personnelRepository,
            IRepository<AssignmentHistory> historyRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _analysisRepository = analysisRepository;
            _developmentRepository = developmentRepository;
            _personnelRepository = personnelRepository;
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TaskPendingListDto>> GetPendingTasksAsync(
            string search = null,
            int pageIndex = 1,
            int pageSize = 25)
        {
            var query = _taskRepository
                .GetWhere(t => t.Stage == TaskStage.Open && t.TaskAnalysis == null)
                .Include(t => t.CreatedByPersonnel)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(t =>
                    t.Title.ToLower().Contains(search) ||
                    t.Description.ToLower().Contains(search));
            }

            query = query.OrderBy(t => t.CreatedAt);

            var paged = await PaginatedList<Core.Entities.Task>.CreateAsync(query, pageIndex, pageSize);
            var dtos = _mapper.Map<List<TaskPendingListDto>>(paged.ToList());
            return new PaginatedList<TaskPendingListDto>(dtos, paged.TotalCount, pageIndex, pageSize);
        }

        public async Task<PaginatedList<AnalystTaskListDto>> GetMyAnalysesAsync(
            int personnelId,
            string search = null,
            int? status = null,
            int pageIndex = 1,
            int pageSize = 25)
        {
            var query = _analysisRepository
                .GetWhere(a => a.AnalystPersonnelId == personnelId)
                .Include(a => a.Task)
                    .ThenInclude(t => t.CreatedByPersonnel)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(a => a.Task.Title.ToLower().Contains(search));
            }

            if (status.HasValue)
                query = query.Where(a => (int)a.Status == status.Value);

            query = query.OrderByDescending(a => a.CreatedAt);

            var paged = await PaginatedList<TaskAnalysis>.CreateAsync(query, pageIndex, pageSize);
            var dtos = _mapper.Map<List<AnalystTaskListDto>>(paged.ToList());
            return new PaginatedList<AnalystTaskListDto>(dtos, paged.TotalCount, pageIndex, pageSize);
        }

        public async Task<AnalystTaskDetailDto> GetAnalysisDetailAsync(
            int analysisId,
            int personnelId)
        {
            var analysis = await _analysisRepository
                .GetWhere(a => a.Id == analysisId && a.AnalystPersonnelId == personnelId)
                .Include(a => a.Task)
                    .ThenInclude(t => t.CreatedByPersonnel)
                .Include(a => a.TaskDevelopment)
                .FirstOrDefaultAsync();

            if (analysis == null) return null;

            var dto = _mapper.Map<AnalystTaskDetailDto>(analysis);

            dto.CanUpdate = analysis.Status == AnalysisStatus.InProgress ||
                            (analysis.Task.Stage == TaskStage.InDevelopment &&
                             analysis.TaskDevelopment != null &&
                             analysis.TaskDevelopment.Status != DevelopmentStatus.InProgress);

            return dto;
        }

        public async Task<ResponseMessageDto> TakeTaskAsync(int taskId, int personnelId)
        {
            var task = await _taskRepository
                .GetWhere(t => t.Id == taskId && t.Stage == TaskStage.Open, tracking: true)
                .Include(t => t.TaskAnalysis)
                .FirstOrDefaultAsync();

            if (task == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Task bulunamadı veya artık analiz için uygun değil."
                };

            if (task.TaskAnalysis != null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Bu task zaten bir analist tarafından alınmış."
                };

            var analysis = new TaskAnalysis
            {
                TaskId = taskId,
                AnalystPersonnelId = personnelId,
                Status = AnalysisStatus.InProgress,
                DifficultyLevel = DifficultyLevel.Medium,
                Priority = TaskPriority.Low
            };
            await _analysisRepository.AddAsync(analysis);

            task.Stage = TaskStage.InAnalysis;
            _taskRepository.Update(task);

            var history = new AssignmentHistory
            {
                TaskId = taskId,
                PersonnelId = personnelId,
                DifficultyLevel = null,
                StageType = StageType.Analysis,
                AssignmentScore = 0,
                IsConsecutiveBlocked = false,
                IsReassignment = false,
                SelectionReason = "Analist tarafından üstlenildi",
                ReassignmentReason = null
            };
            await _historyRepository.AddAsync(history);

            await _taskRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task analiz için üstünüze alındı."
            };
        }

        public async Task<ResponseMessageDto> UpdateAnalysisAsync(
            AnalysisUpdateDto dto,
            int personnelId)
        {
            var analysis = await _analysisRepository
                .GetWhere(a => a.Id == dto.AnalysisId && a.AnalystPersonnelId == personnelId,
                          tracking: true)
                .Include(a => a.Task)
                .Include(a => a.TaskDevelopment)
                .FirstOrDefaultAsync();

            if (analysis == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Analiz kaydı bulunamadı."
                };

            if (analysis.TaskDevelopment != null &&
                analysis.TaskDevelopment.Status == DevelopmentStatus.InProgress)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Geliştirme devam ederken analiz güncellenemez."
                };

            analysis.DifficultyLevel = dto.DifficultyLevel;
            analysis.Priority = dto.Priority;
            analysis.AnalystNotes = dto.AnalystNotes;
            analysis.RequirementsDetail = dto.RequirementsDetail;

            _analysisRepository.Update(analysis);
            await _analysisRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Analiz notu güncellendi."
            };
        }

        public async Task<ResponseMessageDto> RejectTaskAsync(
            AnalysisRejectDto dto,
            int personnelId)
        {
            var analysis = await _analysisRepository
                .GetWhere(a => a.Id == dto.AnalysisId && a.AnalystPersonnelId == personnelId,
                          tracking: true)
                .Include(a => a.Task)
                .FirstOrDefaultAsync();

            if (analysis == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Analiz kaydı bulunamadı."
                };

            if (analysis.Status != AnalysisStatus.InProgress)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Yalnızca inceleme aşamasındaki task'lar reddedilebilir."
                };

            analysis.Status = AnalysisStatus.Rejected;
            analysis.RejectionReason = dto.RejectionReason;
            _analysisRepository.Update(analysis);

            analysis.Task.Stage = TaskStage.Rejected;
            _taskRepository.Update(analysis.Task);

            await _analysisRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Task reddedildi."
            };
        }

        public async Task<ResponseMessageDto> ApproveTaskAsync(
            AnalysisApproveDto dto,
            int personnelId)
        {
            var analysis = await _analysisRepository
                .GetWhere(a => a.Id == dto.AnalysisId && a.AnalystPersonnelId == personnelId,
                          tracking: true)
                .Include(a => a.Task)
                .FirstOrDefaultAsync();

            if (analysis == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Analiz kaydı bulunamadı."
                };

            if (analysis.Status != AnalysisStatus.InProgress)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Yalnızca inceleme aşamasındaki task'lar geliştirmeye alınabilir."
                };

            analysis.DifficultyLevel = dto.DifficultyLevel;
            analysis.Priority = dto.Priority;
            analysis.AnalystNotes = dto.AnalystNotes;
            analysis.RequirementsDetail = dto.RequirementsDetail;
            analysis.Status = AnalysisStatus.Approved;
            _analysisRepository.Update(analysis);

            var (developer, isConsecutiveBlocked, score) =
                await SelectDeveloperAsync(dto.DifficultyLevel);

            if (developer == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Uygun developer bulunamadı. Lütfen daha sonra tekrar deneyin."
                };

            var development = new TaskDevelopment
            {
                TaskAnalysisId = analysis.Id,
                DeveloperPersonnelId = developer.Id,
                Status = DevelopmentStatus.InProgress
            };
            await _developmentRepository.AddAsync(development);

            analysis.Task.Stage = TaskStage.InDevelopment;
            _taskRepository.Update(analysis.Task);

            var history = new AssignmentHistory
            {
                TaskId = analysis.TaskId,
                PersonnelId = developer.Id,
                DifficultyLevel = dto.DifficultyLevel,
                StageType = StageType.Development,
                AssignmentScore = score,
                IsConsecutiveBlocked = isConsecutiveBlocked,
                IsReassignment = false,
                SelectionReason = "Sistem tarafından adil dağılım algoritmasıyla seçildi",
                ReassignmentReason = null
            };
            await _historyRepository.AddAsync(history);

            await _analysisRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = $"Task geliştirmeye alındı. Atanan developer: {developer.FirstName} {developer.LastName}"
            };
        }

        private async Task<(Personnel Developer, bool IsConsecutiveBlocked, int Score)>
    SelectDeveloperAsync(DifficultyLevel difficultyLevel)
        {
            var developers = await _personnelRepository
                .GetWhere(p =>
                    p.User.RoleId == (int)UserRole.Developer &&
                    p.Status == PersonnelStatus.Active)
                .Include(p => p.User)
                .ToListAsync();

            if (!developers.Any()) return (null, false, 0);

            var activeDevelopments = await _developmentRepository
                .GetWhere(d =>
                    d.Status == DevelopmentStatus.InProgress ||
                    d.Status == DevelopmentStatus.OnHold)
                .ToListAsync();

            var historySameLevel = await _historyRepository
                .GetWhere(h =>
                    h.DifficultyLevel == difficultyLevel &&
                    h.StageType == StageType.Development)
                .ToListAsync();

            var lastAssignments = await _historyRepository
                .GetWhere(h => h.StageType == StageType.Development)
                .GroupBy(h => h.PersonnelId)
                .Select(g => new
                {
                    PersonnelId = g.Key,
                    LastDifficulty = g
                        .OrderByDescending(h => h.CreatedAt)
                        .Select(h => h.DifficultyLevel)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var scored = developers.Select(dev =>
            {
                var activeCount = activeDevelopments.Count(d => d.DeveloperPersonnelId == dev.Id);

                var sameLevelCount = historySameLevel.Count(h => h.PersonnelId == dev.Id);

                var lastDiff = lastAssignments.FirstOrDefault(x => x.PersonnelId == dev.Id)?.LastDifficulty;
                var isConsecutiveBlocked = lastDiff == difficultyLevel;

                return new
                {
                    Developer = dev,
                    ActiveCount = activeCount,
                    SameLevelCount = sameLevelCount,
                    IsConsecutiveBlocked = isConsecutiveBlocked
                };
            }).ToList();

            var available = scored
                .Where(x => !x.IsConsecutiveBlocked)
                .OrderBy(x => x.ActiveCount)
                .ThenBy(x => x.SameLevelCount)
                .ToList();

            if (available.Any())
            {
                var selected = available.First();
                return (selected.Developer, false, selected.ActiveCount + selected.SameLevelCount);
            }

            var fallback = scored
                .OrderBy(x => x.ActiveCount)
                .ThenBy(x => x.SameLevelCount)
                .First();

            return (fallback.Developer, true, fallback.ActiveCount + fallback.SameLevelCount);
        }
    }
}