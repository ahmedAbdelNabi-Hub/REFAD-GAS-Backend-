using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class AdminDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "super_admin";
        public string? PasswordHash { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
