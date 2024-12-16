using OllamaSharp.Models.Chat;

namespace BlazorChat.Components.Pages
{
    public partial class Home
    {
        string imageUrl = "https://www.designi.com.br/images/preview/10859604.jpg";
        string context = "";
        string classificationResult = "Resultado";
        string chatResult = "";

        List<Message> messages = new();

        async Task InsectIdentify()
        {
            classificationResult = "...";

            await InvokeAsync(StateHasChanged);

            try
            {
                var input = new InsectVision.ModelInput()
                {
                    ImageSource = await Client.GetByteArrayAsync(imageUrl)
                };

                var classification = InsectVision.Predict(input);

                await InvokeAsync(StateHasChanged);

                if (classification.Score.Max() > 0.7)
                {
                    classificationResult = classification.PredictedLabel;
                    await SendUserPrompt("Considerando a cultura da soja descreva o inseto " + classificationResult);
                }
                else
                {
                    classificationResult = "Não foi possivel identificar";
                }
            }
            catch
            {
                classificationResult = "Erro ao carregar a imagem";
                return;
            }

        }

        async Task SendUserPrompt(string ctx)
        {
            chatResult = "";

            messages.Add(new()
            {
                Role = ChatRole.User,
                Content = ctx
            });

            await foreach (var item in Chat.ChatAsync(new()
            {
                Messages = messages
            }))
            {
                chatResult += item?.Message.Content;
                await InvokeAsync(StateHasChanged);
            };
        }

        void AddSystemContext()
        {
            messages.Add(new()
            {
                Role = ChatRole.System,
                Content = context
            });

            context = "";
        }

        void CleanContext()
        {
            messages = new();
        }
    }
}
