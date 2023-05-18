using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcToDoList.Data;
using MvcToDoList.Models;
using Rook;
Rook.RookOptions options = new Rook.RookOptions()
{
    token = "86044e31c8627ca8e6fc0c7bb1ef5e2593782db9e3b5f826847dbf20a3b6ba9a",
    labels = new Dictionary<string, string> { { "env", "Amit" } }
};
Rook.API.Start(options);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MvcToDoListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcToDoListContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
    pattern: "{controller=ToDoList}/{action=Index}/{id?}");

app.Run();
