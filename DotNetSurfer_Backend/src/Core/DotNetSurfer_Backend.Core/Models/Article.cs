using System;
using System.Collections.Generic;

namespace DotNetSurfer_Backend.Core.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; } = "Free";

        public byte[] Picture { get; set; }

        public string PictureMimeType { get; set; }

        public string PictureUrl { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int ReadCount { get; set; }

        public bool ShowFlag { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}