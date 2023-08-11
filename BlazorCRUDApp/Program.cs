using BlazorCRUDApp.Data;
using BlazorCRUDApp.Globals;
using BlazorCRUDApp.Repositories;
using BlazorCRUDApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorCRUDApp
{
    public class Program
    {
        // Source: https://www.youtube.com/watch?v=-nTJqx0t29I
        // Nået til: 17:30Sql
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            AccessToDb.ConnectionString = builder.Configuration.GetConnectionString("EmplDB");
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

            builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepositoryMock>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}