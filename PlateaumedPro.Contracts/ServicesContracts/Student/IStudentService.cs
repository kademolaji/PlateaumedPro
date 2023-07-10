using PlateaumedPro.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Contracts
{
    public interface IStudentService
    {
        Task<ApiResponse<CreateResponse>> CreateStudent(StudentDto model);
        Task<ApiResponse<GetResponse<StudentDto>>> GetStudent(long studentId);
        Task<ApiResponse<SearchReply<StudentDto>>> GetStudentList(SearchCall<SearchParameter> options);
        Task<ApiResponse<DeleteReply>> DeleteStudent(long studentId);
        Task<ApiResponse<DeleteReply>> DeleteMultipleStudent(MultipleDeleteDto model);
        Task<ApiResponse<GetResponse<byte[]>>> ExportStudent();
        Task<ApiResponse<CreateResponse>> UploadStudent(byte[] record);
    }
}
