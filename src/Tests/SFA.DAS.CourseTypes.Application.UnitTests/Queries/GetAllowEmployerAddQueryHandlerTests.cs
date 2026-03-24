using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowEmployerAdd;
using SFA.DAS.CourseTypes.Domain.CourseTypes;
using SFA.DAS.CourseTypes.Domain.Factories;

namespace SFA.DAS.CourseTypes.Application.UnitTests.Queries;

[TestFixture]
public class GetAllowEmployerAddQueryHandlerTests
{
    private Mock<ICourseTypeFactory> _mockCourseTypeFactory = null!;
    private GetAllowEmployerAddQueryHandler _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _mockCourseTypeFactory = new Mock<ICourseTypeFactory>();
        _sut = new GetAllowEmployerAddQueryHandler(_mockCourseTypeFactory.Object);
    }

    [Test]
    public async Task Handle_WhenLearningTypeIsApprenticeship_ShouldReturnTrue()
    {
        var courseType = new Apprenticeship();
        var query = new GetAllowEmployerAddQuery { LearningType = "Apprenticeship" };

        _mockCourseTypeFactory.Setup(x => x.Get("Apprenticeship")).Returns(courseType);

        var result = await _sut.Handle(query, CancellationToken.None);

        result.AllowEmployerAdd.Should().BeTrue();
    }

    [Test]
    public async Task Handle_WhenLearningTypeIsApprenticeshipUnit_ShouldReturnFalse()
    {
        var courseType = new ApprenticeshipUnit();
        var query = new GetAllowEmployerAddQuery { LearningType = "ApprenticeshipUnit" };

        _mockCourseTypeFactory.Setup(x => x.Get("ApprenticeshipUnit")).Returns(courseType);

        var result = await _sut.Handle(query, CancellationToken.None);

        result.AllowEmployerAdd.Should().BeFalse();
    }

    [Test]
    public async Task Handle_WhenLearningTypeIsFoundationApprenticeship_ShouldReturnTrue()
    {
        var courseType = new FoundationApprenticeship();
        var query = new GetAllowEmployerAddQuery { LearningType = "FoundationApprenticeship" };

        _mockCourseTypeFactory.Setup(x => x.Get("FoundationApprenticeship")).Returns(courseType);

        var result = await _sut.Handle(query, CancellationToken.None);

        result.AllowEmployerAdd.Should().BeTrue();
    }
}
