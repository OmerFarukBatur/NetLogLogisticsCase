using Core.DTOs;
using Core.DTOs.AuthDtos;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;

        public AuthService(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(ResponseMessageDto Response, AuthenticationDto? Data)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository
                .GetWhere(x => x.Email == loginDto.Email, tracking: false)
                .Include(x => x.Role)
                .Include(x => x.Personnel)
                .FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return (new ResponseMessageDto
                {
                    Status = false,
                    Message = "Geçersiz e-posta veya şifre."
                }, null);

            if (!user.IsActive)
                return (new ResponseMessageDto
                {
                    Status = false,
                    Message = "Kullanıcı hesabı pasif durumda. Lütfen yönetici ile iletişime geçin."
                }, null);

            if (user.Personnel == null && user.RoleId != (int)UserRole.Admin)
                return (new ResponseMessageDto
                {
                    Status = false,
                    Message = "Kullanıcıya ait personel kaydı bulunamadı."
                }, null);

            var firstName = user.Personnel?.FirstName ?? string.Empty;
            var lastName = user.Personnel?.LastName ?? string.Empty;
            var personnelId = user.Personnel?.Id ?? 0;

            var welcomeName = string.IsNullOrEmpty(firstName)
                ? user.Email
                : $"{firstName} {lastName}";

            var authDto = new AuthenticationDto
            {
                Id = user.Id,
                PersonnelId = personnelId,
                Email = user.Email,
                FirstName = firstName,
                LastName = lastName,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                IsActive = user.IsActive
            };

            return (new ResponseMessageDto
            {
                Status = true,
                Message = $"Hoş geldin, {welcomeName}!"
            }, authDto);
        }

        public async Task<ResponseMessageDto> PasswordResetAsync(PasswordResetDto passwordResetDto)
        {
            var user = await _userRepository
                .GetWhere(x => x.Email == passwordResetDto.Email, tracking: true)
                .FirstOrDefaultAsync();

            if (user == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Bu email ile kayıtlı kullanıcı bulunamadı."
                };

            if (!user.IsActive)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Hesabınız pasif durumda. Lütfen yönetici ile iletişime geçin."
                };

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordResetDto.Password);
            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Şifreniz başarıyla güncellendi."
            };
        }
    }
}