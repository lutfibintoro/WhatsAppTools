
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToolsApi.Data;
using ToolsApi.Extensions;

namespace ToolsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllersWithViews();
            builder.Services.AddApplicationScopeService();

            builder.Services.AddDbContext<ToolsApiDbContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AivenProviderMySQL")!));





            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler("/exception");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseWebSockets(new WebSocketOptions() { KeepAliveInterval = TimeSpan.FromMinutes(2) });
            app.MapControllers();
            app.MapEndpoints();

            app.Run();
        }
    }
}
