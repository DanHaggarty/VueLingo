using AiContentService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VueLingo.WebApi.Models;

namespace VueLingo.WebApi.Controllers
{
    /// <summary>
    /// API controller for rewriting text content using a specified tone and optional translation.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RewriteController : ControllerBase
    {
        private readonly ITextRewriter _rewriter;
        private readonly ILogger<RewriteController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RewriteController"/> class.
        /// </summary>
        /// <param name="rewriter">Service responsible for rewriting text.</param>
        /// <param name="logger">Logger instance for this controller.</param>
        public RewriteController(ITextRewriter rewriter, ILogger<RewriteController> logger)
        {
            _rewriter = rewriter;
            _logger = logger;
        }

        /// <summary>
        /// Rewrites the provided text based on the specified tone and optional translation language.
        /// </summary>
        /// <param name="request">The rewrite request containing the original text, tone, and optional language.</param>
        /// <returns>A <see cref="RewriteResponse"/> containing the rewritten text.</returns>
        [HttpPost]
        public async Task<ActionResult<RewriteResponse>> Rewrite([FromBody] RewriteRequest request)
        {
            _logger.LogInformation("Received rewrite request. Text: {Text}, Tone: {Tone}, TranslateTo: {TranslateTo}",
                request.Text, request.Tone, request.TranslateTo);

            try
            {
                var result = await _rewriter.RewriteAsync(request.Text, request.Tone, request.TranslateTo);

                _logger.LogInformation("Rewrite completed. Result: {Result}", result);

                return Ok(new RewriteResponse { RewrittenText = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while rewriting text.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
