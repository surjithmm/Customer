using CustomerServiceContract.ServiceObjects.Master;

namespace CustomerServiceContract.IService.Master;

public interface ICmrService
{
    public Task<CmrDataServiceObject> GetAsync(int Id);
    public Task<List<CmrServiceObject>> GetListAsync(string name = null, string email = null, string mobileNo = null, DateTime? visitedDateFrom = null, DateTime? visitedDateTo = null);
    public Task<CmrDataServiceObject> PostAsync(CmrDataServiceObject cmrServiceObject);
    public Task<CmrDataServiceObject> PutAsync(CmrDataServiceObject cmrServiceObject);
    public Task<CmrDataServiceObject> DeleteAsync(int Id);
    Task<List<CmrServiceObject>> CheckDB();

}
