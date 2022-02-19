using Application.Interfaces.Context;
using Application.Permissions;
using Application.ProductCategories;
using Application.UserPanel;
using Application.Users;
using Common;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Infrastructure.MappingProfile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Contexts;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace WebSite
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
            services.AddRazorPages();
            services.AddControllersWithViews();


            #region config services

            services.AddTransient(typeof(IDataBaseContext), typeof(DataBaseContext));
            services.AddTransient<IUserService, UserServcie>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IUserPanelService, UserPanelService>();
            #endregion


            #region config database

            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StockShopConnection"));
            });

            #endregion

            #region authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            #endregion

            #region html encoder

            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

            #endregion

            #region mapper
            services.AddAutoMapper(typeof(UserMappinProfile));
            #endregion

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();



                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                            name: "areas",
                            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
