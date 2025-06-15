using Duende.IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                if(builder.Environment.IsEnvironment("Docker"))
                {
                    Log.Debug("Running in Docker environment, setting IssuerUri to localhost:5001");
                     options.IssuerUri = "http://localhost:5001";
                }
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddLicenseSummary()
            .AddProfileService<CustomProfileService>();

        // ??
        builder.Services.ConfigureApplicationCookie(options => {
            options.Cookie.SameSite = SameSiteMode.Lax;
        });
        
        builder.Services.AddAuthentication();
           
        
        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy("CorsPolicy", builder => 
        //         builder.WithOrigins($"http://localhost")
        //                .AllowAnyOrigin()
        //                .AllowAnyMethod()
        //                .AllowAnyHeader());
        // });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages()
            .RequireAuthorization();
        app.UseCors("CorsPolicy");

        return app;
    }
}
