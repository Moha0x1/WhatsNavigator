# 🧭 WhatsNavigator

> **The Ultimate WhatsApp Bot Framework for .NET 8.0+**

WhatsNavigator is an **opinionated**, powerful, and extremely easy-to-use framework designed specifically to master the **Meta WhatsApp Cloud API**. It's not just a client; it's the engine that allows you to build production-ready bots with a clean architecture and a fluid developer experience.

---

## ✨ Features that make it unique

*   🚀 **Fluent Routing**: Register commands and handlers with a syntax that flows naturally.
*   🛡️ **Middleware Pipeline**: Inject audit, logging, or global security logic in nanoseconds.
*   🧠 **Native State Management**: Out-of-the-box support for managing complex conversations (InMemory and Redis).
*   🎭 **Mock & Simulation**: Develop locally without cables, API Keys, or a Meta account.
*   🔌 **Dependency Injection**: Perfectly integrated with the `IServiceCollection` ecosystem.

---

## 🏎️ Quick Start

Set up your bot in less than 1 minute:

```csharp
var builder = WebApplication.CreateBuilder(args);

// WhatsNavigator Configuration
builder.Services.AddWhatsNavigator(options => {
    options.ApiToken = "MY_META_TOKEN";
    options.PhoneNumberId = "PHONE_ID";
})
.Use<LoggingMiddleware>() // Global middleware
.OnCommand("ping", async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "🏓 Pong!");
})
.OnMessage(m => m.Text?.Contains("buy") == true, async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "🛒 What would you like to buy?");
});

var app = builder.Build();
app.MapControllers(); // Automatically exposes the /webhook endpoint
app.Run();
```

---

## 🛠️ Local Sandbox (No Meta Account Required)

Don't have a Meta account? **No problem.** WhatsNavigator detects if there's no Token and automatically activates **Console Mock Mode**.

Use our included simulator:
```powershell
.\simulator.ps1 -Msg "Hello! I want to test the bot"
```

---

## 📋 Roadmap (To-Do List)

Want to contribute? Here's what we've built and what we dream of for the future:

### ✅ Completed (Available Now)
- [x] Primary Routing Engine (Commands/Messages).
- [x] Middleware Pipeline.
- [x] State Management Support (InMemory / Redis).
- [x] HTTP Client for text messages and templates.
- [x] Automated Webhook Controller.
- [x] Local Simulation Mode (MOCK CLI).

### ⏳ Coming Soon (Next Steps)
- [ ] **Multimedia Support**: Send and receive Images, PDFs, Videos, and Audio.
- [ ] **Interactive Messages**: Native support for Reply Buttons and Lists.
- [ ] **Advanced Security**: Automatic signature verification (X-Hub-Signature).
- [ ] **Extended Persistence**: Adapters for SQL Server, MongoDB, and CosmosDB.
- [ ] **Documentation**: Automatic conversation flow generator.

---

## 🤝 Contributions

This is an open project. If you have a cool idea or want to fix a bug, open a PR or an Issue!

Created by [Moha0x1](https://github.com/Moha0x1).
