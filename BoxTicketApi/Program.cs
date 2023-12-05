using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.BLL.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using BoxTicketApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BoxTicketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection
        ("ConnectionString: DefaultConnection").Value);
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PerformanceRepository>();
builder.Services.AddScoped<TicketOptionsRepository>();
builder.Services.AddScoped<TicketsRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<GenreRepository>();

builder.Services.AddScoped<ITicketRepository,TicketsRepository>();
builder.Services.AddScoped<ITicketOptionsRepository, TicketOptionsRepository>();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<ITicketOptionsService, TicketOptionsService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
