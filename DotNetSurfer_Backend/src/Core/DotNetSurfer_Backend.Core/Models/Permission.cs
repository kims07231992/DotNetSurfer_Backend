using System.Collections.Generic;

namespace DotNetSurfer_Backend.Core.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public PermissionType PermissionType { get; set; }

        public ICollection<User> Users { get; set; }
    }

    public enum PermissionType
    {
        Admin = 0,
        User = 1
    };
}
