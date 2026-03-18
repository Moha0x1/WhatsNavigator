# 🧭 WhatsNavigator

> **The Ultimate WhatsApp Bot Framework for .NET 9 (Downgraded to 8.0 for ultimate compatibility)**

WhatsNavigator es un framework **opinionado**, potente y extremadamente sencillo de usar, diseñado específicamente para dominar la **WhatsApp Cloud API** de Meta. No es solo un cliente; es el motor que permite construir bots de producción con una arquitectura limpia y una experiencia de desarrollo fluida.

---

## ✨ Características que lo hacen único

*   🚀 **Fluent Routing**: Registra comandos y manejadores con una sintaxis que fluye de forma natural.
*   🛡️ **Middleware Middleware**: Inyecta lógica de auditoría, logging o seguridad global en nanosegundos.
*   🧠 **State Management Nativo**: Soporte *out-of-the-box* para gestionar conversaciones complejas (InMemory y Redis).
*   🎭 **Mock & Simulation**: Desarrolla localmente sin cables, sin API Keys y sin cuenta de Meta.
*   🔌 **Inyección de Dependencias**: Perfectamente integrado con el ecosistema de `IServiceCollection`.

---

## 🏎️ Inicio Rápido

Configura tu bot en menos de 1 minuto:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configuración de WhatsNavigator
builder.Services.AddWhatsNavigator(options => {
    options.ApiToken = "MY_META_TOKEN";
    options.PhoneNumberId = "PHONE_ID";
})
.Use<LoggingMiddleware>() // Middleware global
.OnCommand("ping", async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "🏓 Pong!");
})
.OnMessage(m => m.Text?.Contains("comprar") == true, async ctx => {
    await ctx.Client.SendTextAsync(ctx.Message.SenderNumber, "🛒 ¿Qué te gustaría comprar?");
});

var app = builder.Build();
app.MapControllers(); // Expone el endpoint /webhook automáticamente
app.Run();
```

---

## 🛠️ Sandbox Local (Sin Cuenta de Meta)

¿No tienes cuenta de Meta? **No hay problema.** WhatsNavigator detecta si no hay Token y activa el **modo consola** automáticamente. 

Usa nuestro simulador incluido:
```powershell
.\simulator.ps1 -Msg "Hola! Quiero probar el bot"
```

---

## 📋 Roadmap & Tareas (Tareas por hacer)

¿Quieres contribuir? Aquí tienes lo que hemos hecho y lo que soñamos para el futuro:

### ✅ Finalizado (Available Now)
- [x] Motor de Routing principal (Commands/Messages).
- [x] Pipeline de Middlewares.
- [x] Soporte para State (InMemory / Redis).
- [x] Cliente HTTP para mensajes de texto y plantillas.
- [x] Webhook Controller automatizado.
- [x] Modo de simulación local (MOCK CLI).

### ⏳ Próximamente (Next Steps)
- [ ] **Soporte Multimedia**: Envía y recibe Imágenes, PDFs, Vídeos y Cadenas de Audio.
- [ ] **Mensajes Interactivos**: Soporte nativo para Botones (`Reply Buttons`) y Listas.
- [ ] **Seguridad Avanzada**: Verificación automática de la firma de seguridad de Meta (X-Hub-Signature).
- [ ] **Persistencia Extendida**: Adaptadores para SQL Server, MongoDB y CosmosDB.
- [ ] **Documentación**: Generador automático de esquemas de conversación.

---

## 🤝 Contribuciones

Este es un proyecto abierto. Si tienes una idea chula o quieres arreglar un bug, ¡abre un PR o un Issue! 

Hecho por [Moha0x1](https://github.com/Moha0x1).
