using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Domain.CourseTypes;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.UnitTests.Factories;

[TestFixture]
public class CourseTypeFactoryTests
{
    private CourseTypeFactory _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new CourseTypeFactory();
    }

    [Test]
    public void Get_WhenApprenticeshipShortCode_ShouldReturnApprenticeshipType()
    {
        // Act
        var result = _sut.Get("Apprenticeship");

        // Assert
        result.Should().BeOfType<Apprenticeship>();
        result.ShortCode.Should().Be("Apprenticeship");
    }

    [Test]
    public void Get_WhenFoundationApprenticeshipShortCode_ShouldReturnFoundationApprenticeshipType()
    {
        // Act
        var result = _sut.Get("FoundationApprenticeship");

        // Assert
        result.Should().BeOfType<FoundationApprenticeship>();
        result.ShortCode.Should().Be("FoundationApprenticeship");
    }

    [Test]
    public void Get_WhenApprenticeshipUnitShortCode_ShouldReturnApprenticeshipUnitType()
    {
        // Act
        var result = _sut.Get("ApprenticeshipUnit");

        // Assert
        result.Should().BeOfType<ApprenticeshipUnit>();
        result.ShortCode.Should().Be("ApprenticeshipUnit");
    }

    [Test]
    public void Get_WhenCaseInsensitiveShortCode_ShouldReturnCorrectType()
    {
        // Act
        var result = _sut.Get("apprenticeshipunit");

        // Assert
        result.Should().BeOfType<ApprenticeshipUnit>();
        result.ShortCode.Should().Be("ApprenticeshipUnit");
    }

    [Test]
    public void Get_WhenInvalidShortCode_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _sut.Get("InvalidCourseType"));
    }
}