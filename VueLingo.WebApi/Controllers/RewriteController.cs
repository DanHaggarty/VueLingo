using AiContentService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VueLingo.WebApi.Models;

namespace VueLingo.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// API controller for rewriting text content using a specified tone and optional translation.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RewriteController : ControllerBase
    {
        private readonly ITextRewriter _rewriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="RewriteController"/> class.
        /// </summary>
        /// <param name="rewriter">Service responsible for rewriting text.</param>
        public RewriteController(ITextRewriter rewriter)
        {
            _rewriter = rewriter;
        }

        /// <summary>
        /// Rewrites the provided text based on the specified tone and optional translation language.
        /// </summary>
        /// <param name="request">The rewrite request containing the original text, tone, and optional language.</param>
        /// <returns>A <see cref="RewriteResponse"/> containing the rewritten text.</returns>
        [HttpPost]
        public async Task<ActionResult<RewriteResponse>> Rewrite([FromBody] RewriteRequest request)
        {
            var result = await _rewriter.RewriteAsync(request.Text, request.Tone, request.TranslateTo);
            return Ok(new RewriteResponse { RewrittenText = result });
        }
    }

}
