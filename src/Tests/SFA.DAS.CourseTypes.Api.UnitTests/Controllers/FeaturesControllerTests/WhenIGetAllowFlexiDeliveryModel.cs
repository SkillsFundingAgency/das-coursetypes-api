using System.Net;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Api.ApiResponses;
using SFA.DAS.CourseTypes.Api.Controllers;
using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowFlexiDeliveryModel;

namespace SFA.DAS.CourseTypes.Api.UnitTests.Controllers.FeaturesControllerTests;

[TestFixture]
public class WhenIGetAllowFlexiDeliveryModel
{
    private Mock<IMediator> _mockMediator;
    private Mock<ILogger<FeaturesController>> _mockLogger;
    private FeaturesController _sut;

    [SetUp]
    public void SetUp()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<FeaturesController>>();
        _sut = new FeaturesController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Then_The_AllowFlexiDeliveryModel_Is_Returned_From_Mediator()
    {
        // Arrange
        var courseTypeShortCode = "FoundationApprenticeship";
        var queryResult = new GetAllowFlexiDeliveryModelResult { IsAllowed = true };

        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowFlexiDeliveryModelQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _sut.GetAllowFlexiDeliveryModel(courseTypeShortCode);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeOfType<GetAllowFlexiDeliveryModelApiResponse>();
        var response = okResult.Value as GetAllowFlexiDeliveryModelApiResponse;
        response!.IsAllowed.Should().BeTrue();
    }

    [Test]
    public async Task Then_The_Correct_Query_Is_Sent_To_Mediator()
    {
        // Arrange
        var courseTypeShortCode = "ApprenticeshipUnit";
        var queryResult = new GetAllowFlexiDeliveryModelResult { IsAllowed = false };

        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowFlexiDeliveryModelQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        await _sut.GetAllowFlexiDeliveryModel(courseTypeShortCode);

        // Assert
        _mockMediator.Verify(x => x.Send(
            It.Is<GetAllowFlexiDeliveryModelQuery>(q => q.CourseTypeShortCode == courseTypeShortCode),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task And_Exception_Thrown_Then_Returns_Internal_Server_Error()
    {
        // Arrange
        var courseTypeShortCode = "ApprenticeshipUnit";

        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowFlexiDeliveryModelQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _sut.GetAllowFlexiDeliveryModel(courseTypeShortCode);

        // Assert
        result.Should().BeOfType<StatusCodeResult>();
        var statusResult = result as StatusCodeResult;
        statusResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}