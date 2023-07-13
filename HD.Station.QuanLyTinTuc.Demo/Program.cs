using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using HD.Station.QuanLyTinTuc.SqlServer.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.Services;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Security;
using HD.Station.QuanLyTinTuc.Mvc.Filters;
using HD.Station.QuanLyTinTuc.Demo.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddAbstractions();
builder.Services.AddDbContext<QuanLyTinTucDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLTTContext")
      ?? throw new InvalidOperationException("Connection string 'QLTTContext' not found.")));

builder.Services.AddTransient<INewsStore, NewsStore>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ISlugifier, Slugifier>();

builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add(typeof(ApiExceptionFilterAttribute));
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<JwtOptionsSetup>()
    .ConfigureOptions<JwtBearerOptionsSetup>()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
  ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
      var quanLyTinTucDbContext = services.GetRequiredService<QuanLyTinTucDbContext>();
      var articles = await quanLyTinTucDbContext.Articles.ToListAsync();
      Console.WriteLine(articles[0].Slug);

  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.ToString());
  }
}

app.Run();
