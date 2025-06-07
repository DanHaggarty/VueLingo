using Microsoft.AspNetCore.Mvc;
using VueLingo.WebApi.Models;

namespace VueLingo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewriteController : ControllerBase
    {
        [HttpPost]
        public ActionResult<RewriteResponse> Rewrite([FromBody] RewriteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text) || string.IsNullOrWhiteSpace(request.Tone))
                return BadRequest("Text and Tone are required.");

            // Mock "AI" rewriting logic
            var rewritten = $"[🔁 {request.Tone.ToUpper()} version] {request.Text}";

            var response = new RewriteResponse
            {
                RewrittenText = rewritten
            };

            return Ok(response);
        }
    }
}
