namespace Core.DTOs.AuthDtos
{
    public class PasswordResetDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
