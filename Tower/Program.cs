using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tower.Classes;
using Tower.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
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
    var key = SecurityClass.GenerateKey();
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key.Key),
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
    // Habilita a documenta��o de anota�oes do Swagger
    c.EnableAnnotations();

    // Define a informa��o b�sica da API
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de consultas",
    });

    var securySchema = new OpenApiSecurityScheme
    {
        Description = "Token de acesso, coloque no cabe�alho da chamada o token de acesso da mesma maneira que foi gerado na pagina",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Token",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Token"
        },
    };
    c.AddSecurityDefinition("Token", securySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securySchema, new[] { "Token" } }
            });

    // Configura��oo para evitar conflitos de rotas
    c.ResolveConflictingActions(x => x.First());

    // Habilita as anota��es para heran�a e polimorfismo
    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

    // Configura��o para incluir a documenta��o XML da API
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
