using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PantAPI.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PantAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PantAPI", Version = "v1" });
            });
            services.AddMemoryCache();
            services.AddCors();
            services.AddTransient(s => new BagRepository(Configuration.GetValue<string>("StorageConnectionString")));
            services.AddTransient(s => new TokenRepository(Configuration.GetValue<string>("StorageConnectionString")));
            services.AddTransient(s => new UserRepository(Configuration.GetValue<string>("StorageConnectionString"), s.GetRequiredService<TokenRepository>()));
            services.AddScoped<AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PantAPI V1");
            });

            app.UseCors(builder => 
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("x-plukk-token"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
