namespace CustomerWebApi.DataTransferObject.Master.CustomerDto
{
    public class CreateCmrResponseDto
    {
        public int Id { get; set; } 
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string VisitedDate { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }


    }
}
