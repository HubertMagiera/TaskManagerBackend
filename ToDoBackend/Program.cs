using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoBackend;
using ToDoBackend.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ToDoBackend.Entities.DTO_Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using ToDoBackend.Authorization;

var builder = WebApplication.CreateBuilder(args);

//read authentication settings from appsettings.json and bind them to appropriate class
AuthenticationSettings settings = new AuthenticationSettings();
builder.Configuration.GetSection("Jwt").Bind(settings);
builder.Services.AddSingleton(settings);
//configure authentication with bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{   options.SaveToken = true;
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = settings.Issuer,
        ValidAudience = settings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
    }; 
    });
//Add dbcontext class to the container
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("ConnectionString")));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add services for the controllers
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
//add classes which run authorization rules
builder.Services.AddScoped<IAuthorizationHandler, Task_Owner_Reqiurement_Handler>();
builder.Services.AddHttpContextAccessor();
//add hasher for password hashing
builder.Services.AddScoped<IPasswordHasher<User_DTO>,PasswordHasher<User_DTO>>();
//add auto mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//add middleware
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//add middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

//add authentication
//for implementing jwt token authentication, both app.UseAuthentication() and app.UseAuthorization()
//are reqiured, but useAuthentication needs to be before useAuthorization
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
