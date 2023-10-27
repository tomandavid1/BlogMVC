using Blog.Web.Data;
using Blog.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inject DB context inside services of application
builder.Services.AddDbContext<BlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnectionString")));

// Add injection to services - when calling ITagRepository, give TagRepository
builder.Services.AddScoped<ITagRepository, TagRepository>();
// Add injection to services - when calling IBlogPostRepository, give BlogPostRepository
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

// Breadcrumbs


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");

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
