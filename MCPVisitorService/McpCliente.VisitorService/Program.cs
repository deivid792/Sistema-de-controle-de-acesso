using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;


var builder = WebApplication.CreateBuilder(args);


const string ollamaEndpoint = "http://localhost:11434";
const string ModelName = "llama3.1:latest";

builder.Services.AddChatClient(new OllamaChatClient(new Uri(ollamaEndpoint), ModelName))
    .UseFunctionInvocation();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "MCP_Chat_";
});


builder.Services.AddScoped<ChatHistoryService>();

builder.Services.AddSingleton<McpClient>(sp =>
{
    var transport = new HttpClientTransport(new HttpClientTransportOptions
    {
        Endpoint = new Uri("http://localhost:5000/sse"),
        TransportMode = HttpTransportMode.Sse
    });
    return McpClient.CreateAsync(transport).GetAwaiter().GetResult();
});

builder.Services.AddCors(options => options.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();
app.UseCors();


app.MapPost("/chat", async (ChatRequest request, IChatClient chatClient, McpClient mcpClient, ChatHistoryService historyService) =>
{
    string sessionId = request.UserId ?? "usuario_padrao";

    var messages = await historyService.GetHistoryAsync(sessionId);

    if (messages.Count == 0)
    {
        messages.Add(new ChatMessage(ChatRole.System, """
            Você é um assistente de um Sistema de controle de acessos.
            Seja direto e amigável.
            Após usar uma ferramenta com sucesso, responda apenas confirmando a ação para o usuário,
            sem descrever o processo técnico.
            Ao cadastrar um usúario nunca fale repita os dados dele que voce cadastrou.
            Apenas diga "Voce foi cadastrado com Sucesso :)"
            """));
    }

    messages.Add(new ChatMessage(ChatRole.User, request.Prompt));

    IList<McpClientTool> tools = await mcpClient.ListToolsAsync();
    var options = new ChatOptions { Tools = tools.Cast<AITool>().ToList() };

    var response = await chatClient.GetResponseAsync(messages, options);

    messages.Add(new ChatMessage(ChatRole.Assistant, response.Text ?? ""));
    await historyService.SaveHistoryAsync(sessionId, messages);

    return Results.Ok(new { answer = response.Text });
});

app.Run("http://localhost:5001");

public record ChatRequest(string Prompt, string? UserId);