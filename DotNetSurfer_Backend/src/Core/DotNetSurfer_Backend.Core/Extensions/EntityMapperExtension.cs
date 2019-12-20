using DotNetSurfer_Backend.Core.Models;
using System.Linq;

namespace DotNetSurfer_Backend.Core.Extensions
{
    public static class EntityMapperExtension
    {
        public static Header MapToDomainHeader(this Topic entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Header
            {
                Id = entity.TopicId,
                Title = entity.Title,
                SideNodes = entity.Articles?.Select(a => new Header.SideNode
                {
                    Id = a.ArticleId,
                    Title = a.Title
                })
            };
        }
    }
}
