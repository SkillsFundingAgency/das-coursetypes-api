using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Domain.CourseTypes;
using SFA.DAS.CourseTypes.Domain.Features;

namespace SFA.DAS.CourseTypes.Application.UnitTests.CourseTypes;

[TestFixture]
public class ApprenticeshipUnitTests
{
    private ApprenticeshipUnit _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new ApprenticeshipUnit();
    }

    [Test]
    public void ShortCode_ShouldReturnApprenticeshipUnit()
    {
        // Act
        var result = _sut.ShortCode;

        // Assert
        result.Should().Be("ApprenticeshipUnit");
    }

    [Test]
    public void RecognitionOfPriorLearning_ShouldNotBeRequired()
    {
        // Act
        var result = _sut.RecognitionOfPriorLearning;

        // Assert
        result.Should().BeOfType<RecognitionOfPriorLearningNotRequired>();
        result.IsRequired.Should().BeFalse();
        result.OffTheJobTrainingMinimumHours.Should().BeNull();
    }

    [Test]
    public void LearnerAge_ShouldHaveCorrectAgeRange()
    {
        // Act
        var result = _sut.LearnerAge;

        // Assert
        result.MinimumAge.Should().Be(15);
        result.MaximumAge.Should().Be(115);
    }

    [Test]
    public void CourseDuration_ShouldHaveCorrectDurationRange()
    {
        // Act
        var result = _sut.CourseDuration;

        // Assert
        result.MinimumDurationMonths.Should().Be(8);
        result.MaximumDurationMonths.Should().Be(48);
    }

    [Test]
    public void AllowBulkUpload_ShouldBeFalse()
    {
        // Act
        var result = _sut.AllowBulkUpload;

        // Assert
        result.IsAllowed.Should().BeFalse();
    }

    [Test]
    public void AllowFlexiDeliveryModel_ShouldBeFalse()
    {
        // Act
        var result = _sut.AllowFlexiDeliveryModel;

        // Assert
        result.IsAllowed.Should().BeFalse();
    }
}