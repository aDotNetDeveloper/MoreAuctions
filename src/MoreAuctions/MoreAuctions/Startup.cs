using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoreAuctions.Models;

namespace MoreAuctions
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
            services.AddMvc();

            services.Configure<BusinessRules>(Configuration.GetSection("BusinessRules"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            
            services.AddDbContext<MoreAuctionsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MoreAuctionsContext")));

            services.AddSingleton<IEmailSender, EmailSender>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                // ensure migrations are complete
                using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<MoreAuctionsContext>();
                    context.Database.Migrate();
                }
            }
            else
            {
                app.UseExceptionHandler("/Auctions/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Auctions}/{action=Index}/{id?}");
            });
        }
    }
}
