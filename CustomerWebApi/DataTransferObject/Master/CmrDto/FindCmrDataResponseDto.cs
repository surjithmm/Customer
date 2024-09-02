namespace CustomerWebApi.DataTransferObject.Master.CmrDto
{
    public class FindCmrDataResponseDto
    {
        public FindCmrResponseDto cmrResponseDto { get; set; }
        public List<FindCmrAddressResponseDto> cmrAddressListResponseDto { get; set; }
    }
    public class FindCmrResponseDto
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string VisitedDate { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
    public class FindCmrAddressResponseDto
    {
        public int CustomerId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }
    }
}
