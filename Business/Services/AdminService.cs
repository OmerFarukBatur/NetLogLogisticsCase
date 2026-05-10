using AutoMapper;
using Core.DTOs;
using Core.DTOs.AdminDtos;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Personnel> _personnelRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public AdminService(
            IRepository<User> userRepository,
            IRepository<Personnel> personnelRepository,
            IRepository<Role> roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _personnelRepository = personnelRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserListDto>> GetUsersAsync(
            string search = null,
            int? roleId = null,
            int pageIndex = 1,
            int pageSize = 25)
        {
            var query = _userRepository
                .GetAll()
                .Include(u => u.Role)
                .Include(u => u.Personnel)
                .AsQueryable();

            query = query.Where(u => u.RoleId != (int)UserRole.Admin).Select(x=> x);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(u =>
                    u.Email.ToLower().Contains(search) ||
                    u.Personnel.FirstName.ToLower().Contains(search) ||
                    u.Personnel.LastName.ToLower().Contains(search));
            }

            if (roleId.HasValue)
                query = query.Where(u => u.RoleId == roleId.Value);

            query = query.OrderByDescending(u => u.CreatedAt);

            var paged = await PaginatedList<User>.CreateAsync(query, pageIndex, pageSize);

            var dtos = _mapper.Map<List<UserListDto>>(paged.ToList());
            return new PaginatedList<UserListDto>(dtos, paged.TotalCount, pageIndex, pageSize);
        }

        public async Task<UserDetailDto> GetUserDetailAsync(int userId)
        {
            var user = await _userRepository
                .GetWhere(u => u.Id == userId)
                .Include(u => u.Role)
                .Include(u => u.Personnel)
                .FirstOrDefaultAsync();

            return user == null ? null : _mapper.Map<UserDetailDto>(user);
        }

        public async Task<ResponseMessageDto> CreateUserAsync(
            UserCreateDto dto,
            int createdByPersonnelId)
        {
            var exists = await _userRepository
                .GetWhere(u => u.Email == dto.Email)
                .AnyAsync();

            if (exists)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Bu email adresi zaten kayıtlı."
                };

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
                IsActive = true,
                IsDeleted = false
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            var personnel = new Personnel
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Status = PersonnelStatus.Active,
                IsDeleted = false,
                CreatedByUserId = createdByPersonnelId
            };

            await _personnelRepository.AddAsync(personnel);
            await _personnelRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = $"{dto.FirstName} {dto.LastName} başarıyla eklendi."
            };
        }

        public async Task<ResponseMessageDto> UpdateUserAsync(UserUpdateDto dto)
        {
            var user = await _userRepository
                .GetWhere(u => u.Id == dto.Id)
                .Include(u => u.Personnel)
                .FirstOrDefaultAsync();

            if (user == null)
                return new ResponseMessageDto { Status = false, Message = "Kullanıcı bulunamadı." };

            if (user.Email != dto.Email)
            {
                var emailExists = await _userRepository
                    .GetWhere(u => u.Email == dto.Email && u.Id != dto.Id)
                    .AnyAsync();

                if (emailExists)
                    return new ResponseMessageDto { Status = false, Message = "Bu email adresi başka bir kullanıcıya ait." };
            }

            user.Email = dto.Email;
            user.RoleId = dto.RoleId;
            user.IsActive = dto.IsActive;
            _userRepository.Update(user);

            if (user.Personnel != null)
            {
                user.Personnel.FirstName = dto.FirstName;
                user.Personnel.LastName = dto.LastName;
                user.Personnel.Status = dto.Status;
                _personnelRepository.Update(user.Personnel);
            }

            await _userRepository.SaveAsync();

            return new ResponseMessageDto { Status = true, Message = "Kullanıcı başarıyla güncellendi." };
        }

        public async Task<ResponseMessageDto> DeleteUserAsync(int userId)
        {
            var user = await _userRepository
                .GetWhere(u => u.Id == userId)
                .Include(u => u.Personnel)
                .FirstOrDefaultAsync();

            if (user == null)
                return new ResponseMessageDto
                {
                    Status = false,
                    Message = "Kullanıcı bulunamadı."
                };

            if (user.Personnel != null)
            {
                var hasActiveTask = await _personnelRepository
                    .GetWhere(p => p.Id == user.Personnel.Id)
                    .Include(p => p.TaskAnalyses.Where(a =>
                        a.Status != AnalysisStatus.Approved &&
                        a.Status != AnalysisStatus.Rejected))
                    .Include(p => p.TaskDevelopments.Where(d =>
                        d.Status != DevelopmentStatus.Done &&
                        d.Status != DevelopmentStatus.Cancelled))
                    .AnyAsync(p =>
                        p.TaskAnalyses.Any() ||
                        p.TaskDevelopments.Any());

                if (hasActiveTask)
                    return new ResponseMessageDto
                    {
                        Status = false,
                        Message = "Üzerinde aktif task bulunan kullanıcı silinemez. Önce task'ları devredin."
                    };

                _personnelRepository.Remove(user.Personnel);
            }

            _userRepository.Remove(user);
            await _userRepository.SaveAsync();

            return new ResponseMessageDto
            {
                Status = true,
                Message = "Kullanıcı başarıyla silindi."
            };
        }

        public async Task<IEnumerable<(int Value, string Name)>> GetRolesAsync()
        {
            var roles = await _roleRepository
                .GetAll()
                .ToListAsync();

            return roles.Select(r => (Value: r.Id, Name: r.Name));
        }
    }
}
