using System;
using System.Collections.Generic;

namespace DotNetSurfer_Backend.Core.Models
{
    public class Topic
    {
        public int TopicId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public string PictureMimeType { get; set; }

        public string PictureUrl { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public bool ShowFlag { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}