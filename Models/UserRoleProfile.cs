using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskSystem.Models
{
    public class UserRoleProfile :UserActivity
    {
        public int Id { get; set; }

        [DisplayName("System Task")]
        public int TaskId { get; set; }
        public SystemTask Task { get; set; }

        [DisplayName("System Role")]
        public string? RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}
