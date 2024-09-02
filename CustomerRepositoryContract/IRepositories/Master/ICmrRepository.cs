using CustomerRepositoryContract.ResultEntities.Master;

namespace CustomerRepositoryContract.IRepositories.Master;

public interface ICmrRepository
{
     Task<CmrDataResult> GetAsync(int Id);
     Task<List<CmrResult>> GetListAsync(string name = null, string email = null, string mobileNo = null, DateTime? visitedDateFrom = null, DateTime? visitedDateTo = null);
     Task<CmrDataResult> PostAsync(CmrDataResult cmrDataResult);
     Task<CmrDataResult> PutAsync(CmrDataResult cmrDataResult);
     Task<CmrDataResult> DeleteAsync(int Id);
     Task<List<CmrResult>> CheckDB();
}
