using Microsoft.EntityFrameworkCore;
using OA.Data.Interface;
using OA.Repo;
using OA.Repo.Repo;
using OA.Service.Implementation;
using OA.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddMvc();
//builder.Services.AddAutoMapper();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IClientContactService, ClientContactService>();

// Configure HttpClient for the service
builder.Services.AddHttpClient<HttpClientService>();

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BCityDB")), ServiceLifetime.Transient);


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationContext>();

    // Apply pending migrations
    await context.Database.MigrateAsync();



}
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
