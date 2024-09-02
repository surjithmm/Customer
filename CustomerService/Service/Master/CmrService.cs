using System.Xml.Linq;
using AutoMapper;
using CustomerRepositoryContract.IRepositories.Master;
using CustomerRepositoryContract.ResultEntities.Master;
using CustomerServiceContract.IService.Master;
using CustomerServiceContract.ServiceObjects.Master;

namespace CustomerService.Service.Master;

public class CmrService : ICmrService
{
    private readonly ICmrRepository _cmrRepository;
    private readonly IMapper _mapper;
    public CmrService(ICmrRepository cmrRepository,IMapper mapper)
    {
        _cmrRepository = cmrRepository;
        _mapper = mapper;
    }

    public async Task<List<CmrServiceObject>> CheckDB()
    {
        return _mapper.Map<List<CmrServiceObject>>(await _cmrRepository.CheckDB());
    }

    public async Task<CmrDataServiceObject> DeleteAsync(int Id)
    {
        return _mapper.Map<CmrDataServiceObject>(await _cmrRepository.DeleteAsync(Id));
    }

    public async Task<CmrDataServiceObject> GetAsync(int Id)
    {

        return _mapper.Map<CmrDataServiceObject>(await _cmrRepository.GetAsync(Id));
    }

    public async Task<List<CmrServiceObject>> GetListAsync(string name = null, string email = null, string mobileNo = null, DateTime? visitedDateFrom = null, DateTime? visitedDateTo = null)
    {
        return _mapper.Map<List<CmrServiceObject>>(await _cmrRepository.GetListAsync(name,email,mobileNo, visitedDateFrom, visitedDateTo));
     
    }

    public async Task<CmrDataServiceObject> PostAsync(CmrDataServiceObject cmrDataServiceObject)
    {
  
        var serviceRequest = _mapper.Map<CmrDataResult>(cmrDataServiceObject);
        var s = await _cmrRepository.PostAsync(serviceRequest);       
        var g = _mapper.Map<CmrDataServiceObject>(s);
        return g;
    }
    public async Task<CmrDataServiceObject> PutAsync(CmrDataServiceObject cmrDataServiceObject)
    {
        var serviceRequest = _mapper.Map<CmrDataResult>(cmrDataServiceObject);
        var s = await _cmrRepository.PutAsync(serviceRequest);
        var g = _mapper.Map<CmrDataServiceObject>(s);
        return g;
    }
}
