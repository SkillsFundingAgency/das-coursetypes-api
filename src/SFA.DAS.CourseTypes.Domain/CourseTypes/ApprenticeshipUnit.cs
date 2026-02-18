using SFA.DAS.CourseTypes.Domain.Features;

namespace SFA.DAS.CourseTypes.Domain.CourseTypes;

public class ApprenticeshipUnit : CourseType
{
    public override string ShortCode => "ApprenticeshipUnit";
    public override RecognitionOfPriorLearning RecognitionOfPriorLearning => new RecognitionOfPriorLearningNotRequired();
    public override LearnerAge LearnerAge => new(minimumAge: 15, maximumAge: 115);
    public override CourseDuration CourseDuration => new(minimumDurationMonths: 8, maximumDurationMonths: 48);
    public override AllowBulkUpload AllowBulkUpload => new(isAllowed: false);
    public override AllowFlexiDeliveryModel AllowFlexiDeliveryModel => new(isAllowed: false);
}