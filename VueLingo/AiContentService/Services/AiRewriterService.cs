using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AiContentService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AiContentService.Services
{
    /// <summary>
    /// Generic AI-based text rewriter service.
    /// 
    /// NOTE: This implementation currently uses OpenAI GPT-3.5 API under the hood.
    /// The class name is generic to allow future replacement without breaking consumers.
    /// </summary>
    public class AiRewriterService : ITextRewriter
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AiRewriterService"/> class.
        /// </summary>
        /// <param name="httpClient">The injected HTTP client used to make API requests.</param>
        /// <param name="config">The configuration source used to read the API key.</param>
        /// <exception cref="ArgumentNullException">Thrown if API key is missing from config.</exception>
        public AiRewriterService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OpenAI:ApiKey"] ?? throw new ArgumentNullException("OpenAI:ApiKey");
        }

        /// <summary>
        /// Rewrites the input text using AI, styled with the specified tone, and optionally translates it to a target language.
        /// </summary>
        /// <param name="text">The original input text to be rewritten.</param>
        /// <param name="tone">The tone or style to apply to the rewritten text (e.g., formal, casual, SEO).</param>
        /// <param name="translateTo">
        /// Optional. The language into which the rewritten text should be translated (e.g., "French", "German").
        /// If null or empty, no translation is applied.
        /// </param>
        /// <returns>A string containing the AI-rewritten (and optionally translated) content.</returns>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request to the AI service fails.</exception>
        public async Task<string> RewriteAsync(string text, string tone, string? translateTo = null)
        {
            var prompt = $"Rewrite the following text in a {tone} tone:\n\n{text}";

            if (!string.IsNullOrWhiteSpace(translateTo))
            {
                prompt += $"\n\nThen translate it into {translateTo}.";
            }

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "You are a helpful content rewriter." },
            new { role = "user", content = prompt }
        },
                temperature = 0.7
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var json = await JsonDocument.ParseAsync(contentStream);

            return json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()
                ?? "[No AI response]";
        }


        ///// <summary>
        ///// Rewrites the input text using AI, styled with the specified tone.
        ///// </summary>
        ///// <param name="text">The original input text.</param>
        ///// <param name="tone">The tone in which to rewrite (e.g., friendly, formal, SEO).</param>
        ///// <returns>A rewritten version of the input text.</returns>
        //public async Task<string> RewriteAsync(string text, string tone, string? translateTo = null)
        //{
        //    var prompt = $"Rewrite the following text in a {tone} tone:\n\n{text}";

        //    if (!string.IsNullOrWhiteSpace(translateTo))
        //    {
        //        prompt += $"\n\nThen translate it into {translateTo}.";
        //    }

        //    var payload = new
        //    {
        //        model = "gpt-3.5-turbo",
        //        messages = new[]
        //        {
        //            new { role = "system", content = "You are a helpful content rewriter." },
        //            new { role = "user", content = prompt }
        //        },
        //        temperature = 0.7
        //    };

        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        //    request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();

        //    using var contentStream = await response.Content.ReadAsStreamAsync();
        //    using var json = await JsonDocument.ParseAsync(contentStream);

        //    return json.RootElement
        //        .GetProperty("choices")[0]
        //        .GetProperty("message")
        //        .GetProperty("content")
        //        .GetString()
        //        ?? "[No AI response]";
        //}
    }
}
