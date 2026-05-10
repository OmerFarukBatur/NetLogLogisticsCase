using Core.DTOs;
using Core.DTOs.AuthDtos;

namespace Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<(ResponseMessageDto Response, AuthenticationDto? Data)> LoginAsync(LoginDto loginDto);
        Task<ResponseMessageDto> PasswordResetAsync(PasswordResetDto passwordResetDto);
    }
}
