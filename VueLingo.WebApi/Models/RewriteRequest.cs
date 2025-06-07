namespace VueLingo.WebApi.Models
{
    public class RewriteRequest
    {
        public string Text { get; set; } = string.Empty;
        public string Tone { get; set; } = "neutral";
    }
}
