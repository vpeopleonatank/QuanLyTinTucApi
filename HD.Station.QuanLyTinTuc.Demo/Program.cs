using Microsoft.EntityFrameworkCore;

using HD.Station.QuanLyTinTuc.SqlServer.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;
using HD.Station.QuanLyTinTuc.Abstractions.Services;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuanLyTinTucDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLTTContext")
      ?? throw new InvalidOperationException("Connection string 'QLTTContext' not found.")));

builder.Services.AddTransient<INewsStore, NewsStore>();
builder.Services.AddTransient<INewsService, NewsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
