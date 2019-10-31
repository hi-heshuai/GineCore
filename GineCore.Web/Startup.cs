using GineCore.Common.Appsetting;
using GineCore.Common.Extend;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using GineCore.Service;
using GineCore.Web.Areas.Base.Filter;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GineCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<AppsettingModel>(Configuration.GetSection("AppSetting"))
                .AddMvc();

            //跨域设置
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            //过滤器
            services.AddMvc(config =>
            {
                config.Filters.Add<ExceptionFilter>();
                config.Filters.Add<LoginAuthorizationFilter>();
                config.Filters.Add<ModelFillFilter>();
            });

            RegisterIoc(services);//注册ioc
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "Api",
                    areaName: "Api",
                    template: "{area:exists}/{controller=Home}/{action=Start}/{id?}");
            });

            app.UseCors("any");

            // 使用Cook的中间件
            app.UseAuthentication();

        }

        /// <summary>
        /// 注册ioc
        /// </summary>
        /// <param name="services"></param>
        private void RegisterIoc(IServiceCollection services)
        {
            services.RegisterInterfaceTypes(typeof(IServiceIoc));
            services.RegisterInterfaceTypes(typeof(IRepositoryIoc));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

    }
}
