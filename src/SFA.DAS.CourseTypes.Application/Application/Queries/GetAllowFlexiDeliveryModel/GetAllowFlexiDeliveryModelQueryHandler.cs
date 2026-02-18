using MediatR;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowFlexiDeliveryModel;

public class GetAllowFlexiDeliveryModelQueryHandler(ICourseTypeFactory courseTypeFactory) : IRequestHandler<GetAllowFlexiDeliveryModelQuery, GetAllowFlexiDeliveryModelResult>
{
    public async Task<GetAllowFlexiDeliveryModelResult> Handle(GetAllowFlexiDeliveryModelQuery request, CancellationToken cancellationToken)
    {
        var courseType = courseTypeFactory.Get(request.CourseTypeShortCode);

        return new GetAllowFlexiDeliveryModelResult
        {
            IsAllowed = courseType.AllowFlexiDeliveryModel.IsAllowed
        };
    }
}