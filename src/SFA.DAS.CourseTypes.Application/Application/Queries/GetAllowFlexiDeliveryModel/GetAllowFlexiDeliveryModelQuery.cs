using MediatR;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowFlexiDeliveryModel;

public class GetAllowFlexiDeliveryModelQuery : IRequest<GetAllowFlexiDeliveryModelResult>
{
    public string CourseTypeShortCode { get; set; } = null!;
}