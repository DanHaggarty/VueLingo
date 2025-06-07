namespace AiContentService.Interfaces
{
    public interface ITextRewriter
    {
        /// <summary>
        /// Rewrites the given text in the specified tone (e.g. formal, casual, SEO).
        /// </summary>
        /// <param name="text">The input text to be rewritten.</param>
        /// <param name="tone">The desired tone/style of the rewritten output.</param>
        /// <returns>Rewritten string from the AI model.</returns>
        Task<string> RewriteAsync(string text, string tone);
    }
}
