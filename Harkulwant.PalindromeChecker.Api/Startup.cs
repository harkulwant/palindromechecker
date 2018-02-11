using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Harkulwant.PalindromeChecker.Api
{
    public class Startup
    {
        public IContainer ApplicationContaier { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Palindrome Checker API",
                    Version = "v1",
                    Description = "",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Harkulwant Hundal", Email = "harkulwant@gmail.com" }
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Harkulwant.PalindromeChecker.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    corsBuilder => corsBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });

            var builder = new ContainerBuilder();
            builder.RegisterModule<Service.AutofacModule>();
            builder.RegisterModule(new Repository.AutofacModule(Configuration));

            builder.Populate(services);

            ApplicationContaier = builder.Build();
            var serviceProvider = ApplicationContaier.Resolve<IServiceProvider>();
            return new AutofacServiceProvider(ApplicationContaier);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Palindrome V1");
            });

            // adding nlog to Asp.Net Core
            loggerFactory.AddNLog();
            
            // needed for non-NETSTANDARD platforms: configure nlog.config in your project root
            env.ConfigureNLog("nlog.config");

            // Shows UseCors with named policy
            app.UseCors("AllowAll");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
