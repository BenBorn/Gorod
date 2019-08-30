using System;
using GreenPipes;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Consumers;

namespace OrderManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMassTransit(
                provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                 {
                     var hostUri = new UriBuilder(
                         "rabbitmq",
                         "localhost",
                         5673);
                     var host = cfg.Host(hostUri.Uri, hostConfiguration =>
                     {
                         hostConfiguration.Username("guest");
                         hostConfiguration.Password("guest");
                     });

                     cfg.ReceiveEndpoint(host, "OrderManagerQueue", ep =>
                     {
                         ep.PrefetchCount = 1;
                         ep.UseMessageRetry(r => r.Interval(2, 100));

                         // Add consumers also here:
                         ep.ConfigureConsumer<DeleteOrderConsumer>(provider);
                     });
                 }),
                config => { config.AddConsumer<DeleteOrderConsumer>(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
