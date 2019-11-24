using AutoMapper;
using NewsFeedReader.Domain.ApiModels.Request;
using NewsFeedReader.Domain.ApiModels.Response;
using NewsFeedReader.Domain.Models;
using NewsFeedReader.Logic.Models;
using NewsFeedReader.Web.Mappings;
using System;
using System.Collections.Generic;

namespace NewsFeedReader.Web
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping settings
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ApplicationUser, LoggedInUserDto>();

            CreateMap<RegisterRequestDto, ApplicationUser>()
               .ForMember(x => x.LastLoginDate, y => y.MapFrom(z => DateTime.UtcNow))
               .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password));

            CreateMap<List<Feed>, FeedModel>().ConvertUsing<FeedToFeedModelConverter>();
        }
    }
}
