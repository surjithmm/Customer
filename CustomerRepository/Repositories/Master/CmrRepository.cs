using CustomerRepositoryContract.IRepositories.Master;
using CustomerRepositoryContract.ResultEntities.Master;
using CustomerGeneral.GeneralErrors;
using Dapper;
using System.Data;
using Newtonsoft.Json;
using System.Data.Common;

namespace CustomerRepository.Repositories.Master;

public sealed class CmrRepository : ICmrRepository
{
    private readonly CustomerDapperDBContext _dbDapperContext;
    public CmrRepository(CustomerDapperDBContext dbDapperContext)
    {
        _dbDapperContext = dbDapperContext;
    }

    public async Task<CmrDataResult> DeleteAsync(int Id)
    {
        var cmrDataResult = new CmrDataResult();
        var parameters = new DynamicParameters();


        parameters.Add("@CustomerId", Id);
        parameters.Add("@DeletedCustomerData", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);
        parameters.Add("@DeletedCustomerAddressListData", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);

        using (var connection = _dbDapperContext.CreateConnection())
        {

            var executed = await connection.ExecuteAsync("SpDeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
            if (executed == 0)
            {
                throw new CustomNoRowAffectedException(ErrorCode.NoRowAffected.Message());
            }
        }


        var customerDataJson = parameters.Get<string>("@DeletedCustomerData");
        var customerAddressListJson = parameters.Get<string>("@DeletedCustomerAddressListData");
        cmrDataResult.cmrResult = JsonConvert.DeserializeObject<List<CmrResult>>(customerDataJson)?.FirstOrDefault();
        cmrDataResult.cmrAddressListData = customerAddressListJson;

        return cmrDataResult;
    }

    public async Task<CmrDataResult> GetAsync(int Id)
    {
        var cmrDataResult = new CmrDataResult();
        var parameters = new DynamicParameters();


        parameters.Add("@CustomerId",Id);
        parameters.Add("@CustomerDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);
        parameters.Add("@CustomerAddressListDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);

        using (var connection = _dbDapperContext.CreateConnection())
        {

            var executed = await connection.ExecuteAsync("SpFindCustomer", parameters, commandType: CommandType.StoredProcedure);
            if (executed == 0)
            {
                throw new CustomNoRowAffectedException(ErrorCode.NoRowAffected.Message());
            }
        }


        var customerDataJson = parameters.Get<string>("@CustomerDataBack");
        var customerAddressListJson = parameters.Get<string>("@CustomerAddressListDataBack");
        cmrDataResult.cmrResult = JsonConvert.DeserializeObject<List<CmrResult>>(customerDataJson)?.FirstOrDefault();
        cmrDataResult.cmrAddressListData = customerAddressListJson;

        return cmrDataResult;

    }
   

    public async Task<List<CmrResult>> GetListAsync(string name = null, string email = null, string mobileNo = null, DateTime? visitedDateFrom = null, DateTime? visitedDateTo = null)
    {
        using (var connection = _dbDapperContext.CreateConnection())
        { 
            var result=new List<CmrResult>();   
            var parameters = new DynamicParameters();
            parameters.Add("@Name", name);
            parameters.Add("@Email", email);
            parameters.Add("@MobileNo", mobileNo);
            parameters.Add("@VisitedDateFrom", visitedDateFrom);
            parameters.Add("@VisitedDateTo", visitedDateTo);
            var sql = "SpGetCustomers";
            result=(await connection.QueryAsync<CmrResult>(sql, parameters, commandType: CommandType.StoredProcedure)).ToList();
            return result;
        }
    }

    public async Task<CmrDataResult> PostAsync(CmrDataResult cmrDataResult)
    {
        var parameters = new DynamicParameters();

        
        parameters.Add("@Name", cmrDataResult.cmrResult.Name);
        parameters.Add("@Email", cmrDataResult.cmrResult.Email);
        parameters.Add("@MobileNo", cmrDataResult.cmrResult.MobileNo);
        parameters.Add("@VisitedDate", cmrDataResult.cmrResult.VisitedDate);
        parameters.Add("@CustomerAddressListData", cmrDataResult.cmrAddressListData);
        parameters.Add("@CustomerDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);
        parameters.Add("@CustomerAddressListDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);

        using (var connection = _dbDapperContext.CreateConnection())
        {

            var executed = await connection.ExecuteAsync("SpCreateCustomer", parameters, commandType: CommandType.StoredProcedure);
            if (executed == 0)
            {
                throw new CustomNoRowAffectedException(ErrorCode.NoRowAffected.Message());
            }
        }       

      
        var customerDataJson = parameters.Get<string>("@CustomerDataBack");
        var customerAddressListJson = parameters.Get<string>("@CustomerAddressListDataBack");       
        cmrDataResult.cmrResult = JsonConvert.DeserializeObject<List<CmrResult>>(customerDataJson)?.FirstOrDefault();
        cmrDataResult.cmrAddressListData = customerAddressListJson;

        return cmrDataResult;
    }

    public async Task<CmrDataResult> PutAsync(CmrDataResult cmrDataResult)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@CustomerId", cmrDataResult.cmrResult.Id);
        parameters.Add("@Name", cmrDataResult.cmrResult.Name);
        parameters.Add("@Email", cmrDataResult.cmrResult.Email);
        parameters.Add("@MobileNo", cmrDataResult.cmrResult.MobileNo);
        parameters.Add("@VisitedDate", cmrDataResult.cmrResult.VisitedDate);
        parameters.Add("@CustomerAddressListData", cmrDataResult.cmrAddressListData);
        parameters.Add("@CustomerDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);
        parameters.Add("@CustomerAddressListDataBack", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);

        using (var connection = _dbDapperContext.CreateConnection())
        {

            var executed = await connection.ExecuteAsync("SpUpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
            if (executed == 0)
            {
                throw new CustomNoRowAffectedException(ErrorCode.NoRowAffected.Message());
            }
        }


        var customerDataJson = parameters.Get<string>("@CustomerDataBack");
        var customerAddressListJson = parameters.Get<string>("@CustomerAddressListDataBack");
        cmrDataResult.cmrResult = JsonConvert.DeserializeObject<List<CmrResult>>(customerDataJson)?.FirstOrDefault();
        cmrDataResult.cmrAddressListData = customerAddressListJson;

        return cmrDataResult;
    }
    public async Task<List<CmrResult>> CheckDB()
    {
        using (var connection = _dbDapperContext.CreateConnection())
        {
            var sql = "SELECT * FROM Customer";
            var enitities=(await connection.QueryAsync<CmrResult>(sql)).ToList();
            return enitities;
        }
    }

 
}
