using AutoMapper;
using Elect.Mapper.AutoMapper.IMappingExpressionUtils;
using Goblin.Landing.Contract.Repository.Models;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Mapper
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<CreateSampleModel, SampleEntity>()
                .IgnoreAllNonExisting();
            
            CreateMap<SampleEntity, SampleModel>()
                .IgnoreAllNonExisting();
        }
    }
}