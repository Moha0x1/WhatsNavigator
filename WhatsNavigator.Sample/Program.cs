using WhatsNavigator.Core.Extensions;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Middleware;
using WhatsNavigator.Infrastructure.Client;
using WhatsNavigator.Infrastructure.Services;
using WhatsNavigator.Infrastructure.State;
using WhatsNavigator.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add framework services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add WhatsNavigator
builder.Services.AddWhatsNavigator(options =>
{
    builder.Configuration.GetSection("WhatsNavigator").Bind(options);
})
.Use<LoggingMiddleware>()
.OnCommand("start", async ctx =>
{
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "¡Bienvenidos al Quiz Bot de WhatsNavigator! 🚀\n\nPrueba los siguientes comandos:\n/quiz - Empezar el quiz\n/help - Ver ayuda");
})
.OnCommand("quiz", async ctx =>
{
    var state = new QuizState(0, 0);
    await ctx.State.SetAsync($"{ctx.Message.SenderNumber}_quiz", state);
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "🚀 ¡Comencemos el Quiz!\n\nPregunta 1: ¿Cuál es el lenguaje de programación de .NET?\nA) Java\nB) C#\nC) Python");
})
.OnMessage(m => m.Text is not null && !m.Text.StartsWith("/"), async ctx =>
{
    var quizState = await ctx.State.GetAsync<QuizState>($"{ctx.Message.SenderNumber}_quiz");
    if (quizState != null)
    {
        int score = quizState.Score;
        if (ctx.Message.Text.Trim().Equals("B", StringComparison.OrdinalIgnoreCase))
        {
            score++;
            await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "✅ ¡Correcto!");
        }
        else
        {
            await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "❌ Incorrecto.");
        }

        await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, $"Finalizaste el quiz. Tu puntaje fue: {score}/1");
        await ctx.State.RemoveAsync($"{ctx.Message.SenderNumber}_quiz");
    }
})
.OnMessage(m => m.Text is not null && m.Text.Contains("hola", StringComparison.OrdinalIgnoreCase), async ctx =>
{
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, $"¡Hola {ctx.Message.SenderNumber}! Soy un bot impulsado por WhatsNavigator.");
});

// Infrastructure registrations
if (builder.Configuration["WhatsNavigator:ApiToken"] == "YOUR_META_ACCESS_TOKEN" || string.IsNullOrEmpty(builder.Configuration["WhatsNavigator:ApiToken"]))
{
    builder.Services.AddSingleton<IWhatsAppClient, ConsoleWhatsAppClient>();
    Console.WriteLine("Running in MOCK mode (Console client). No Meta account needed.");
}
else
{
    builder.Services.AddHttpClient<IWhatsAppClient, WhatsAppClient>();
}

builder.Services.AddSingleton<IWhatsAppState, InMemoryWhatsAppState>();
builder.Services.AddScoped<WhatsAppWebhookProcessor>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

// Define Quiz State
public record QuizState(int QuestionIndex, int Score);
