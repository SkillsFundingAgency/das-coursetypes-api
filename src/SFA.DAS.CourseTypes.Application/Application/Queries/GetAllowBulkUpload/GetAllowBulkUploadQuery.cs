using MediatR;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowBulkUpload;

public class GetAllowBulkUploadQuery : IRequest<GetAllowBulkUploadResult>
{
    public string CourseTypeShortCode { get; set; } = null!;
}