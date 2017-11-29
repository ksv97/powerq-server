using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoreApp.Models;
using CoreApp.Repositories.FacultyRepository;
using CoreApp.Repositories.RolesRepository;
using CoreApp.Repositories;
using CoreApp.Repositories.EventsRepository;

namespace CoreApp
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
			// получаем строку подключения из файла конфигурации
			string connection = Configuration.GetConnectionString("DefaultConnection");

			// добавляем контекст Context в качестве сервиса в приложение
			services.AddDbContext<Context>(options =>
				options.UseSqlServer(connection));
			//services.AddIdentity<IdentityUser, IdentityRole>(options =>
			//{
			//	options.Password.RequiredLength = 5;
			//	options.Password.RequireDigit = false;
			//	options.Password.RequiredUniqueChars = 4;
			//	options.Password.RequireUppercase = false;
			//	options.Password.RequireLowercase = false;
			//	options.Password.RequireNonAlphanumeric = false;
			//}
			//).AddEntityFrameworkStores<Context>()
			//.AddDefaultTokenProviders();

			services.AddCors();
			services.AddMvc();

			services.AddScoped<IFacultyRepository, FacultyRepository>();
			services.AddScoped<IRolesRepository, RolesRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEventRepository, EventRepository>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

			app.UseAuthentication();
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

			DbSeed.Seed(context);
        }
    }
}
