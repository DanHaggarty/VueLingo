using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AiContentService.Interfaces;
using VueLingo.WebApi.Controllers;
using VueLingo.WebApi.Models;
using Microsoft.Extensions.Logging;

namespace VueLingo.Tests.ControllerTests
{
    public class RewriteControllerTests
    {
        private const string RewrittenText = "Rewritten text";

        private readonly Mock<ITextRewriter> _mockRewriter;
        private readonly Mock<ILogger<RewriteController>> _mockLogger;
        private readonly RewriteController _controller;

        public RewriteControllerTests()
        {
            _mockRewriter = new Mock<ITextRewriter>();
            _mockLogger = new Mock<ILogger<RewriteController>>();
            _controller = new RewriteController(_mockRewriter.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Rewrite_LogsRequest()
        {
            _mockRewriter
                .Setup(r => r.RewriteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(RewrittenText);

            var result = await _controller.Rewrite(new RewriteRequest { Text = "", Tone = "neutral", TranslateTo = null });

            // Assert
            _mockLogger.Verify(
                l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) =>
                        v.ToString().Contains("Received rewrite request. Text:") &&
                        v.ToString().Contains("Tone:") &&
                        v.ToString().Contains("TranslateTo:")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Rewrite_ReturnsRewrittenResponse()
        {
            _mockRewriter
                .Setup(r => r.RewriteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(RewrittenText);

            var result = await _controller.Rewrite(new RewriteRequest { Text = "", Tone = "neutral", TranslateTo = null });

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<RewriteResponse>(okResult.Value);
            Assert.Equal(RewrittenText, response.RewrittenText);
        }

        [Fact]
        public async Task Rewrite_WhenRewriterThrows_ReturnsServerError()
        {
            _mockRewriter
                .Setup(r => r.RewriteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Test exception"));

            var result = await _controller.Rewrite(new RewriteRequest { Text = "test", Tone = "neutral", TranslateTo = null });

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);

            _mockLogger.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("Error occurred while rewriting text.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                ),
                Times.Once
            );
        }
    }
}