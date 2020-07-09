using AutoMapper;
using Elect.Mapper.AutoMapper.IMappingExpressionUtils;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GoblinIdentityUserModel, UpdateProfileModel>().IgnoreAllNonExisting();
            
            CreateMap<UpdateProfileModel, GoblinIdentityUpdateProfileModel>().IgnoreAllNonExisting();
            
            CreateMap<GoblinIdentityUpdateProfileModel, UpdateProfileModel>().IgnoreAllNonExisting();
        }
    }
}