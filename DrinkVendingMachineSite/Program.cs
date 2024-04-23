using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.DataAccess.DatabaseContext;
using DrinkVendingMachine.DataAccess.Implementations.Repositories.EF;
using DrinkVendingMachine.DataAccess.Implementations.UnitOfWork;
using DrinkVendingMachine.Services.Implementations.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MSSQLSERVER settings
builder.Services.AddDbContext<DrinkVendingMachineDBContext>(options
    => options.UseSqlServer(("DatabaseConnection"), b => b.MigrationsAssembly("DrinkVendingMachine.API")));

//Repositories
builder.Services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(EFGenericRepositoryAsync<>));
builder.Services.AddTransient<IDrinkEntitiesRepositoryAsync, EFDrinkEntitiesRepositoryAsync>();
builder.Services.AddTransient<ICoinEntitiesRepositoryAsync, EFCoinEntitiesRepositoryAsync>();

//Unit of Work
builder.Services.AddTransient<IUnitOfWorkAsync, EFUnitOfWorkAsync>();

//Services
builder.Services.AddTransient(typeof(IGenericServiceAsync<>), typeof(GenericServiceAsync<>));
builder.Services.AddTransient<IDrinkEntitiesServiceAsync, DrinkEntitiesServiceAsync>();
builder.Services.AddTransient<ICoinEntitiesServiceAsync, CoinEntitiesServiceAsync>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
