using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Services
{
    public class StudentServices : IStudentService
    {
        private readonly DataContext context;
        private readonly IAuditTrailService auditTrail;

        public StudentServices(DataContext appContext, IAuditTrailService _auditTrail)
        {
            context = appContext;
            this.auditTrail = _auditTrail;
        }

        public async Task<ApiResponse<CreateResponse>> CreateStudent(StudentDto model)
        {
            try
            {

                if (model.Id > 0)
                {
                    return await Update(model);
                }
                if (string.IsNullOrEmpty(model.NationalIDNumber))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "NationalIDNumber is required." }, IsSuccess = false };
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Name is required." }, IsSuccess = false };
                }
                if (string.IsNullOrEmpty(model.Surname))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Surname is required." }, IsSuccess = false };
                }
                //TODO: Date should consider leap year
                if ((DateTime.Now.Year - model.DateOfBirth.Year) > 22)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Age cannot be greater than 22" }, IsSuccess = false };
                }
                var isExist = await context.Students.AnyAsync(x => x.NationalIDNumber == model.NationalIDNumber );

                if (isExist)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"NationalIDNumber {model.NationalIDNumber} already exist." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<CreateResponse>();
                bool result = false;
                Student entity = null;
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        entity = model.ToModel<Student>();
                        context.Students.Add(entity);
                        result = await context.SaveChangesAsync() > 0;
                        if (result)
                        {
                            var details = $"Created new Student: NationalIDNumber = {model.NationalIDNumber}, Name = {model.Name}, Surname = {model.Surname}, DateOfBirth = {model.DateOfBirth}, StudentNumber = {model.StudentNumber} ";
                            await auditTrail.SaveAuditTrail(details, "Student", ActionType.Created, "");
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
                    }
                }

                var response = new CreateResponse
                {
                    Status = result,
                    Id = entity.Id,
                    Message = "Record created successfully"
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;

            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<CreateResponse>> Update(StudentDto model)
        {
            try
            {


                if (string.IsNullOrEmpty(model.NationalIDNumber))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "NationalIDNumber is required." }, IsSuccess = false };
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Name is required." }, IsSuccess = false };
                }
                if (string.IsNullOrEmpty(model.Surname))
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Surname is required." }, IsSuccess = false };
                }
                //TODO: Date should consider leap year
                if ((DateTime.Now.Year - model.DateOfBirth.Year) > 22)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Age cannot be greater than 22" }, IsSuccess = false };
                }
                var isExist = await context.Students.AnyAsync(x => x.NationalIDNumber == model.NationalIDNumber && x.Id != model.Id);

                if (isExist)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Another student with NationalIDNumber {model.NationalIDNumber} already exist." }, IsSuccess = false };
                }

               

                var apiResponse = new ApiResponse<CreateResponse>();
                bool result = false;
                var entity = await context.Students.FindAsync(model.Id);
                if (entity == null)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Record does not exist." }, IsSuccess = false };
                }
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        entity.NationalIDNumber = model.NationalIDNumber;
                        entity.Name = model.Name;
                        entity.DateOfBirth = model.DateOfBirth;
                        entity.Surname = model.Surname;
                        result = await context.SaveChangesAsync() > 0;
                        if (result)
                        {
                            var details = $"Updated Student: NationalIDNumber = {model.NationalIDNumber}, Name = {model.Name}, Surname = {model.Surname}, DateOfBirth = {model.DateOfBirth}, StudentNumber = {model.StudentNumber} ";
                            await auditTrail.SaveAuditTrail(details, "Student", ActionType.Updated, "");
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
                    }
                }

                var response = new CreateResponse
                {
                    Status = result,
                    Id = entity.Id,
                    Message = "Record updated successfully"
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;

            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<SearchReply<StudentDto>>> GetStudentList(SearchCall<SearchParameter> options)
        {
            int count = 0;
            int pageNumber = options.From > 0 ? options.From : 0;
            int pageSize = options.PageSize > 0 ? options.PageSize : 10;
            string sortOrder = string.IsNullOrEmpty(options.SortOrder) ? "asc" : options.SortOrder;
            string sortField = string.IsNullOrEmpty(options.SortField) ? "nationalIdNumber" : options.SortField;

            try
            {
                var apiResponse = new ApiResponse<SearchReply<StudentDto>>();


                IQueryable<Student> query = context.Students;
                int offset = (pageNumber) * pageSize;

                if (!string.IsNullOrEmpty(options.Parameter.SearchQuery))
                {
                    query = query.Where(x => x.NationalIDNumber.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                   || x.Name.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                   || x.Surname.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                    || x.StudentNumber.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower()));
                }
                switch (sortField)
                {
                    case "nationalIdNumber":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.NationalIDNumber) : query.OrderByDescending(s => s.NationalIDNumber);
                        break;
                    case "name":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name);
                        break;
                    case "surname":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.Surname) : query.OrderByDescending(s => s.Surname);
                        break;
                    case "studentNumber":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.StudentNumber) : query.OrderByDescending(s => s.StudentNumber);
                        break;
                    case "dateOfBirth":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.DateOfBirth) : query.OrderByDescending(s => s.DateOfBirth);
                        break;
                    default:
                        query = query.OrderBy(s => s.NationalIDNumber);
                        break;
                }
                count = query.Count();
                var items = await query.Skip(offset).Take(pageSize).ToListAsync();


                var response = new SearchReply<StudentDto>()
                {
                    TotalCount = count,
                    Result = items.Select(x => x.ToModel<StudentDto>()).ToList(),
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<SearchReply<StudentDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new SearchReply<StudentDto>() { TotalCount = count }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<GetResponse<StudentDto>>> GetStudent(long studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return new ApiResponse<GetResponse<StudentDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<StudentDto> { Status = false, Entity = null, Message = "StudentId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<GetResponse<StudentDto>>();

                var result = await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);

                if (result == null)
                {
                    return new ApiResponse<GetResponse<StudentDto>>() { StatusCode = System.Net.HttpStatusCode.NotFound, ResponseType = new GetResponse<StudentDto>() { Status = false, Message = "No record found" }, IsSuccess = false };
                }

                var response = new GetResponse<StudentDto>()
                {
                    Status = true,
                    Entity = result.ToModel<StudentDto>(),
                    Message = ""
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetResponse<StudentDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<StudentDto>() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<DeleteReply>> DeleteStudent(long studentId)
        {
            try
            {

                if (studentId <= 0)
                {
                    return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply { Status = false, Message = "StudentId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<DeleteReply>();

                var result = context.Students.Find(studentId);

                if (result == null)
                {
                    return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.NotFound, ResponseType = new DeleteReply { Status = false, Message = "No record found" }, IsSuccess = false };
                }
                result.IsDeleted = true;


                var response = new DeleteReply()
                {
                    Status = await context.SaveChangesAsync() > 0,
                    Message = "Record Deleted Successfully"
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                var details = $"Deleted Student: NationalIDNumber = {result.NationalIDNumber}, Name = {result.Name}, Surname = {result.Surname}, DateOfBirth = {result.DateOfBirth}, StudentNumber = {result.StudentNumber} ";
                await auditTrail.SaveAuditTrail(details, "Student", ActionType.Deleted, "");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<DeleteReply>> DeleteMultipleStudent(MultipleDeleteDto model)
        {
            try
            {
                if (model.targetIds.Count <= 0)
                {
                    return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply { Status = false, Message = "StudentId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<DeleteReply>();

                foreach (var item in model.targetIds)
                {
                    var data = await context.Students.FindAsync(item);
                    if (data != null)
                    {
                        data.IsDeleted = true;
                    }
                };

                var response = new DeleteReply()
                {
                    Status = await context.SaveChangesAsync() > 0,
                    Message = "Records Deleted Successfully"
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                var details = $"Deleted Multiple Students: with Ids {model.targetIds.ToArray()} ";
                await auditTrail.SaveAuditTrail(details, "Student", ActionType.Deleted ,"");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<GetResponse<byte[]>>> ExportStudent()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("NationalIDNumber");
                dt.Columns.Add("Name");
                dt.Columns.Add("Surname");
                dt.Columns.Add("DateOfBirth");
                dt.Columns.Add("StudentNumber");
                var apiResponse = new ApiResponse<GetResponse<byte[]>>();

                var students = await (from a in context.Students
                                        where  a.IsDeleted == false
                                        select new StudentDto
                                        {
                                            Id = a.Id,
                                            NationalIDNumber = a.NationalIDNumber,
                                            Name = a.Name,
                                            Surname = a.Surname,
                                            DateOfBirth = a.DateOfBirth,
                                            StudentNumber = a.StudentNumber,
                                        }).ToListAsync();

                if (students.Count == 0)
                {
                    return new ApiResponse<GetResponse<byte[]>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<byte[]> { Status = false, Message = "No record found." }, IsSuccess = false };
                }
    
                foreach (var kk in students)
                {
                    var row = dt.NewRow();
                    row["Id"] = kk.Id;
                    row["NationalIDNumber"] = kk.NationalIDNumber;
                    row["Name"] = kk.Name;
                    row["Surname"] = kk.Surname;
                    row["DateOfBirth"] = kk.DateOfBirth;
                    row["StudentNumber"] = kk.StudentNumber;
                    dt.Rows.Add(row);
                }
                Byte[] fileBytes = null;

                if (students != null)
                {
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Students");
                        ws.DefaultColWidth = 20;
                        ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.None);
                        fileBytes = pck.GetAsByteArray();
                    }
                }
                var response = new GetResponse<byte[]>()
                {
                    Status = true,
                    Entity = fileBytes,
                    Message = ""
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                var details = $"Downloaded Students: TotalCount {students.Count} ";
                await auditTrail.SaveAuditTrail(details, "Student", ActionType.Download, "");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetResponse<byte[]>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<byte[]> { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<CreateResponse>> UploadStudent(byte[] record)
        {
            try
            {
                var apiResponse = new ApiResponse<CreateResponse>();
                if (record == null)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Upload a valid record." }, IsSuccess = false };
                }

                List<StudentDto> uploadedRecord = new List<StudentDto>();

                using (MemoryStream stream = new MemoryStream(record))
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    //Use first sheet by default
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    int totalRows = workSheet.Dimension.Rows;
                    //First row is considered as the header
                    for (int i = 2; i <= totalRows; i++)
                    {
                        uploadedRecord.Add(new StudentDto
                        {
                            NationalIDNumber = workSheet.Cells[i, 1].Value.ToString(),
                            Name = workSheet.Cells[i, 2].Value.ToString(),
                            Surname = workSheet.Cells[i, 3].Value.ToString(),
                            DateOfBirth = Convert.ToDateTime(workSheet.Cells[i, 4].Value),
                            StudentNumber = workSheet.Cells[i, 5].Value.ToString(),
                        });
                    }
                }
                List<Student> structures = new List<Student>();
                if (uploadedRecord.Count > 0)
                {

                    foreach (var item in uploadedRecord)
                    {
                        if (string.IsNullOrEmpty(item.NationalIDNumber))
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "NationalIDNumber is required." }, IsSuccess = false };
                        }
                        if (string.IsNullOrEmpty(item.Name))
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Name is required." }, IsSuccess = false };
                        }
                        if (string.IsNullOrEmpty(item.Surname))
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Surname is required." }, IsSuccess = false };
                        }
                        //TODO: Date should consider leap year
                        if ((DateTime.Now.Year - item.DateOfBirth.Year) > 22)
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Age cannot be greater than 22" }, IsSuccess = false };
                        }
                        var isExist = await context.Students.AnyAsync(x => x.NationalIDNumber == item.NationalIDNumber);

                        if (isExist)
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"NationalIDNumber {item.NationalIDNumber} already exist." }, IsSuccess = false };
                        }

                        var structure = new Student
                        {
                            NationalIDNumber = item.NationalIDNumber,
                            Name = item.Name,
                            Surname = item.Surname,
                            DateOfBirth = item.DateOfBirth,
                            StudentNumber = item.StudentNumber,
                            IsDeleted = false,
                            CreatedOn = DateTime.UtcNow,
                        };
                        structures.Add(structure);
                    }
                    context.Students.AddRange(structures);
                }
                var result = await context.SaveChangesAsync() > 0;

                var response = new CreateResponse
                {
                    Status = result,
                    Id = "",
                    Message = "Record Uploaded Successfully"
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                var details = $"Uploaded Students: TotalCount {structures.Count} ";
                await auditTrail.SaveAuditTrail(details, "Student", ActionType.Upload,  "");

                return apiResponse;

            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }
    }
}