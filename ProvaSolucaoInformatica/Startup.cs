using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProvaSolucaoInformatica.Databases.Default;
using ProvaSolucaoInformatica.Services;

namespace ProvaSolucaoInformatica
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
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

#warning Insira a connection string antes de iniciar
            optionsBuilder.UseMySql("Insira connection string aqui");

            services.AddTransient(s => new GerenciadorUsuarios(new DefaultDbContext(optionsBuilder.Options)));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = "/Conta/Login";
                    options.LogoutPath = "/Conta/Deslogar";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                });

            services.AddControllersWithViews();

#warning Insira a connection string antes de iniciar
            services.AddDbContextPool<DefaultDbContext>(builder =>
                builder.UseMySql("Insira connection string aqui"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}