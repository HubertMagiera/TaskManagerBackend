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

var builder = WebApplication.CreateBuilder(args);

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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
