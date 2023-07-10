using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;


namespace PlateaumedPro.Contracts
{
    public interface IAuditTrailService
    {
        void SaveAuditTrail(string details, string endpoint, ActionType actionType, string createdBy);
        Task<ApiResponse<SearchReply<SearchAuditTrailDto>>> SearchAuditTrail(SearchCall<string> options);
    }
}
