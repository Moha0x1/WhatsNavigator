# WhatsNavigator 🚀

**WhatsNavigator** is a lightweight, opinionated, and production-ready WhatsApp bot framework for .NET. Inspired by Telegram bot frameworks like Navigator, it simplifies the interaction with Meta's WhatsApp Cloud API.

## Features
- ✅ **Minimal Setup**: Get started with just a few lines of code.
- ✅ **Fluent Routing**: Easy to define command and message handlers.
- ✅ **Middleware Support**: Intercept and process messages before they reach your handlers.
- ✅ **State Management**: Built-in support for InMemory and Redis state.
- ✅ **Mock Mode**: Develop and test locally without a Meta account.

## Quick Start (Sample)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWhatsNavigator(options => {
    options.ApiToken = "YOUR_TOKEN";
    options.PhoneNumberId = "YOUR_ID";
})
.Use<LoggingMiddleware>()
.OnCommand("start", async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "Welcome to WhatsNavigator! 🚀");
})
.OnMessage(m => m.Text != null && m.Text.Contains("hello"), async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "Hi! How can I help you?");
});

var app = builder.Build();
app.MapControllers();
app.Run();
```

## Local Development (Simulating WhatsApp)

If you don't have a Meta account yet, WhatsNavigator runs in **MOCK mode** by default if the API Token is empty. You can test your bot using the provided `simulator.ps1` script:

```powershell
.\simulator.ps1 -Msg "/start"
```

## Contributing

Created with ❤️ by [Moha0x1](https://github.com/Moha0x1).
