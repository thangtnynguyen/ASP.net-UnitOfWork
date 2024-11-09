using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.News;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class NewsMapper : Profile
    {
        public NewsMapper()
        {
            CreateMap<News, CreateNewsRequest>();
            CreateMap<CreateNewsRequest, News>();
            CreateMap<EditNewsRequest, News>();
            CreateMap<News, EditNewsRequest>();
            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();
        }
    }

}
