using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSMiVenta.Models.Common;
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

                //añadir el barer token en la documentación de swager (para que podamos usarlo como postman) ------
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Colocar aqui SOLO el token",
                    In = ParameterLocation.Header, //localización de texto
                    Type = SecuritySchemeType.Http, //tipo de seguridad
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                }); 
                //-----------------------------------------------------------------------------------------------------
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
            services.AddScoped<IUsuarioService, UsuarioServicio>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IPedidoService, PedidoService>();

            //Configuración de JWT -------------------------------------------------------------------------------------------------------------------------------------------
            var appSettingsSecction = Configuration.GetSection("AppSettings"); //variable, el GetSection lleva entre parentesis el nombre de la variable declarada en appSettings.json
            services.Configure<AppSettingsCommon>(appSettingsSecction);

            //JWT
            var appSettings = appSettingsSecction.Get<AppSettingsCommon>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto); //encryptamos mi secreto

            services.AddAuthentication(d =>
            { //damos de alta el token
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true; //vida del token osea que se pueda guardar
                    d.TokenValidationParameters = new TokenValidationParameters // parametros de validación del token
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(llave),  //esta es la que dara el token, asi que le asignamos el secreto, ahora asignada en llave
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("admin", policy =>
            //    {
            //        policy.RequireClaim(ClaimTypes.Role, "admin");                    

            //        policy.RequireAssertion((context) =>
            //        {
            //            foreach (var item in context.User.Claims)
            //            {
            //                var isAdmin = item.Type.Equals(ClaimTypes.Role, StringComparison.InvariantCultureIgnoreCase) && item.Value.Equals("admin", StringComparison.InvariantCultureIgnoreCase);

            //                if (isAdmin)
            //                {
            //                    return true;
            //                }                            
            //            }
            //            return false;
            //        });

            //    });
            //});

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

            app.UseCors(MiCors); //para CORS

            app.UseAuthentication(); //le decimos que ocupara autenticación

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
