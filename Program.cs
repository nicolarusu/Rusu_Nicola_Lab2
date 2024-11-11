using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Rusu_Nicola_Lab2.Data; // Replace with your actual namespace for Rusu_Nicola_Lab2Context
using Rusu_Nicola_Lab2.Models; // Replace with the actual namespace for LibraryIdentityContext

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});


// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});

// Configure the main application DbContext (Rusu_Nicola_Lab2Context)
builder.Services.AddDbContext<Rusu_Nicola_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Rusu_Nicola_Lab2Context")
    ?? throw new InvalidOperationException("Connection string 'Rusu_Nicola_Lab2Context' not found.")));

// Configure the second DbContext (Nume_Pren_Lab2Context)
builder.Services.AddDbContext<Rusu_Nicola_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Rusu_Nicola_Lab2Context")
    ?? throw new InvalidOperationException("Connection string 'Nume_Pren_Lab2Context' not found.")));

// Configure Identity with the LibraryIdentityContext for user management
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Rusu_Nicola_Lab2Context")
    ?? throw new InvalidOperationException("Connection string 'using Microsoft.EntityFrameworkCore;\r\nusing Microsoft.Extensions.DependencyInjection;\r\nusing Microsoft.AspNetCore.Identity;\r\nusing Rusu_Nicola_Lab2.Data; // Replace with your actual namespace for Rusu_Nicola_Lab2Context\r\nusing Rusu_Nicola_Lab2.Models; // Replace with the actual namespace for LibraryIdentityContext\r\n\r\nvar builder = WebApplication.CreateBuilder(args);\r\n\r\n// Add services to the container.\r\nbuilder.Services.AddRazorPages();\r\n\r\n// Configure the main application DbContext (Rusu_Nicola_Lab2Context)\r\nbuilder.Services.AddDbContext<Rusu_Nicola_Lab2Context>(options =>\r\n    options.UseSqlServer(builder.Configuration.GetConnectionString(\"Rusu_Nicola_Lab2Context\")\r\n    ?? throw new InvalidOperationException(\"Connection string 'Rusu_Nicola_Lab2Context' not found.\")));\r\n\r\n// Configure the second DbContext (Nume_Pren_Lab2Context)\r\nbuilder.Services.AddDbContext<Rusu_Nicola_Lab2Context>(options =>\r\n    options.UseSqlServer(builder.Configuration.GetConnectionString(\"Rusu_Nicola_Lab2Context\")\r\n    ?? throw new InvalidOperationException(\"Connection string 'Nume_Pren_Lab2Context' not found.\")));\r\n\r\n// Configure Identity with the LibraryIdentityContext for user management\r\nbuilder.Services.AddDbContext<LibraryIdentityContext>(options =>\r\n    options.UseSqlServer(builder.Configuration.GetConnectionString(\"Nume_Pren_Lab2Context\")\r\n    ?? throw new InvalidOperationException(\"Connection string 'Nume_Pren_Lab2Context' not found.\")));\r\n\r\n// Configure Identity services\r\nbuilder.Services.AddDefaultIdentity<IdentityUser>(options =>\r\n    options.SignIn.RequireConfirmedAccount = true)\r\n    .AddEntityFrameworkStores<LibraryIdentityContext>();\r\n\r\nvar app = builder.Build();\r\n\r\n// Configure the HTTP request pipeline.\r\nif (!app.Environment.IsDevelopment())\r\n{\r\n    app.UseExceptionHandler(\"/Error\");\r\n    app.UseHsts();\r\n}\r\n\r\napp.UseHttpsRedirection();\r\napp.UseStaticFiles();\r\n\r\napp.UseRouting();\r\n\r\napp.UseAuthorization();\r\n\r\napp.MapRazorPages();\r\n\r\napp.Run();\r\nContext' not found.")));

// Configure Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
