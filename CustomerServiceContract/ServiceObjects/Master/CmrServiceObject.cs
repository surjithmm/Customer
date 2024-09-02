using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceContract.ServiceObjects.Master
{
    public class CmrServiceObject
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public DateTime VisitedDate { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}
