using Core.DTOs;
using Core.DTOs.AnalystDtos;
using Core.Helpers;

namespace Core.Interfaces.IServices
{
    public interface IAnalystService
    {
        Task<PaginatedList<TaskPendingListDto>> GetPendingTasksAsync(
            string search = null,
            int pageIndex = 1,
            int pageSize = 15);

        Task<PaginatedList<AnalystTaskListDto>> GetMyAnalysesAsync(
            int personnelId,
            string search = null,
            int? status = null,
            int pageIndex = 1,
            int pageSize = 15);

        Task<AnalystTaskDetailDto> GetAnalysisDetailAsync(int analysisId, int personnelId);
        Task<ResponseMessageDto> TakeTaskAsync(int taskId, int personnelId);
        Task<ResponseMessageDto> UpdateAnalysisAsync(AnalysisUpdateDto dto, int personnelId);
        Task<ResponseMessageDto> RejectTaskAsync(AnalysisRejectDto dto, int personnelId);
        Task<ResponseMessageDto> ApproveTaskAsync(AnalysisApproveDto dto, int personnelId);
    }
}
