using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Test_coffe.Controllers.Hubs;
using Test_coffe.Controllers.mobile.Repository;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Repository;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe
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
            services.AddTransient<ICities, CitiesRepository>();
            services.AddTransient<ICataloges, CatalogesRepository>();
            services.AddTransient<ITables, TablesRepository>();
            services.AddTransient<ILogin, LoginRepository>();
            services.AddTransient<IBills, BillsRepository>();
            services.AddTransient<IBillDetails, BillDetailsRepository>();
            services.AddTransient<IShop, ShopRepository>();
            services.AddTransient<IProduct, ProductRepository>();
            services.AddTransient<IMenu, MenuRepository>();

            services.AddTransient<IShops, ShopsRepository>();
            services.AddTransient<IUploadImage, UploadRepository>();
            services.AddTransient<IProducts, ProductsRepository>();
            services.AddTransient<IFloors, FloorsRepository>();
            services.AddTransient<ITablesMobile, TablesMobileRepository>();

            SQLUtils._connStr = Configuration.GetConnectionString("DefaultConnection");

            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:5001",
                                                          "http://127.0.0.1:5500")
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod();
                                  });
            });

            //services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = true;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256")),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        RequireExpirationTime = false,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddScoped<ITokenBuilder, TokenBuilder>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        // Clock skew compensates for server time drift.
            //        // We recommend 5 minutes or less:
            //        ClockSkew = TimeSpan.FromMinutes(5),
            //        // Specify the key used to sign the token:
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256")),
            //        RequireSignedTokens = true,
            //        // Ensure the token hasn't expired:
            //        RequireExpirationTime = true,
            //        ValidateLifetime = true,
            //        // Ensure the token audience matches our audience value (default true):
            //        ValidateAudience = true,
            //        ValidAudience = "api://default",
            //        // Ensure the token was issued by a trusted authorization server (default true):
            //        ValidateIssuer = true,
            //        ValidIssuer = "https://{yourOktaDomain}/oauth2/default"
            //    };
            //});




            services.AddSession();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddSignalR();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("Policy1");
            app.UseAuthorization();
            app.UseSession();
            //app.UseMiddleware<JwtTokenMiddleware>();
            app.UseMvc();

            app.UseSignalR(config =>
            {
                config.MapHub<SignalServer>("/signalServer");
            });

            app.UseSignalR(config =>
            {
                config.MapHub<Cancel>("/Cancel");
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //   name: "default",
                //   pattern: "{controller=Mobile}/{action=LoginNew}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
