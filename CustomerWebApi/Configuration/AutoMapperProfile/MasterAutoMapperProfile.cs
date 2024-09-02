using AutoMapper;
using CustomerGeneral.Utils;
using CustomerServiceContract.ServiceObjects.Master;
using CustomerWebApi.DataTransferObject.Master.CmrDto;
using CustomerWebApi.DataTransferObject.Master.CustomerDto;

namespace CustomerWebApi.Configuration.AutoMapperProfile
{
    public sealed class MasterAutoMapperProfile : Profile
    {
        public MasterAutoMapperProfile()
        {
            // Mapping configuration for CMR
            this.MapForCmr();
        }

        private void MapForCmr()
        {
            //Insert
           
            this.CreateMap<CreateCmrDataRequestDto, CmrDataServiceObject>()
            .ForMember(dest => dest.cmrAddressListData,
                       opt => opt.MapFrom(src => GeneralUtils.JsonDataFromList<CreateCmrAddressRequestDto>(src.cmrAddressListRequestDto)))
            .AfterMap((src, dest) =>
            {
                if (src.cmrRequestDto != null)
                {
                    dest.cmrServiceObject ??= new CmrServiceObject(); // Initialize if null

                    if (!string.IsNullOrEmpty(src.cmrRequestDto.VisitedDate))
                    {
                        dest.cmrServiceObject.VisitedDate = GeneralUtils.ToDateTime(src.cmrRequestDto.VisitedDate);
                    }

                    
                    dest.cmrServiceObject.Name = src.cmrRequestDto.Name;
                    dest.cmrServiceObject.Email = src.cmrRequestDto.Email;
                    dest.cmrServiceObject.MobileNo = src.cmrRequestDto.MobileNo;
                }
            });


            this.CreateMap<CmrDataServiceObject, CreateCmrDataResponseDto>()                    
                    .ForMember(dest => dest.cmrAddressListResponseDto,
                               opt => opt.MapFrom(src => GeneralUtils.JsonDataToList<CreateCmrAddressResponseDto>(src.cmrAddressListData)))

                    .AfterMap((src, dest) =>
                    {
                        if (src.cmrServiceObject != null)
                        {
                            dest.cmrResponseDto ??= new CreateCmrResponseDto(); // Initialize if null

                            dest.cmrResponseDto.Id = src.cmrServiceObject.Id;
                            dest.cmrResponseDto.CustomerCode = src.cmrServiceObject.CustomerCode;
                            dest.cmrResponseDto.Name = src.cmrServiceObject.Name;
                            dest.cmrResponseDto.Email = src.cmrServiceObject.Email;
                            dest.cmrResponseDto.MobileNo = src.cmrServiceObject.MobileNo;

                            // If you need to map VisitedDate, ensure the format method is correct
                            dest.cmrResponseDto.VisitedDate = GeneralUtils.FormatDate(src.cmrServiceObject.VisitedDate);
                        }
                    });
            //update
            this.CreateMap<UpdateCmrDataRequestDto, CmrDataServiceObject>()
            .ForMember(dest => dest.cmrAddressListData,
                       opt => opt.MapFrom(src => GeneralUtils.JsonDataFromList<UpdateCmrAddressRequestDto>(src.cmrAddressListRequestDto)))
            .AfterMap((src, dest) =>
            {
                if (src.cmrRequestDto != null)
                {
                    dest.cmrServiceObject ??= new CmrServiceObject(); // Initialize if null

                    if (!string.IsNullOrEmpty(src.cmrRequestDto.VisitedDate))
                    {
                        dest.cmrServiceObject.VisitedDate = GeneralUtils.ToDateTime(src.cmrRequestDto.VisitedDate);
                    }

                    dest.cmrServiceObject.Id = src.cmrRequestDto.Id;
                    dest.cmrServiceObject.Name = src.cmrRequestDto.Name;
                    dest.cmrServiceObject.Email = src.cmrRequestDto.Email;
                    dest.cmrServiceObject.MobileNo = src.cmrRequestDto.MobileNo;
                }
            });


            this.CreateMap<CmrDataServiceObject, UpdateCmrDataResponseDto>()
                    .ForMember(dest => dest.cmrAddressListResponseDto,
                               opt => opt.MapFrom(src => GeneralUtils.JsonDataToList<UpdateCmrAddressResponseDto>(src.cmrAddressListData)))

                    .AfterMap((src, dest) =>
                    {
                        if (src.cmrServiceObject != null)
                        {
                            dest.cmrResponseDto ??= new UpdateCmrResponseDto(); // Initialize if null

                            dest.cmrResponseDto.Id=src.cmrServiceObject.Id;
                            dest.cmrResponseDto.CustomerCode=src.cmrServiceObject.CustomerCode; 
                            dest.cmrResponseDto.Name = src.cmrServiceObject.Name;
                            dest.cmrResponseDto.Email = src.cmrServiceObject.Email;
                            dest.cmrResponseDto.MobileNo = src.cmrServiceObject.MobileNo;

                            // If you need to map VisitedDate, ensure the format method is correct
                            dest.cmrResponseDto.VisitedDate = GeneralUtils.FormatDate(src.cmrServiceObject.VisitedDate);
                        }
                    });
                    this.CreateMap<CmrServiceObject, CreateCmrResponseDto>()
                    .ForMember(dest => dest.VisitedDate,
                    opt => opt.MapFrom(src => GeneralUtils.FormatDate(src.VisitedDate,"view")));

            this.CreateMap<CmrDataServiceObject, FindCmrDataResponseDto>()
            .ForMember(dest => dest.cmrAddressListResponseDto,
                      opt => opt.MapFrom(src => GeneralUtils.JsonDataToList<FindCmrAddressResponseDto>(src.cmrAddressListData)))

           .AfterMap((src, dest) =>
           {
               if (src.cmrServiceObject != null)
               {
                   dest.cmrResponseDto ??= new FindCmrResponseDto(); // Initialize if null

                   dest.cmrResponseDto.Id = src.cmrServiceObject.Id;
                   dest.cmrResponseDto.CustomerCode = src.cmrServiceObject.CustomerCode;
                   dest.cmrResponseDto.Name = src.cmrServiceObject.Name;
                   dest.cmrResponseDto.Email = src.cmrServiceObject.Email;
                   dest.cmrResponseDto.MobileNo = src.cmrServiceObject.MobileNo;

                   // If you need to map VisitedDate, ensure the format method is correct
                   dest.cmrResponseDto.VisitedDate = GeneralUtils.FormatDate(src.cmrServiceObject.VisitedDate);
               }
           });
            


        }
    }
}
