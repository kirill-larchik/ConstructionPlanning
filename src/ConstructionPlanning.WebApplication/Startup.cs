using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Mappings;
using ConstructionPlanning.BusinessLogic.Services;
using ConstructionPlanning.BusinessLogic.Services.Export;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.BusinessLogic.Services.Pagination;
using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Filters;
using ConstructionPlanning.WebApplication.Mappings;
using ConstructionPlanning.WebApplication.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication
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
            services.AddDbContext<ConstructionPlanningDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConstructionPlanningDb")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConstructionPlanningIdentityDb")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(DtoMappingProfile), typeof(ViewModelMappingProfile));

            services.AddTransient<IRepository<Resource>, ResourceRepository>();
            services.AddTransient<IRepository<ResourceType>, ResourceTypeRepository>();
            services.AddTransient<IRepository<Provider>, ProviderRepository>();
            services.AddTransient<IRepository<Delivery>, DeliveryRepository>();
            services.AddTransient<IRepository<Sale>, SaleRepository>();

            services.AddTransient<IPaginationService<Resource, ResourceDto>, ResourcePaginationService>();
            services.AddTransient<IPaginationService<ResourceType, ResourceTypeDto>, ResourceTypePaginationService>();
            services.AddTransient<IPaginationService<Provider, ProviderDto>, ProviderPaginationService>();
            services.AddTransient<IPaginationService<Delivery, DeliveryDto>, DeliveryPaginationService>();
            services.AddTransient<IPaginationService<Sale, SaleDto>, SalePaginationService>();

            services.AddTransient<IResourceTypeService, ResourceTypeService>();
            services.AddTransient<IResourceService, ResourceService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<ISaleService, SaleService>();

            services.AddTransient<IExcelExportService<DeliveryDto>, DeliveryExportService>();
            services.AddTransient<IExcelExportService<SaleDto>, SaleExportService>();

            services.AddControllersWithViews(config => config.Filters.Add(typeof(CustomExceptionFilter)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            UserService.InitializeRolesAndUserAsync(serviceProvider, Configuration).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
