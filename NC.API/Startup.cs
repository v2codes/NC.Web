using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NC.Identity;
using NC.Model;
using NC.Model.Repository;

namespace NC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            // add cors
            AddCors(services);

            // add for identity
            AddIdentityDbContext(services);

            // add ef core
            AddEfDbContext(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // add Autofac 第三方容器接管Core的默认DI，需修改当前方法返回值为 IServiceProvider
            // return AddAutoFac(services);

            #region 默认DI
            //services.addtr
            #endregion
        }

        #region add cors
        private void AddCors(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", policy =>
             {
                 policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
             }));
        }
        #endregion

        #region add identity
        private void AddIdentityDbContext(IServiceCollection services)
        {
            string defaultConnection = Configuration.GetConnectionString("DefaultConnection");

            // add EF and IdentityFramework
            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(defaultConnection));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationUserDbContext>();

            // add authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = this.Configuration["Tokens:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = this.Configuration["Tokens:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true

                };
            });
        }
        #endregion

        #region add ef core
        private void AddEfDbContext(IServiceCollection services)
        {
            string defaultConnection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CTX>(options =>
            {
                options.UseSqlServer(defaultConnection);
                // TODO
                //options.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            });
        }
        #endregion

        #region add AutoFac
        // Creates, wires dependencies and manages lifetime for a set of components. Most instances of Autofac.IContainer are created by a Autofac.ContainerBuilder.
        public IContainer ApplicationContainer { get; set; }
        private IServiceProvider AddAutoFac(IServiceCollection services)
        {
            var _autoFacBuilder = new ContainerBuilder();
            _autoFacBuilder.Populate(services);
            ApplicationContainer = _autoFacBuilder.Build();
            // 注册仓储泛型
            _autoFacBuilder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            // 让第三方容器接管Core 的默认DI
            return new AutofacServiceProvider(ApplicationContainer);
        }
        #endregion

        #region configure the HTTP request pipeline 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("Cors");
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions()
                {

                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}"
                //);
            });
        }
        #endregion
    }
}
