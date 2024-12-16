using Markdig;
using BlazorChat.Interface;

namespace BlazorChat.Service
{
    public class MarkdownService : IMarkdownService
    {
        public string ConvertMarkdownToHtml(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseBootstrap()
                .UseSmartyPants()
                .Build();

            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
