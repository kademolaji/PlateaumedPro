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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IHttpAccessorService _httpAccessorService;
        private readonly ILoggerService _loggerService;
        public StudentController(IStudentService service, IHttpAccessorService httpAccessorService, ILoggerService loggerService)
        {
            _service = service;
            _httpAccessorService = httpAccessorService;
            _loggerService = loggerService;
        }
        //  POST /api/Student/Create
        /// <summary>
        /// Create Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Created success message </returns>
        /// <response code="201">Student Created Successfully</response>
        /// <response code="400">If an error occur or invalid request payload</response>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(201, Type = typeof(CreateResponse))]
        [ProducesResponseType(400, Type = typeof(CreateResponse))]
        public async Task<IActionResult> Create(StudentDto model)
        {
            try
            {
                _loggerService.Info($"[StudentController] Student with NationalIDNumber [{model.NationalIDNumber}] -> New create Student request with model {model}");

                var apiResponse = await _service.CreateStudent(model);
                if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _loggerService.Info($"[StudentController] Student with NationalIDNumber [{model.NationalIDNumber}] -> returned badrequest {apiResponse.ResponseType}");

                    return BadRequest(apiResponse.ResponseType);
                }
                _loggerService.Info($"[StudentController] Student with NationalIDNumber [{model.NationalIDNumber}] -> returned OK {apiResponse.ResponseType}");

                return Ok(apiResponse.ResponseType);

            }
            catch (Exception ex)
            {
                _loggerService.Error($"[StudentController] Student with NationalIDNumber [{model.NationalIDNumber}] -> Server Error {ex.Message}");

                return BadRequest($"Server Error {ex.Message}");
            }
        }
        // GET api/Student/GetList
        /// <summary>
        /// Get list of Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <param name="options"></param>
        /// <returns>List of Student</returns>
        /// <response code="200">Returns list of Student</response>
        /// <response code="404">If list of Student is null</response> 
        /// <response code="400">If an error occur or invalid request payload</response> 
        [HttpPost]
        [Route("GetList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<List<StudentDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
        public async Task<IActionResult> GetList(SearchCall<SearchParameter> options)
        {
            try
            {
                var apiResponse = await _service.GetStudentList(options);
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

        // GET api/Student/Get
        /// <summary>
        /// Get object of Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <param name="studentId"></param>
        /// <returns>Object of Student</returns>
        /// <response code="200">Returns object of Student</response>
        /// <response code="404">If object of Student is null</response> 
        /// <response code="400">If an error occur or invalid request payload</response> 
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<StudentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
        public async Task<IActionResult> Get(long studentId)
        {
            try
            {
                var apiResponse = await _service.GetStudent(studentId);
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

        // GET api/Student/Get
        /// <summary>
        /// Export Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <returns>Object of Student</returns>
        /// <response code="200">Returns object of Student</response>
        /// <response code="404">If object of Student is null</response> 
        /// <response code="400">If an error occur or invalid request payload</response> 
        [HttpGet]
        [Route("Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<byte[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetResponse<ProducesResponseStub>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<ProducesResponseStub>))]
        public async Task<IActionResult> GenerateExportStudent()
        {
            try
            {
                var apiResponse = await _service.ExportStudent();
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

        // GET api/Student/Get
        /// <summary>
        /// Upload Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <returns>Object of Student</returns>
        /// <response code="200">Returns object of Student</response>
        /// <response code="404">If object of Student is null</response> 
        /// <response code="400">If an error occur or invalid request payload</response> 
        [HttpPost]
        [Route("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CreateResponse))]
        public async Task<IActionResult> UploadStudent([FromForm] UploadModel model)
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
                    apiResponse = await _service.UploadStudent(stream.ToArray());
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


        // GET api/Student/Delete
        /// <summary>
        /// Delete Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <param name="StudentId"></param>
        /// <returns>Object of Student</returns>
        /// <response code="200">Returns object of Student</response>
        /// <response code="404">If object of Student is null</response> 
        /// <response code="400">If an error occur or invalid request payload</response> 
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteReply))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteReply))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteReply))]
        public async Task<IActionResult> Delete(long StudentId)
        {
            try
            {

                var apiResponse = await _service.DeleteStudent(StudentId);
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
        /// Delete Multiple Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Object of Student</returns>
        /// <response code="200">Returns object of Student</response>
        /// <response code="404">If object of Student is null</response> 
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

                var apiResponse = await _service.DeleteMultipleStudent(model);
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