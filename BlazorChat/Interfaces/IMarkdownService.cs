namespace BlazorChat.Interface
{
    public interface IMarkdownService
    {
        public string ConvertMarkdownToHtml(string markdown);
    }
}
