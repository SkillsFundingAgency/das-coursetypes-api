using System.Net;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseTypes.Api.ApiResponses;
using SFA.DAS.CourseTypes.Api.Controllers;
using SFA.DAS.CourseTypes.Application.Application.Queries.GetAllowEmployerAdd;

namespace SFA.DAS.CourseTypes.Api.UnitTests.Controllers.FeaturesControllerTests;

[TestFixture]
public class WhenIGetAllowEmployerAdd
{
    private Mock<IMediator> _mockMediator = null!;
    private Mock<ILogger<FeaturesController>> _mockLogger = null!;
    private FeaturesController _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<FeaturesController>>();
        _sut = new FeaturesController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Then_AllowEmployerAdd_Is_Returned_From_Mediator()
    {
        var learningType = "ApprenticeshipUnit";
        var queryResult = new GetAllowEmployerAddResult { AllowEmployerAdd = false };

        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowEmployerAddQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var result = await _sut.GetAllowEmployerAdd(learningType);

        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeOfType<GetAllowEmployerAddApiResponse>();
        var response = okResult.Value as GetAllowEmployerAddApiResponse;
        response!.AllowEmployerAdd.Should().BeFalse();
    }

    [Test]
    public async Task Then_Correct_Query_Is_Sent_To_Mediator()
    {
        var learningType = "Apprenticeship";
        var queryResult = new GetAllowEmployerAddResult { AllowEmployerAdd = true };

        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowEmployerAddQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        await _sut.GetAllowEmployerAdd(learningType);

        _mockMediator.Verify(x => x.Send(
            It.Is<GetAllowEmployerAddQuery>(q => q.LearningType == learningType),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task When_LearningType_Is_Null_Then_Returns_BadRequest()
    {
        var result = await _sut.GetAllowEmployerAdd(null!);

        result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    public async Task When_LearningType_Is_Empty_Then_Returns_BadRequest()
    {
        var result = await _sut.GetAllowEmployerAdd("");

        result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    public async Task And_Exception_Thrown_Then_Returns_Internal_Server_Error()
    {
        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllowEmployerAddQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        var result = await _sut.GetAllowEmployerAdd("ApprenticeshipUnit");

        result.Should().BeOfType<StatusCodeResult>();
        var statusResult = result as StatusCodeResult;
        statusResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
