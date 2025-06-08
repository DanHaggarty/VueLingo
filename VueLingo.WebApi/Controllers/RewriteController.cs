using AiContentService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VueLingo.WebApi.Models;

namespace VueLingo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewriteController : ControllerBase
    {
        private readonly ITextRewriter _rewriter;

        public RewriteController(ITextRewriter rewriter)
        {
            _rewriter = rewriter;
        }

        [HttpPost]
        public async Task<ActionResult<RewriteResponse>> Rewrite([FromBody] RewriteRequest request)
        {
            var result = await _rewriter.RewriteAsync(request.Text, request.Tone, request.TranslateTo);
            return Ok(new RewriteResponse { RewrittenText = result });
        }
    }
}
