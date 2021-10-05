using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Services;

namespace WSMiVenta
{
    public class Startup
    {
        public readonly string MiCors = "MiCors"; //para dar permiso de lectura en CORS (se usa en ConfigureServices)

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WSMiVenta", Version = "v1" });
            });

            //Agregar CORS
            services.AddCors(options => {
                options.AddPolicy(name: MiCors, builder =>
                {
                    builder.WithOrigins("*"); //todos los sitios se permiten OJO aqui, porque define la seguirdad, en este caso se permite la conexión de x lugar
                    builder.WithHeaders("*"); //permite el http post osea insercciones de datos de cualquier sitio
                    builder.WithMethods("*"); //permite el http put y delete de cualquier sitio
                });
            });

            //Inyecciónes por scoped es para cada request de mi servicio
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMiVenta v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
