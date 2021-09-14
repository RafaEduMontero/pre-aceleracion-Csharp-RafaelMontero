using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Interfaces;
using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using ChallengeAlkemyDisney.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChallengeAlkemyDisney", Version = "v1" });
            });
            
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = "https://localhost:5001",
                        ValidIssuer = "https://localhost:5001",
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                });

            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<DisneyContext>(optionsAction: (provider,builder) =>
                {
                    builder.UseInternalServiceProvider(provider);
                    builder.UseSqlServer(Configuration.GetConnectionString(name: "Connection"));
                }
            );
            services.AddDbContext<UserContext>(optionsAction: (provider, builder) =>
            {
                builder.UseInternalServiceProvider(provider);
                builder.UseSqlServer(Configuration.GetConnectionString(name: "UserConnection"));
            });
            services.AddScoped<ICelebrityRepository, CelebrityRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IMovieOrSerieRepository, MovieOrSerieRepository>();

            services.AddSendGrid(SendGrid =>
            {
                SendGrid.ApiKey = "Tu api key send grid";
            });

            services.AddScoped<IMailService, MailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChallengeAlkemyDisney v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
