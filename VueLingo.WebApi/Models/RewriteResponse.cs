namespace VueLingo.WebApi.Models
{
    /// <summary>
    /// Represents the response containing the rewritten text from the AI service.
    /// </summary>
    public class RewriteResponse
    {
        /// <summary>
        /// The rewritten version of the original input text.
        /// </summary>
        public string RewrittenText { get; set; } = string.Empty;
    }

}
