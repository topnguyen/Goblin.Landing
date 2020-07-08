using AutoMapper;
using Elect.Mapper.AutoMapper.IMappingExpressionUtils;
using Goblin.Identity.Share.Models.UserModels;

namespace Goblin.Landing.Core.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GoblinIdentityUserModel, GoblinIdentityUpdateProfileModel>().IgnoreAllNonExisting();
        }
    }
}