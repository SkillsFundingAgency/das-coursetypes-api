using MediatR;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowBulkUpload;

public class GetAllowBulkUploadQueryHandler(ICourseTypeFactory courseTypeFactory) : IRequestHandler<GetAllowBulkUploadQuery, GetAllowBulkUploadResult>
{
    public async Task<GetAllowBulkUploadResult> Handle(GetAllowBulkUploadQuery request, CancellationToken cancellationToken)
    {
        var courseType = courseTypeFactory.Get(request.CourseTypeShortCode);

        return new GetAllowBulkUploadResult
        {
            IsAllowed = courseType.AllowBulkUpload.IsAllowed
        };
    }
}