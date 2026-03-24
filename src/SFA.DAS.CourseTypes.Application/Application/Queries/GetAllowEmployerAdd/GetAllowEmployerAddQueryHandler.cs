using MediatR;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowEmployerAdd;

public class GetAllowEmployerAddQueryHandler(ICourseTypeFactory courseTypeFactory) : IRequestHandler<GetAllowEmployerAddQuery, GetAllowEmployerAddResult>
{
    public async Task<GetAllowEmployerAddResult> Handle(GetAllowEmployerAddQuery request, CancellationToken cancellationToken)
    {
        var courseType = courseTypeFactory.Get(request.LearningType);

        return new GetAllowEmployerAddResult
        {
            AllowEmployerAdd = courseType.AllowEmployerAdd
        };
    }
}
