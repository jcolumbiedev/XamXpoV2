using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIT.Xpo.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BitServer
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
            //HACK Xpo asp core extensions https://www.devexpress.com/Support/Center/Question/Details/T637597/asp-net-core-dependency-injection-in-xpo
            //HACK https://documentation.devexpress.com/XPO/119377/Getting-Started/Getting-Started-with-NET-Core
            //HACK how to read connection strings https://stackoverflow.com/questions/45796776/get-connectionstring-from-appsettings-json-instead-of-being-hardcoded-in-net-co

            DataStoreResolver DataStoreResolver = new DataStoreResolver();
            services.AddSingleton<IDataStoreResolver>(DataStoreResolver);
            services.AddMemoryCache();
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
