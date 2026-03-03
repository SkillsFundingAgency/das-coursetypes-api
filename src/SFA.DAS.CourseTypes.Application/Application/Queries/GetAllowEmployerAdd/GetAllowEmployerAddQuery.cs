using MediatR;

namespace SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowEmployerAdd;

public class GetAllowEmployerAddQuery : IRequest<GetAllowEmployerAddResult>
{
    public string LearningType { get; set; } = null!;
}
