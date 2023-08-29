using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Tower.Classes;
using Tower.Database;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
	JsonConvert.DefaultSettings = () => new JsonSerializerSettings
	{
		Formatting = Newtonsoft.Json.Formatting.Indented,
		ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
	};
});
builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
builder.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddAuthentication().AddCookie(options =>
{
	options.LoginPath = "/Users/login";
	options.AccessDeniedPath = "/Users/AccessDenied";
	options.Cookie.Name = ".AspNet.Sistema.SharedCookie";
	options.Cookie.Path = "/";
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.Cookie.SameSite = SameSiteMode.Lax;
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
}).AddJwtBearer(x =>
{
	var AESKEY = SecurityClass.GenerateKey();
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(AESKEY.Key),
		ValidateIssuer = false,
		ValidateAudience = false,
	};
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("JWT", policy =>
	{
		policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
		policy.RequireAuthenticatedUser();
	});
});
builder.Services.AddSwaggerGen(c =>
{
	c.EnableAnnotations();
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "API de consultas",
	});
	c.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName??x.Name);

	var securySchema = new OpenApiSecurityScheme
	{
		Description = "Token de acesso, coloque no cabeçalho da chamada o token de acesso da mesma maneira que foi gerado na pagina",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Name = "Authorization",
		Reference = new OpenApiReference
		{
			Type = ReferenceType.SecurityScheme,
			Id = "Authorization"
		}
	};
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT header de autorização Authorization usando o Bearer scheme.",
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
						  {
							  Reference = new OpenApiReference
							  {
								  Type = ReferenceType.SecurityScheme,
								  Id = "Bearer"
							  }
						  },
						 new string[] {}
					}
				});

	c.ResolveConflictingActions(x => x.First());

	c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<BDContext>(
dbContextOptions => dbContextOptions
				.UseMySql(BDContext.DefaultConnection, ServerVersion.AutoDetect(BDContext.DefaultConnection))
				.EnableDetailedErrors()
		);

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
app.UseSwagger(c =>
{
	c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(
	options =>
	{
		options.DocumentTitle = "API de consulta";
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
	}
);
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();