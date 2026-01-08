namespace BlueMediCore.DTOs
{
    public class UserDtos
    {
        public class UserResponseDto
        {
            public int Id { get; set; }
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Role { get; set; }
        }

        public class CreateUserDto
        {
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public string Role { get; set; } = "User"; // Varsayılan rol
        }

        public class UpdateUserDto
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
}