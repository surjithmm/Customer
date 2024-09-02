using CustomerWebApi.DataTransferObject.Master.CustomerDto;

namespace CustomerWebApi.DataTransferObject.Master.CmrDto
{
    public class UpdateCmrDataRequestDto
    {
        public UpdateCmrRequestDto cmrRequestDto { get; set; }
        public List<UpdateCmrAddressRequestDto> cmrAddressListRequestDto { get; set; }
    }
    public class UpdateCmrAddressRequestDto
    {
        public int CustomerId { get; set; } 
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }
    }
}
