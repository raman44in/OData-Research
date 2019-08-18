using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using WebAppCoreEF2.Models;

namespace WebAppCoreEF2
{
    public class Startup
    {
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Course>("Course").EntityType.Ignore(ui => ui.Title);
            builder.EntitySet<Course>("Course");

            builder.EntitySet<Person>("Person");
            builder.EntitySet<Vw_ExtendedPerson>("vw_ExtendedPerson").EntityType.HasKey(item => item.PersonId);

            return builder.GetEdmModel();
        }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<SchoolExtendedContext>(options => options.UseSqlServer("Server=LAPTOP-391R7HE2;Database=SchoolExtended;Trusted_Connection=True;"));
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer("Server=LAPTOP-391R7HE2;Database=School;Trusted_Connection=True;"));
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
            app.UseMvc(b =>
            {
                b.EnableDependencyInjection();
                b.Select().Expand().Filter().OrderBy().MaxTop(100).Count(QueryOptionSetting.Allowed);
                b.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
    }
}
