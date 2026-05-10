using Core.DTOs;
using Core.DTOs.DeveloperDtos;
using Core.Helpers;

namespace Core.Interfaces.IServices
{
    public interface IDeveloperService
    {
        Task<PaginatedList<DeveloperTaskListDto>> GetMyTasksAsync(
            int personnelId,
            string search = null,
            int? status = null,
            int pageIndex = 1,
            int pageSize = 15);

        Task<DeveloperTaskDetailDto> GetTaskDetailAsync(int developmentId, int personnelId);
        Task<ResponseMessageDto> UpdateStatusAsync(DevelopmentUpdateDto dto, int personnelId);
        Task<ResponseMessageDto> CompleteTaskAsync(DevelopmentCompleteDto dto, int personnelId);
        Task<ResponseMessageDto> CancelTaskAsync(DevelopmentCancelDto dto, int personnelId);
    }
}
