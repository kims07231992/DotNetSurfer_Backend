using System;
using System.Collections.Generic;

namespace DotNetSurfer_Backend.Core.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Introduction { get; set; }

        public DateTime? Birthdate { get; set; }

        public byte[] Picture { get; set; }

        public string PictureMimeType { get; set; }

        public string PictureUrl { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Announcement> Announcements { get; set; }
    }
}