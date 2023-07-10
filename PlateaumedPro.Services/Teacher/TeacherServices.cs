using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using System.Data;
using System.Xml.Linq;

namespace PlateaumedPro.Services
{

    public class TeacherServices : ITeacherService
    {
        private readonly DataContext context;
        private readonly IAuditTrailService auditTrail;

        public TeacherServices(DataContext appContext, IAuditTrailService _auditTrail)
        {
            context = appContext;
            this.auditTrail = _auditTrail;
        }

        public async Task<ApiResponse<CreateResponse>> CreateTeacher(TeacherDto model)
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
                var isExist = await context.Teachers.AnyAsync(x => x.NationalIDNumber == model.NationalIDNumber);

                if (isExist)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"NationalIDNumber {model.NationalIDNumber} already exist." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<CreateResponse>();
                bool result = false;
                Teacher entity = null;
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        entity = model.ToModel<Teacher>();
                        context.Teachers.Add(entity);
                        result = await context.SaveChangesAsync() > 0;
                        if (result)
                        {
                            var details = $"Created new Teacher: NationalIDNumber = {model.NationalIDNumber}, Name = {model.Name}, Surname = {model.Surname}, DateOfBirth = {model.DateOfBirth}, TeacherNumber = {model.TeacherNumber} ";
                            await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Created, "");
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

        public async Task<ApiResponse<CreateResponse>> Update(TeacherDto model)
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
                var isExist = await context.Teachers.AnyAsync(x => x.NationalIDNumber == model.NationalIDNumber && x.Id != model.Id);

                if (isExist)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Another Teacher with NationalIDNumber {model.NationalIDNumber} already exist." }, IsSuccess = false };
                }



                var apiResponse = new ApiResponse<CreateResponse>();
                bool result = false;
                var entity = await context.Teachers.FindAsync(model.Id);
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
                        entity.Title = entity.Title;
                        entity.TeacherNumber = entity.TeacherNumber;
                        entity.Salary = entity.Salary;

                        result = await context.SaveChangesAsync() > 0;
                        if (result)
                        {
                            var details = $"Updated Teacher: NationalIDNumber = {model.NationalIDNumber}, Name = {model.Name}, Surname = {model.Surname}, DateOfBirth = {model.DateOfBirth}, TeacherNumber = {model.TeacherNumber} ";
                            await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Updated, "");
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

        public async Task<ApiResponse<SearchReply<TeacherDto>>> GetTeacherList(SearchCall<SearchParameter> options)
        {
            int count = 0;
            int pageNumber = options.From > 0 ? options.From : 0;
            int pageSize = options.PageSize > 0 ? options.PageSize : 10;
            string sortOrder = string.IsNullOrEmpty(options.SortOrder) ? "asc" : options.SortOrder;
            string sortField = string.IsNullOrEmpty(options.SortField) ? "nationalIdNumber" : options.SortField;

            try
            {
                var apiResponse = new ApiResponse<SearchReply<TeacherDto>>();


                IQueryable<Teacher> query = context.Teachers;
                int offset = (pageNumber) * pageSize;

                if (!string.IsNullOrEmpty(options.Parameter.SearchQuery))
                {
                    query = query.Where(x => x.NationalIDNumber.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                   || x.Name.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                   || x.Surname.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                    || x.TeacherNumber.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower())
                    || x.Title.Trim().ToLower().Contains(options.Parameter.SearchQuery.Trim().ToLower()));
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
                    case "TeacherNumber":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.TeacherNumber) : query.OrderByDescending(s => s.TeacherNumber);
                        break;
                    case "dateOfBirth":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.DateOfBirth) : query.OrderByDescending(s => s.DateOfBirth);
                        break;
                    case "title":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.Title) : query.OrderByDescending(s => s.Title);
                        break;
                    case "salary":
                        query = sortOrder == "asc" ? query.OrderBy(s => s.Salary) : query.OrderByDescending(s => s.Salary);
                        break;
                    default:
                        query = query.OrderBy(s => s.NationalIDNumber);
                        break;
                }
                count = query.Count();
                var items = await query.Skip(offset).Take(pageSize).ToListAsync();


                var response = new SearchReply<TeacherDto>()
                {
                    TotalCount = count,
                    Result = items.Select(x => x.ToModel<TeacherDto>()).ToList(),
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<SearchReply<TeacherDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new SearchReply<TeacherDto>() { TotalCount = count }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<GetResponse<TeacherDto>>> GetTeacher(long teacherId)
        {
            try
            {
                if (teacherId <= 0)
                {
                    return new ApiResponse<GetResponse<TeacherDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<TeacherDto> { Status = false, Entity = null, Message = "TeacherId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<GetResponse<TeacherDto>>();

                var result = await context.Teachers.FirstOrDefaultAsync(x => x.Id == teacherId);

                if (result == null)
                {
                    return new ApiResponse<GetResponse<TeacherDto>>() { StatusCode = System.Net.HttpStatusCode.NotFound, ResponseType = new GetResponse<TeacherDto>() { Status = false, Message = "No record found" }, IsSuccess = false };
                }

                var response = new GetResponse<TeacherDto>()
                {
                    Status = true,
                    Entity = result.ToModel<TeacherDto>(),
                    Message = ""
                };

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.ResponseType = response;

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetResponse<TeacherDto>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<TeacherDto>() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<DeleteReply>> DeleteTeacher(long teacherId)
        {
            try
            {

                if (teacherId <= 0)
                {
                    return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply { Status = false, Message = "TeacherId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<DeleteReply>();

                var result = context.Teachers.Find(teacherId);

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

                var details = $"Deleted Teacher: NationalIDNumber = {result.NationalIDNumber}, Name = {result.Name}, Surname = {result.Surname}, DateOfBirth = {result.DateOfBirth}, TeacherNumber = {result.TeacherNumber} ";
                await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Deleted, "");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<DeleteReply>> DeleteMultipleTeacher(MultipleDeleteDto model)
        {
            try
            {
                if (model.targetIds.Count <= 0)
                {
                    return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply { Status = false, Message = "TeacherId is required." }, IsSuccess = false };
                }

                var apiResponse = new ApiResponse<DeleteReply>();

                foreach (var item in model.targetIds)
                {
                    var data = await context.Teachers.FindAsync(item);
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

                var details = $"Deleted Multiple Teachers: with Ids {model.targetIds.ToArray()} ";
                await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Deleted, "");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteReply>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new DeleteReply() { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<GetResponse<byte[]>>> ExportTeacher()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("NationalIDNumber");
                dt.Columns.Add("Name");
                dt.Columns.Add("Surname");
                dt.Columns.Add("DateOfBirth");
                dt.Columns.Add("TeacherNumber");
                var apiResponse = new ApiResponse<GetResponse<byte[]>>();

                var Teachers = await (from a in context.Teachers
                                      where a.IsDeleted == false
                                      select new TeacherDto
                                      {
                                          Id = a.Id,
                                          NationalIDNumber = a.NationalIDNumber,
                                          Name = a.Name,
                                          Surname = a.Surname,
                                          DateOfBirth = a.DateOfBirth,
                                          TeacherNumber = a.TeacherNumber,
                                      }).ToListAsync();

                if (Teachers.Count == 0)
                {
                    return new ApiResponse<GetResponse<byte[]>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<byte[]> { Status = false, Message = "No record found." }, IsSuccess = false };
                }
               
                foreach (var kk in Teachers)
                {
                    var row = dt.NewRow();
                    row["Id"] = kk.Id;
                    row["NationalIDNumber"] = kk.NationalIDNumber;
                    row["Name"] = kk.Name;
                    row["Surname"] = kk.Surname;
                    row["DateOfBirth"] = kk.DateOfBirth;
                    row["TeacherNumber"] = kk.TeacherNumber;
                    row["Title"] = kk.Title;
                    row["Salary"] = kk.Salary;
                    dt.Rows.Add(row);
                }
                Byte[] fileBytes = null;

                if (Teachers != null)
                {
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Teachers");
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

                var details = $"Downloaded Teachers: TotalCount {Teachers.Count} ";
                await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Download, "");

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetResponse<byte[]>>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new GetResponse<byte[]> { Status = false, Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }

        public async Task<ApiResponse<CreateResponse>> UploadTeacher(byte[] record)
        {
            try
            {
                var apiResponse = new ApiResponse<CreateResponse>();
                if (record == null)
                {
                    return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = "Upload a valid record." }, IsSuccess = false };
                }

                List<TeacherDto> uploadedRecord = new List<TeacherDto>();

                using (MemoryStream stream = new MemoryStream(record))
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    //Use first sheet by default
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    int totalRows = workSheet.Dimension.Rows;
                    //First row is considered as the header
                    for (int i = 2; i <= totalRows; i++)
                    {
                        uploadedRecord.Add(new TeacherDto
                        {
                            NationalIDNumber = workSheet.Cells[i, 1].Value.ToString(),
                            Name = workSheet.Cells[i, 2].Value.ToString(),
                            Surname = workSheet.Cells[i, 3].Value.ToString(),
                            DateOfBirth = Convert.ToDateTime(workSheet.Cells[i, 4].Value),
                            TeacherNumber = workSheet.Cells[i, 5].Value.ToString(),
                            Title = workSheet.Cells[i, 6].Value.ToString(),
                            Salary = Convert.ToDecimal(workSheet.Cells[i, 7].Value),

                        });
                    }
                }
                List<Teacher> structures = new List<Teacher>();
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
                        var isExist = await context.Teachers.AnyAsync(x => x.NationalIDNumber == item.NationalIDNumber);

                        if (isExist)
                        {
                            return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"NationalIDNumber {item.NationalIDNumber} already exist." }, IsSuccess = false };
                        }

                        var structure = new Teacher
                        {
                            NationalIDNumber = item.NationalIDNumber,
                            Name = item.Name,
                            Surname = item.Surname,
                            DateOfBirth = item.DateOfBirth,
                            TeacherNumber = item.TeacherNumber,
                            Title = item.Title,
                            Salary = item.Salary,
                            IsDeleted = false,
                            CreatedOn = DateTime.UtcNow,
                        };
                        structures.Add(structure);
                    }
                    context.Teachers.AddRange(structures);
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

                var details = $"Uploaded Teachers: TotalCount {structures.Count} ";
                await auditTrail.SaveAuditTrail(details, "Teacher", ActionType.Upload, "");

                return apiResponse;

            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateResponse>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ResponseType = new CreateResponse() { Status = false, Id = "", Message = $"Error encountered {ex.Message}" }, IsSuccess = false };
            }
        }
    }
}