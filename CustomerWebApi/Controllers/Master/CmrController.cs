using AutoMapper;
using Azure;
using CustomerGeneral.GeneralErrors;
using CustomerGeneral.Utils;
using CustomerService;
using CustomerServiceContract.IService.Master;
using CustomerServiceContract.ServiceObjects.Master;
using CustomerWebApi.DataTransferObject.Master.CmrDto;
using CustomerWebApi.DataTransferObject.Master.CustomerDto;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CustomerWebApi.Controllers.Master
{
    [Route("api/Master/[controller]")]
    [ApiController]
    public class CmrController : ControllerBase
    {
        private readonly ICmrService _cmrservice;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCmrRequestDto> _cmrRequestDtoValidator;
        private readonly IValidator<UpdateCmrRequestDto> _cmrUpdateRequestDtoValidator;
        public CmrController(ICmrService cmrService,IMapper mapper, IValidator<CreateCmrRequestDto> cmrRequestDtoValidator, IValidator<UpdateCmrRequestDto> cmrUpdateRequestDtoValidator)
        {
            _cmrservice = cmrService;
            _mapper = mapper;
            _cmrRequestDtoValidator = cmrRequestDtoValidator;
            _cmrUpdateRequestDtoValidator = cmrUpdateRequestDtoValidator;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            try
            {
               
                var serviceResponse = await _cmrservice.GetAsync(Id);
                if (serviceResponse == null)
                    return NotFound(new { Message = $"Resource with Id {serviceResponse.cmrServiceObject.Id} not found." });
                var result = _mapper.Map<FindCmrDataResponseDto>(serviceResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync(string name = null, string email = null, string mobileNo = null, string? visitedDateFromS = null, string? visitedDateToS = null)
        {
            try
            {
              
                var visitedDateFrom = GeneralUtils.ToDateTimeNull(visitedDateFromS);
                var visitedDateTo = GeneralUtils.ToDateTimeNull(visitedDateToS);

                if ((!string.IsNullOrEmpty(visitedDateFromS) && visitedDateFrom == null) ||
                    (!string.IsNullOrEmpty(visitedDateToS) && visitedDateTo == null))
                {
                    return BadRequest(new { Message = "Invalid date format. Please use a valid date format." });
                }

              
                var serviceResponse = await _cmrservice.GetListAsync(name, email, mobileNo, visitedDateFrom, visitedDateTo);                //
                var result = _mapper.Map<List<CreateCmrResponseDto>>(serviceResponse);               
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        //[HttpGet("CheckDB")]
        //public async Task<IActionResult> CheckDBAsync()
        //{

        //    var result=_mapper.Map<List<CreateCmrResponseDto>>(await _cmrservice.CheckDB());
        //    return Ok(result);
        //}
        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCmrDataRequestDto createCmrDataRequestDto)
        {
            var validationResult = await _cmrRequestDtoValidator.ValidateAsync(createCmrDataRequestDto.cmrRequestDto);

            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors.ToList());
            try
            {
                var serviceRequest = _mapper.Map<CmrDataServiceObject>(createCmrDataRequestDto);               
                var serviceResponse = _mapper.Map<CreateCmrDataResponseDto>(await _cmrservice.PostAsync(serviceRequest));

                return StatusCode(StatusCodes.Status201Created, new { Status = ErrorCode.Success.Status(), Message = ErrorCode.Success.Message(), Data = serviceResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
         
            
        }
        [HttpPut("Put")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCmrDataRequestDto updateCmrDataRequestDto)
        {
           
            var validationResult = await _cmrUpdateRequestDtoValidator.ValidateAsync(updateCmrDataRequestDto.cmrRequestDto);

       
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToList());

            try
            {
               
                var serviceRequest = _mapper.Map<CmrDataServiceObject>(updateCmrDataRequestDto);               
                var serviceResponse = await _cmrservice.PutAsync(serviceRequest);                
                if (serviceResponse == null)
                    return NotFound(new { Message = "Resource not found." });

                var serviceResponseDto = _mapper.Map<UpdateCmrDataResponseDto>(serviceResponse);
              
                return Ok(serviceResponseDto);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try
            {
                
                var serviceResponse = await _cmrservice.DeleteAsync(Id);
                if (serviceResponse == null)
                    return NotFound(new { Message = $"Resource with Id {serviceResponse.cmrServiceObject.Id} not found." });
                var result = _mapper.Map<FindCmrDataResponseDto>(serviceResponse);

                return Ok(result);
            }
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

    }
}

