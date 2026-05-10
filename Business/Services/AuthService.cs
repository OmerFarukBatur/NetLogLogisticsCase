using Core.DTOs;
using Core.DTOs.AuthDtos;
using Core.Entities;
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

        public async Task<(ResponseMessageDto Response, AuthenticationDto? Data)> LoginAsync(
            LoginDto loginDto)
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

            if (user.Personnel == null)
                return (new ResponseMessageDto
                {
                    Status = false,
                    Message = "Kullanıcıya ait personel kaydı bulunamadı."
                }, null);

            var authDto = new AuthenticationDto
            {
                Id = user.Id,
                PersonnelId = user.Personnel.Id,
                Email = user.Email,
                FirstName = user.Personnel.FirstName,
                LastName = user.Personnel.LastName,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                IsActive = user.IsActive
            };

            return (new ResponseMessageDto
            {
                Status = true,
                Message = $"Hoş geldin, {user.Personnel.FirstName} {user.Personnel.LastName}!"
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