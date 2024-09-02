using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRepositoryContract.ResultEntities.Master
{
    public class CmrDataResult
    {
        public CmrResult cmrResult { get; set; }
        public string cmrAddressListData { get; set; }
        public string cmrCustomerData { get; set; }

        public List<CmrAddressResult> cmrAddressResultData { get; set; }
    }
    public class CmrAddressResult
    {
        public string CustomerId { get; set; }  
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GeoLocation { get; set; }
    }

}
