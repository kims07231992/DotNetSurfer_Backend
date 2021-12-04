using DotNetSurfer_Backend.Core.Interfaces.Caches;
using DotNetSurfer_Backend.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Caches
{
    public class CacheDataProvider : ICacheDataProvider
    {
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheTime;

        public CacheDataProvider(IConfiguration configuration)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cacheTime = TimeSpan.FromHours(configuration.GetValue<int>("Cache:ExpireHours"));
        }

        #region Articles
        public async Task<IEnumerable<Article>> TryGetArticlesByPageAsync(int pageId)
        {
            Dictionary<int, IEnumerable<Article>> cacheArticleMap = null;
            IEnumerable <Article> cacheArticlesByPage = null;

            _cache.TryGetValue(CacheKeys.ArticlesByPage, out cacheArticleMap);
            if (cacheArticleMap?.ContainsKey(pageId) ?? false)
                cacheArticlesByPage = cacheArticleMap[pageId];

            return await Task.FromResult(cacheArticlesByPage);
        }

        public Task SetArticlesByPageAsync(IEnumerable<Article> articlesByPage, int pageId)
        {
            Dictionary<int, IEnumerable<Article>> cacheArticleMap = null;
            if (!_cache.TryGetValue(CacheKeys.ArticlesByPage, out cacheArticleMap))
            {
                cacheArticleMap = new Dictionary<int, IEnumerable<Article>>();
            }
            
            if (!cacheArticleMap.TryAdd(pageId, articlesByPage))
            {
                cacheArticleMap[pageId] = articlesByPage;
            }

            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheTime);
            _cache.Set(CacheKeys.ArticlesByPage, cacheArticleMap, cacheOptions);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Article>> TryGetTopArticlesAsync()
        {
            IEnumerable<Article> cacheTopArticles = null;
            _cache.TryGetValue(CacheKeys.TopArticles, out cacheTopArticles);

            return await Task.FromResult(cacheTopArticles);
        }

        public Task SetTopArticlesAsync(IEnumerable<Article> topArticles)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheTime);
            _cache.Set(CacheKeys.TopArticles, topArticles, cacheOptions);

            return Task.CompletedTask;
        }

        public Task ClearArticlesByPageAsync()
        {
            _cache.Remove(CacheKeys.ArticlesByPage);

            return Task.CompletedTask;
        }
        #endregion

        #region Features
        public async Task<IEnumerable<Feature>> TryGetFeaturesAsync(FeatureType featureType)
        {
            var cacheKey = featureType == FeatureType.Frontend
                ? CacheKeys.FrontendFeatures
                : CacheKeys.BackendFeatures;
            IEnumerable<Feature> cacheFeatures;
            _cache.TryGetValue(cacheKey, out cacheFeatures);

            return await Task.FromResult(cacheFeatures);
        }

        public Task SetFeaturesAsync(IEnumerable<Feature> features, FeatureType featureType)
        {
            var cacheKey = featureType == FeatureType.Frontend
                ? CacheKeys.FrontendFeatures
                : CacheKeys.BackendFeatures;

            var cacheOptions = new MemoryCacheEntryOptions()
                         .SetSlidingExpiration(_cacheTime);
            _cache.Set(cacheKey, features, cacheOptions);

            return Task.CompletedTask;
        }
        #endregion

        #region HeaderMenus
        public async Task<IEnumerable<Header>> TryGetHeaderMenusAsync()
        {
            IEnumerable<Header> cacheHeaderMenus;
            _cache.TryGetValue(CacheKeys.HeaderMenus, out cacheHeaderMenus);

            return await Task.FromResult(cacheHeaderMenus);
        }

        public Task SetHeaderMenusAsync(IEnumerable<Header> headerMenus)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(_cacheTime);
            _cache.Set(CacheKeys.HeaderMenus, headerMenus, cacheOptions);

            return Task.CompletedTask;
        }
        #endregion

        #region Statuses
        public async Task<IEnumerable<Status>> TryGetStatusesAsync()
        {
            IEnumerable<Status> cacheStatuses;
            _cache.TryGetValue(CacheKeys.Statuses, out cacheStatuses);

            return await Task.FromResult(cacheStatuses);
        }

        public Task SetStatusesAsync(IEnumerable<Status> statuses)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(_cacheTime);
            _cache.Set(CacheKeys.Statuses, statuses, cacheOptions);

            return Task.CompletedTask;
        }
        #endregion
    }
}
