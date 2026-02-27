using SFA.DAS.CourseTypes.Domain.Features;

namespace SFA.DAS.CourseTypes.Domain.CourseTypes;

public class FoundationApprenticeship : CourseType
{
    public override string ShortCode => "FoundationApprenticeship";
    public override RecognitionOfPriorLearning RecognitionOfPriorLearning => new RecognitionOfPriorLearningNotRequired();
    public override LearnerAge LearnerAge => new(minimumAge: 15, maximumAge: 25);
    public override CourseDuration CourseDuration => new(minimumDurationMonths: 8, maximumDurationMonths: 48);
    public override AllowBulkUpload AllowBulkUpload => new(isAllowed: true);
    public override AllowFlexiDeliveryModel AllowFlexiDeliveryModel => new(isAllowed: true);
}