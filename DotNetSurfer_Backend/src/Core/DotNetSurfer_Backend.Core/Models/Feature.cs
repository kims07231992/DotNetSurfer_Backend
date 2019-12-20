
namespace DotNetSurfer_Backend.Core.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string GithubUrl { get; set; }

        public string DocumentUrl { get; set; }

        public string GuideUrl { get; set; }

        public FeatureType FeatureType { get; set; }

        public bool ShowFlag { get; set; }
    }

    public enum FeatureType
    {
        Backend = 0,
        Frontend = 1
    }
}
