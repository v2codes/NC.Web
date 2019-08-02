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
using NC.Core.Database;
using NC.Core.Helper;
using NC.Core.IoC;
using NC.Core.Repositories;
using NC.Identity;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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
            //AddDI(services);
            #endregion

            #region AutoFac
            var provider = AddAutoFac(services);
            return provider;
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

        #region DI

        private void AddDI(IServiceCollection services)
        {
            //// 手动注入
            //services.AddSingleton(Configuration)
            //        .AddScoped(typeof(IRepository<Blog, Guid>), typeof(BlogRepository));
            //services.AddScoped(typeof(IService<Blog, Guid>), typeof(BlogService));

            //// 反射批量注入仓储类、服务类
            //services.AddReposAndServices();

            // 反射批量注入仓储类、服务类（需标记特性DIAttribute）
            services.AddReposAndServicesByAttribute();
        }
        #endregion

        #region AutoFac

        // autofac 单例IoC容器
        public IContainer ApplicationContainer { get; set; }
        private IServiceProvider AddAutoFac(IServiceCollection services)
        {
            // 新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            builder.Populate(services);
            //自定义注册组件,注册泛型仓储
            MyBuild(builder);
            // func?.Invoke(builder);

            // 利用构建器创建容器
            ApplicationContainer = builder.Build();

            // 让第三方容器接管Core 的默认DI
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public static void MyBuild(ContainerBuilder builder)
        {
            var assemblies = ReflectionHelper.GetAllAssemblies().ToArray();

            //注册仓储 && Service
            builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IRepository<,>));
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

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
        }
        #endregion
    }
}
