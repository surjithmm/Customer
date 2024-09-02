using CustomerWebApi.DataTransferObject.Master.CustomerDto;

namespace CustomerWebApi.DataTransferObject.Master.CmrDto
{
    public class CreateCmrDataRequestDto
    {
        public  CreateCmrRequestDto cmrRequestDto { get; set; }
        public List<CreateCmrAddressRequestDto> cmrAddressListRequestDto { get; set; }
    }
    public class CreateCmrAddressRequestDto
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }    
        public string AddressLine2 { get; set; } 
        public string GeoLocation { get; set; } 
    }
}
