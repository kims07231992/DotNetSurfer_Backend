
namespace DotNetSurfer_Backend.Core.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}