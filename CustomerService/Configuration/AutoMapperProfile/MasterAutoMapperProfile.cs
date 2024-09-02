using AutoMapper;
using CustomerRepositoryContract.ResultEntities.Master;
using CustomerServiceContract.ServiceObjects.Master;

namespace CustomerService.Configuration.AutoMapperProfie
{
    public sealed class MasterAutoMapperProfile:Profile
    {
        public MasterAutoMapperProfile() 
        {
            this.MapForCmr();
        }
        public void MapForCmr() 
        {
            this.CreateMap<CmrDataServiceObject,CmrDataResult>()
                .ForMember(dest => dest.cmrResult,
                               opt => opt.MapFrom(src => src.cmrServiceObject));
            this.CreateMap<CmrDataResult,CmrDataServiceObject>()
                .ForMember(dest => dest.cmrServiceObject,
                               opt => opt.MapFrom(src => src.cmrResult));
            this.CreateMap<CmrServiceObject,CmrResult>();
            this.CreateMap<CmrResult,CmrServiceObject>();
        }
    }
}
