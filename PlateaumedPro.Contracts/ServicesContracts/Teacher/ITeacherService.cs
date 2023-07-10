using PlateaumedPro.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Contracts
{
    public interface ITeacherService
    {
        Task<ApiResponse<CreateResponse>> CreateTeacher(TeacherDto model);
        Task<ApiResponse<GetResponse<TeacherDto>>> GetTeacher(long teacherId);
        Task<ApiResponse<SearchReply<TeacherDto>>> GetTeacherList(SearchCall<SearchParameter> options);
        Task<ApiResponse<DeleteReply>> DeleteTeacher(long teacherId);
        Task<ApiResponse<DeleteReply>> DeleteMultipleTeacher(MultipleDeleteDto model);
        Task<ApiResponse<GetResponse<byte[]>>> ExportTeacher();
        Task<ApiResponse<CreateResponse>> UploadTeacher(byte[] record);
    }
}
