using Core.DTOs;
using Core.DTOs.AdminDtos;
using Core.Helpers;

namespace Core.Interfaces.IServices
{
    public interface IAdminService
    {
        Task<PaginatedList<UserListDto>> GetUsersAsync(
            string search = null,
            int? roleId = null,
            int pageIndex = 1,
            int pageSize = 25);

        Task<UserDetailDto> GetUserDetailAsync(int userId);

        Task<ResponseMessageDto> CreateUserAsync(UserCreateDto dto, int createdByPersonnelId);

        Task<ResponseMessageDto> UpdateUserAsync(UserUpdateDto dto);

        Task<ResponseMessageDto> DeleteUserAsync(int userId);

        Task<IEnumerable<(int Value, string Name)>> GetRolesAsync();
    }
}
