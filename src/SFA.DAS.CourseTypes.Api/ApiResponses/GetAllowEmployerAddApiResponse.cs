using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowEmployerAdd;

namespace SFA.DAS.CourseTypes.Api.ApiResponses;

public class GetAllowEmployerAddApiResponse
{
    public bool AllowEmployerAdd { get; set; }

    public static implicit operator GetAllowEmployerAddApiResponse(GetAllowEmployerAddResult source)
    {
        return new GetAllowEmployerAddApiResponse
        {
            AllowEmployerAdd = source.AllowEmployerAdd
        };
    }
}
