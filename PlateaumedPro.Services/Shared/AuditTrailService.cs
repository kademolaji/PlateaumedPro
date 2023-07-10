using Microsoft.EntityFrameworkCore;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;


namespace PlateaumedPro.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly DataContext _context;
        private readonly IHttpAccessorService httpAccessorService;

        public AuditTrailService(DataContext appContext, IHttpAccessorService httpAccessorService)
        {
            _context = appContext;
            this.httpAccessorService = httpAccessorService;
        }

        public async Task SaveAuditTrail(string details, string endpoint, ActionType actionType, string createdBy)
        {
            try
            {
                var auditTrail = new AuditTrail
                {
                    Details = details,
                    ActionDate = DateTime.UtcNow,
                    ActionBy = createdBy ?? null,
                    IPAddress = httpAccessorService?.GetClientIP() ?? "",
                    HostAddress = httpAccessorService?.GetHostAddress() ?? "",
                    Endpoint = endpoint,
                    ActionType = actionType,
                };
                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ApiResponse<SearchReply<SearchAuditTrailDto>>> SearchAuditTrail(SearchCall<string> options)
        {
            try
            {
                var apiResponse = new ApiResponse<SearchReply<SearchAuditTrailDto>>();
                var query = _context.AuditTrails.Select(u => new SearchAuditTrailDto
                {
                    Details = u.Details,
                    ActionDate = u.ActionDate,
                    ActionBy = u.ActionBy,
                    IPAddress = u.IPAddress,
                    HostAddress = u.HostAddress,
                    Endpoint = u.Endpoint,
                    ActionType = u.ActionType,
                });

                if (!string.IsNullOrEmpty(options.Parameter))
                {
                    query = query.Where(x => x.Details.Contains(options.Parameter));
                }
                var result = await query.OrderByDescending(x => x.ActionDate).Take(options.PageSize).Skip(options.From).ToListAsync();
                var response = new SearchReply<SearchAuditTrailDto>()
                {
                    TotalCount = query.Count(),
                    Result = result,
                    Errors = null
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;

            }
            catch (Exception ex)
            {
                return new ApiResponse<SearchReply<SearchAuditTrailDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new SearchReply<SearchAuditTrailDto>() { TotalCount = 0, Result = null, Errors = null }, IsSuccess = false };
            }
        }
    }
}