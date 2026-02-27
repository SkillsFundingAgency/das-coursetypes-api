using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowBulkUpload;

namespace SFA.DAS.CourseTypes.Api.ApiResponses;

public class GetAllowBulkUploadApiResponse
{
    public bool IsAllowed { get; set; }

    public static implicit operator GetAllowBulkUploadApiResponse(GetAllowBulkUploadResult source)
    {
        return new GetAllowBulkUploadApiResponse
        {
            IsAllowed = source.IsAllowed
        };
    }
}