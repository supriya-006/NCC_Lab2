var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Dependency Injection Lifetime Services
builder.Services.AddTransient<WebApp3BySupriya.Services.ITransientService, WebApp3BySupriya.Services.OperationService>();
builder.Services.AddScoped<WebApp3BySupriya.Services.IScopedService, WebApp3BySupriya.Services.OperationService>();
builder.Services.AddSingleton<WebApp3BySupriya.Services.ISingletonService, WebApp3BySupriya.Services.OperationService>();
builder.Services.AddTransient<WebApp3BySupriya.Services.DILifetimeDemoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
