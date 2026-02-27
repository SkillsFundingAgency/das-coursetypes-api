namespace SFA.DAS.CourseTypes.Domain.Features;

public class AllowFlexiDeliveryModel(bool isAllowed)
{
    public bool IsAllowed { get; } = isAllowed;
}