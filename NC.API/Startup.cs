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
using NC.Common.Helper;
using NC.Core.Database;
using NC.Core.IoC;
using NC.Core.Repositories;
using NC.Identity;
using NC.Identity.Models;
using NC.Identity.Store;
using NC.Model.EntityModels;
using NC.Common.Middleware;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
        // public IServiceProvider ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            // add cors
            AddCors(services);

            // add for identity
            AddIdentity(services);

            // add ef core
            AddEfDbContext(services);

            //// add UnitOfWork
            //AddUnitOfWork(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // add Autofac 第三方容器接管Core的默认DI，需修改当前方法返回值为 IServiceProvider
            // return AddAutoFac(services);

            // default DI 
            AddDI(services);

            // AutoFac DI
            //var provider = AddAutoFac(services);
            //return provider;

            // 使用 [ApiExplorerSettings(GroupName = "xxx")] 为 api 进行分组,对应UI中的 definition 下拉框
            // register the Swagger services
            // services.AddOpenApiDocument(config =>
            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "SYSTEM";
                config.ApiGroupNames = new string[]{ "SYSTEM"};
                config.PostProcess = doc =>
                {
                    doc.Info.Version = "v0.1";
                    doc.Info.Title = "NC.Web API";
                    doc.Info.Description = "SYSTEM part apis of ASP.NET Core WebApi";

                    #region other infos
                    //doc.Info.TermsOfService = "None";
                    //doc.Info.Contact = new NSwag.OpenApiContact
                    //{
                    //    Name = "Leo",
                    //    Email = "leo.guo08@gmail.com",
                    //    Url = "http:localhost:5000"
                    //};
                    //doc.Info.License = new NSwag.OpenApiLicense
                    //{
                    //    Name = "Use under LICX",
                    //    Url = "http:localhost:5000/license"
                    //};
                    #endregion 
                };
            });
            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "BUSINESS";
                config.ApiGroupNames = new string[]{ "BUSINESS" };
                config.PostProcess = doc =>
                {
                    doc.Info.Version = "v0.1";
                    doc.Info.Title = "NC.Web API";
                    doc.Info.Description = "BUSINESS part apis of ASP.NET Core WebApi";
                };
            });
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
        private void AddIdentity(IServiceCollection services)
        {
            string defaultConnection = Configuration.GetConnectionString("DefaultConnection");

            // add EF and IdentityFramework
            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(defaultConnection));

            //// add custom normalizer (UpperInvariantLookupNormalizer is the default)
            // services.AddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();

            // add identity types
            services.AddIdentity<SysUser, SysRole>()
                    .AddEntityFrameworkStores<ApplicationUserDbContext>();

            // add services
            services.AddTransient<IUserStore<SysUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<SysRole>, ApplicationRoleStore>();

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
            });
        }
        #endregion

        #region add Microsoft.EntityFrameworkCore.UnitOfWork
        private void AddUnitOfWork(IServiceCollection services)
        {
            services.AddUnitOfWork<CTX>();
        }
        #endregion

        #region default DI
        /// <summary>
        /// net core 默认依赖注入
        /// </summary>
        /// <param name="services"></param>
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

        #region This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content"
            });

            // use custom exception middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            //app.UseOpenApi(config =>
            //{
            //    config.DocumentName = "swagger";
            //    config.Path = "/swagger/v1/swagger.json";
            //});

            app.UseSwaggerUi3(config =>
            {
                config.CustomStylesheetPath = "/Content/swagger/style.css";
            });

            // TODO： reDoc 不支持多 api json文件
            //app.UseReDoc(config =>
            //{
            //    config.Path = "/redoc";
            //    config.DocumentPath = "/swagger/sys/swagger.json";
            //});

            //app.UseReDoc(config =>
            //{
            //    config.Path = "/redoc";
            //    config.DocumentPath = "/swagger/biz/swagger.json";
            //});

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
