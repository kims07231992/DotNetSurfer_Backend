
namespace DotNetSurfer_Backend.Core.Caches
{
    public static class CacheKeys
    {
        public static string ArticlesByPage { get { return "_ArticlesByPage"; } }
        public static string TopArticles { get { return "_TopArticles"; } }
        public static string FrontendFeatures { get { return "_FrontendFeatures"; } }
        public static string BackendFeatures { get { return "_BackendFeatures"; } }
        public static string HeaderMenus { get { return "_HeaderMenus"; } }
        public static string Statuses { get { return "_Statuses"; } }
    }
}
