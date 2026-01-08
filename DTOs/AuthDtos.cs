namespace BlueMediCore.DTOs
{
    public class AuthDtos
    {
        public class RegisterDto
        {
            public required string Email { get; set; }
            public required string UserName { get; set; }
            public required string Password { get; set; }
        }

        public class LoginDto
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class AuthResponseDto
        {
            public string Token { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}
