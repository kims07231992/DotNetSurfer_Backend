using System;

namespace DotNetSurfer_Backend.Core.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }

        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public bool ShowFlag { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}
