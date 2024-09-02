using CustomerWebApi.DataTransferObject.Master.CustomerDto;

namespace CustomerWebApi.DataTransferObject.Master.CmrDto
{
    public class CreateCmrDataResponseDto
    {
        public  CreateCmrResponseDto cmrResponseDto { get; set; }
        public List<CreateCmrAddressResponseDto> cmrAddressListResponseDto { get; set; }
    }
    public class CreateCmrAddressResponseDto
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }
    }
}
