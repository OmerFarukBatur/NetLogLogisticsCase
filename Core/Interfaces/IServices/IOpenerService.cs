using Core.DTOs;
using Core.DTOs.OpenerDtos;
using Core.Helpers;

namespace Core.Interfaces.IServices
{
    public interface IOpenerService
    {
        Task<PaginatedList<TaskListDto>> GetMyTasksAsync(
            int personnelId,
            string search = null,
            int? stage = null,
            int pageIndex = 1,
            int pageSize = 25);

        Task<TaskDetailDto> GetTaskDetailAsync(int taskId, int personnelId);
        Task<ResponseMessageDto> CreateTaskAsync(TaskCreateDto dto, int personnelId);
        Task<ResponseMessageDto> UpdateTaskAsync(TaskUpdateDto dto, int personnelId);
        Task<ResponseMessageDto> DeleteTaskAsync(int taskId, int personnelId);
    }
}
