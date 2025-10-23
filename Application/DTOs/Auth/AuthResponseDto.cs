using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }


        public AuthResponseDto(string email, string name, string token, string refreshToken, DateTime expiresAt, string role)
        {
            Email = email;
            Name = name;
            Token = token;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
            Role = role;
        }
    }
}
