namespace SFA.DAS.CourseTypes.Domain.Features;

public class AllowBulkUpload(bool isAllowed)
{
    public bool IsAllowed { get; } = isAllowed;
}