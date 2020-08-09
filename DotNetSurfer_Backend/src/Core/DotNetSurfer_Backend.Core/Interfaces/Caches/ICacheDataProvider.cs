using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Caches
{
    public interface ICacheDataProvider
    {
        #region Articles
        Task<IEnumerable<Article>> TryGetArticlesByPageAsync(int pageId);
        Task SetArticlesByPageAsync(IEnumerable<Article> articlesByPage, int pageId);
        Task<IEnumerable<Article>> TryGetTopArticlesAsync();
        Task SetTopArticlesAsync(IEnumerable<Article> topArticles);
        #endregion

        #region Features
        Task<IEnumerable<Feature>> TryGetFeaturesAsync(FeatureType featureType);
        Task SetFeaturesAsync(IEnumerable<Feature> features, FeatureType featureType);
        #endregion

        #region HeaderMenus
        Task<IEnumerable<Header>> TryGetHeaderMenusAsync();
        Task SetHeaderMenusAsync(IEnumerable<Header> headerMenus);
        #endregion

        #region Statuses
        Task<IEnumerable<Status>> TryGetStatusesAsync();
        Task SetStatusesAsync(IEnumerable<Status> statuses);
        #endregion
    }
}
