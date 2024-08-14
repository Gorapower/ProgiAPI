using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProgiAPI.DbModels;
using System.Reflection;
using ProgiAPI.DbLogic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProgiAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Dbcontext service setup
        Action<DbContextOptionsBuilder, string> dbContextOptionAction = (options, connectionString) => options.UseNpgsql(connectionString);
        services.AddDbContext<progiContext>(options => dbContextOptionAction(options, Configuration.GetConnectionString("DefaultConnection")))
        .AddScoped<IProgiContext>(s => s.GetRequiredService<progiContext>())
            .AddScoped<IConnections, Connections>(c => new Connections(c.GetService<IProgiContext>()));


        // added Swagger UI with Authorization support
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("V1", new OpenApiInfo { Title = "ProgiAPI", Version = "V1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();

        //Setup swagger UI
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = typeof(Startup)
                .Assembly
                .GetCustomAttribute<AssemblyProductAttribute>()
                .Product;
            c.SwaggerEndpoint("swagger/V1/swagger.json", "ProgiAPI");
            c.RoutePrefix = String.Empty;
            c.DisplayOperationId();
            c.DisplayRequestDuration();

        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}