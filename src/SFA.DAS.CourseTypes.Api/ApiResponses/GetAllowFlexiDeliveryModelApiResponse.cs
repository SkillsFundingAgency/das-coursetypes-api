using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowFlexiDeliveryModel;

namespace SFA.DAS.CourseTypes.Api.ApiResponses;

public class GetAllowFlexiDeliveryModelApiResponse
{
    public bool IsAllowed { get; set; }

    public static implicit operator GetAllowFlexiDeliveryModelApiResponse(GetAllowFlexiDeliveryModelResult source)
    {
        return new GetAllowFlexiDeliveryModelApiResponse
        {
            IsAllowed = source.IsAllowed
        };
    }
}