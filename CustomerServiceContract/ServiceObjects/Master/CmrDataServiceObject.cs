using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceContract.ServiceObjects.Master
{
    public class CmrDataServiceObject
    {
        public CmrServiceObject cmrServiceObject { get; set; } 

        public string cmrCustomerData { get; set; } 
        public string cmrAddressListData { get; set; }
        public List<CmrAddressObject> cmrAddressListObject {  get; set; } 
    }
    public class CmrAddressObject
    {
        public string CustomerId { get; set; }  
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }

    }


}
