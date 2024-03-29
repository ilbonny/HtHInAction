﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.AspNetCore.Http;
using HtHInAction.Models;
using HtHInAction.Repositories;
using HtHInAction.Services.Hubs;

namespace HtHInAction
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
            services.AddMvc();
            services.Configure<Settings>(o=>{
                o.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                o.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddTransient<IRepository<Customer>, Repository<Customer>>();
            services.AddTransient<IRepository<EmailSettings>, Repository<EmailSettings>>();
            services.AddTransient<IRepository<MailSentAndReceived>, Repository<MailSentAndReceived>>();
            
            services.AddTransient<IRepository<Mail>, Repository<Mail>>();
            services.AddTransient<IEmailService, EmailService>();
            
            services.AddSignalR();
            services.AddSingleton<IHostedService, Weather>();
            services.AddSingleton<IHostedService, EmailNotifications>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }            

            app.UseSignalR(routes =>
            {
                routes.MapHub<WeatherHub>("weather");
                routes.MapHub<EmailNotificationsHub>("emailNotifications");
             
            });

            app.UseStaticFiles();
            app.UseMvc();

           
        }
    }
}
