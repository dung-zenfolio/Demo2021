using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using poc_database.Context;
using AutoMapper;
using poc_resource.Repositories;
using poc_resource.Repositories.IRepository;
using poc_service.services.Interfaces;
using poc_common.Configurations;
using poc_manager.Interfaces;
using poc_manager.Infrastructure;
using poc_service.services;
using IdentityServer4.Validation;
using poc_service.Validator;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;

namespace poc_userservice
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
            services.Configure<Configurations.Encrypt>(Configuration.GetSection(
                                        Configurations.Encrypt.Section));
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<UserDBContext>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddSingleton<IRabbitConnection, RabbitConnection>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title="User Service", Version = "1" });
            });
            services.AddControllers();

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication("Bearer", options =>
            //    {
            //        options.ApiName = "userApi";
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = "http://localhost:5000";
            //    });

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IdentityServer4.Services.IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDBContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseAuthorization();

            // db.Database.EnsureCreated();
            db.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
        }



    }
}
