using CustomerWebApi.DataTransferObject.Master.CustomerDto;

namespace CustomerWebApi.DataTransferObject.Master.CmrDto
{
    public class UpdateCmrDataResponseDto
    {
        public UpdateCmrResponseDto cmrResponseDto { get; set; }
        public List<UpdateCmrAddressResponseDto> cmrAddressListResponseDto { get; set; }
    }
    public class UpdateCmrAddressResponseDto
    {
        public int CustomerId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }
    }
}
