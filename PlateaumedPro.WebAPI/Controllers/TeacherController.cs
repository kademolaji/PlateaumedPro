using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.WebAPI.MiddleWare;
using PlateaumedPro.WebAPI.Utilities;

namespace PlateaumedPro.WebAPI.Controllers
{

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _service;
    private readonly IHttpAccessorService _httpAccessorService;
    private readonly ILoggerService _loggerService;
    public TeacherController(ITeacherService service, IHttpAccessorService httpAccessorService, ILoggerService loggerService)
    {
        _service = service;
        _httpAccessorService = httpAccessorService;
        _loggerService = loggerService;
    }
    //  POST /api/Teacher/Create
    /// <summary>
    /// Create Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <param name="model"></param>
    /// <returns>Created success message </returns>
    /// <response code="201">Teacher Created Successfully</response>
    /// <response code="400">If an error occur or invalid request payload</response>
    [HttpPost]
    [Route("Create")]
    [ProducesResponseType(201, Type = typeof(CreateResponse))]
    [ProducesResponseType(400, Type = typeof(CreateResponse))]
    public async Task<IActionResult> Create(TeacherDto model)
    {
        try
        {
            _loggerService.Info($"[TeacherController] Teacher with NationalIDNumber [{model.NationalIDNumber}] -> New create Teacher request with model {model}");

            var apiResponse = await _service.CreateTeacher(model);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                _loggerService.Info($"[TeacherController] Teacher with NationalIDNumber [{model.NationalIDNumber}] -> returned badrequest {apiResponse.ResponseType}");

                return BadRequest(apiResponse.ResponseType);
            }
            _loggerService.Info($"[TeacherController] Teacher with NationalIDNumber [{model.NationalIDNumber}] -> returned OK {apiResponse.ResponseType}");

            return Ok(apiResponse.ResponseType);

        }
        catch (Exception ex)
        {
            _loggerService.Error($"[TeacherController] Teacher with NationalIDNumber [{model.NationalIDNumber}] -> Server Error {ex.Message}");

            return BadRequest($"Server Error {ex.Message}");
        }
    }
    // GET api/Teacher/GetList
    /// <summary>
    /// Get list of Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <param name="options"></param>
    /// <returns>List of Teacher</returns>
    /// <response code="200">Returns list of Teacher</response>
    /// <response code="404">If list of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpPost]
    [Route("GetList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<List<TeacherDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
    public async Task<IActionResult> GetList(SearchCall<SearchParameter> options)
    {
        try
        {
            var apiResponse = await _service.GetTeacherList(options);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }

    // GET api/Teacher/Get
    /// <summary>
    /// Get object of Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <param name="TeacherId"></param>
    /// <returns>Object of Teacher</returns>
    /// <response code="200">Returns object of Teacher</response>
    /// <response code="404">If object of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpGet]
    [Route("Get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<TeacherDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
    public async Task<IActionResult> Get(long TeacherId)
    {
        try
        {
            var apiResponse = await _service.GetTeacher(TeacherId);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }

    // GET api/Teacher/Get
    /// <summary>
    /// Export Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <returns>Object of Teacher</returns>
    /// <response code="200">Returns object of Teacher</response>
    /// <response code="404">If object of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpGet]
    [Route("Export")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<byte[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
    public async Task<IActionResult> GenerateExportTeacher()
    {
        try
        {
            var apiResponse = await _service.ExportTeacher();
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);

        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }

    // GET api/Teacher/Get
    /// <summary>
    /// Upload Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <returns>Object of Teacher</returns>
    /// <response code="200">Returns object of Teacher</response>
    /// <response code="404">If object of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpPost]
    [Route("Upload")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CreateResponse))]
    public async Task<IActionResult> UploadTeacher([FromForm] UploadModel model)
    {
        try
        {

            var apiResponse = new ApiResponse<CreateResponse>();

            if (model.file == null || model.file.Length <= 0)
            {
                return BadRequest(new CreateResponse { Id = "", Message = "File upload is required", Status = false });
            }
            if (!Path.GetExtension(model.file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new CreateResponse { Id = "", Message = "Unsuported file format", Status = false });
            }
            using (var stream = new MemoryStream())
            {
                await model.file.CopyToAsync(stream);
                apiResponse = await _service.UploadTeacher(stream.ToArray());
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);

        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }


    // GET api/Teacher/Delete
    /// <summary>
    /// Delete Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <param name="TeacherId"></param>
    /// <returns>Object of Teacher</returns>
    /// <response code="200">Returns object of Teacher</response>
    /// <response code="404">If object of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteReply))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteReply))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteReply))]
    public async Task<IActionResult> Delete(long TeacherId)
    {
        try
        {

            var apiResponse = await _service.DeleteTeacher(TeacherId);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);

        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }

    /// <summary>
    /// Delete Multiple Teacher
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// </remarks>
    /// <param name="model"></param>
    /// <returns>Object of Teacher</returns>
    /// <response code="200">Returns object of Teacher</response>
    /// <response code="404">If object of Teacher is null</response> 
    /// <response code="400">If an error occur or invalid request payload</response> 
    [HttpPost]
    [Route("MultipleDelete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteReply))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteReply))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteReply))]
    public async Task<IActionResult> MultipleDelete(MultipleDeleteDto model)
    {
        try
        {

            var apiResponse = await _service.DeleteMultipleTeacher(model);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(apiResponse.ResponseType);
            }

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(apiResponse.ResponseType);
            }

            return Ok(apiResponse.ResponseType);

        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }
}
}