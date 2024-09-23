//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using DatabaseModels;
//using MediatR;
//using Microsoft.OpenApi.Models;

//namespace Sobes
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            // Configure Entity Framework Core
//            services.AddDbContext<ApplicationContext>(options =>
//                options.UseNpgsql(Configuration.GetConnectionString("NotesDb")) // Или UseNpgsql для PostgreSQL
//            );

//            // Configure MediatR
//            services.AddMvc();

//            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));

//            // Register services
//            services.AddScoped<INoteRepository, NoteRepository>();
//            services.AddScoped<NoteService>();

//            // Add controllers
//            services.AddControllers();

//            // Configure Swagger
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotesDb", Version = "v1" });
//            });
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            // Enable middleware to serve generated Swagger as a JSON endpoint.
//            app.UseSwagger();
//            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
//            // specifying the Swagger JSON endpoint.
//            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotesDb v1"));

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.UseHttpsRedirection();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}