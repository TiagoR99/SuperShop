using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop
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
            // Configura o DbContext com a string de conexão
            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Adiciona suporte a controladores e views (MVC)
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Página de erro detalhado no desenvolvimento
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Página de erro genérica em produção
                app.UseExceptionHandler("/Home/Error");
                // Força o uso de HTTPS em produção
                app.UseHsts();
            }

            // Redireciona HTTP para HTTPS
            app.UseHttpsRedirection();

            // Permite servir ficheiros estáticos (ex: CSS, JS, imagens)
            app.UseStaticFiles();

            // Ativa o sistema de routing
            app.UseRouting();

            // Ativa a autorização (caso venhas a usar)
            app.UseAuthorization();

            // Define o endpoint padrão da aplicação
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
