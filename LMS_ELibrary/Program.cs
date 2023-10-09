using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//conect db
builder.Services.AddDbContext<LMS_ELibraryContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("strConnect"));

}

); ;

//register service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMonhocService, MonhocService>();
builder.Services.AddScoped<IChudeService, ChudeService>();
builder.Services.AddScoped<ITailieuService, TailieuService>();
builder.Services.AddScoped<ILopgiangService, LopgiangService>();
//builder.Services.AddScoped<IBaigiangService, BaigiangService>();
builder.Services.AddScoped<IDethiService, DethiService>();
builder.Services.AddScoped<ICauhoiService, CauhoiService>();
builder.Services.AddScoped<IThongbaoService, ThongbaoService>();
builder.Services.AddScoped<IHotroService, HotroService>();
builder.Services.AddScoped<IHoidapService, HoidapService>();
builder.Services.AddScoped<ITobomonService, TobomonService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

app.Run();
