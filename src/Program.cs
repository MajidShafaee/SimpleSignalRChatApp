using Microsoft.AspNetCore.SignalR;
using SimpleSignalRChatApp.Hubs;
using SimpleSignalRChatApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


//Options can be configured for all hubs by providing an options delegate to the AddSignalR
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
    hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
    
});

builder.Services.AddSingleton<IChatRoomService, InMemoryChatRoomService>();


var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Options for a single hub override the global options
app.MapHub<ChatHub>("/chatHub", opt =>
{
    opt.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);  
    
});
app.MapHub<AgentHub>("/agentHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
