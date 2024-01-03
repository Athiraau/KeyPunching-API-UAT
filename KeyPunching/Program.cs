using Bussiness.Contracts;
using Bussiness.Services;
using Bussiness.Services.GetPunch;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using DataAccess.Repository;
using FluentValidation;
using KeyPunching.Extensions;
using KeyPunching.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);

builder.Logging.AddNLog(logPath).SetMinimumLevel(LogLevel.Trace);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IServiceWrapper, ServiceWrapper>();
//builder.Services.AddSingleton<IServiceWrapper, ServiceWrapper>();   
builder.Services.AddTransient<KeyPunchGetDetails>();
builder.Services.AddTransient<DtoWrapper>();
builder.Services.AddTransient<ServiceHelper>();
builder.Services.AddTransient<ErrorResponse>();
builder.Services.AddTransient<IKeyPunchService, KeyPunchService>();
//builder.Services.AddTransient<KeyPunchEmpGetDto, KeyPunchEmpGetDto>();
builder.Services.AddTransient<KeyPunchReqDto>();

builder.Services.AddTransient<IValidator<KeyPunchEmpGetDto>,KeyPunchValidator>();

builder.Services.AddTransient<IValidator<KeyPunchReqDetailsDto>, KeyPunchPostValidator>();

builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true);
}));


//    .AddNewtonsoftJson(x =>
//    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    // This middleware serves the Swagger documentation UI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Punching API V1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();


app.UseRouting();

app.UseCors("MyPolicy");
app.UseMiddleware<CorsMiddleware>();

//app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddlewareExtension>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
