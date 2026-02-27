using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowFlexiDeliveryModel;
using SFA.DAS.CourseTypes.Domain.CourseTypes;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.UnitTests.Queries;

[TestFixture]
public class GetAllowFlexiDeliveryModelQueryHandlerTests
{
    private Mock<ICourseTypeFactory> _mockCourseTypeFactory;
    private GetAllowFlexiDeliveryModelQueryHandler _sut;

    [SetUp]
    public void SetUp()
    {
        _mockCourseTypeFactory = new Mock<ICourseTypeFactory>();
        _sut = new GetAllowFlexiDeliveryModelQueryHandler(_mockCourseTypeFactory.Object);
    }

    [Test]
    public async Task Handle_WhenCourseTypeAllowsFlexiDeliveryModel_ShouldReturnTrue()
    {
        // Arrange
        var courseType = new Apprenticeship();
        var query = new GetAllowFlexiDeliveryModelQuery { CourseTypeShortCode = "Apprenticeship" };

        _mockCourseTypeFactory.Setup(x => x.Get("Apprenticeship")).Returns(courseType);

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.IsAllowed.Should().BeTrue();
    }

    [Test]
    public async Task Handle_WhenCourseTypeDoesNotAllowFlexiDeliveryModel_ShouldReturnFalse()
    {
        // Arrange
        var courseType = new ApprenticeshipUnit();
        var query = new GetAllowFlexiDeliveryModelQuery { CourseTypeShortCode = "ApprenticeshipUnit" };

        _mockCourseTypeFactory.Setup(x => x.Get("ApprenticeshipUnit")).Returns(courseType);

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.IsAllowed.Should().BeFalse();
    }

    [Test]
    public async Task Handle_WhenFoundationApprenticeshipCourseType_ShouldReturnTrue()
    {
        // Arrange
        var courseType = new FoundationApprenticeship();
        var query = new GetAllowFlexiDeliveryModelQuery { CourseTypeShortCode = "FoundationApprenticeship" };

        _mockCourseTypeFactory.Setup(x => x.Get("FoundationApprenticeship")).Returns(courseType);

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.IsAllowed.Should().BeTrue();
    }
}