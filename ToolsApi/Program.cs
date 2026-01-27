
using Scalar.AspNetCore;

namespace ToolsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddOpenApi();



            
            
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

            app.Run();
        }
    }
}
