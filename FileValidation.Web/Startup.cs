using AutoMapper;
using FileValidation.Mediatr;
using FileValidation.Mediatr.AutoMapper;
using FileValidation.Services.Abstraction;
using FileValidation.Services.Implementation;
using FileValidation.Web.AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddControllers();

            var mapperConfig = new MapperConfiguration((ex) =>
            {
                ex.AddProfile<DtoMediatrModelsProfile>();
                ex.AddProfile<MediatrServiceModelsProfile>();
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddSingleton<IRegexValidationService, RegexValidationService>();
            services.AddScoped<IFileValidationService, FileValidationService>();
            services.AddMediatR(typeof(IAssemblyMarker));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
