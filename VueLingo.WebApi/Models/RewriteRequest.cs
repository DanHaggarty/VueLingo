namespace VueLingo.WebApi.Models
{
    /// <summary>
    /// Represents a request to rewrite content with an optional tone and translation.
    /// </summary>
    public class RewriteRequest
    {
        /// <summary>
        /// The original text to be rewritten.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The desired tone of the rewritten content (e.g., "formal", "casual").
        /// Defaults to "neutral".
        /// </summary>
        public string Tone { get; set; } = "neutral";

        /// <summary>
        /// The target language code for translation (e.g., "es", "fr").
        /// If null, no translation is performed.
        /// </summary>
        public string? TranslateTo { get; set; }
    }
    
}
