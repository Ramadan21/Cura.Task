using Microsoft.EntityFrameworkCore;
using Cura.Task.Sheard.EntityFramework;
using Cura.Task.DAL;
using Cura.Task.DAL.Repositories.MailAttachmentRepository;
using Cura.Task.DAL.Repositories.UserRepository;
using Cura.Task.DAL.Repositories.MailRepository;
using Cura.Task.Service.UserService;
using Cura.Task.Service.MailService;
using Cura.Task.Sheard.Helpers.EmailSender;
using Microsoft.Extensions.Configuration;
using Cura.Task.Service.Helper;

namespace Cura.Task.API.Extensions
{
    public static class ConfigureServicesExtension
    {
        private static void AddHttpClientHelpers(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClient>();
            services.AddHttpClient();
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseConfig(configuration);
            services.AddHttpClientHelpers();
            services.RegisterRepositories();
            services.RegisterServices();
            return services;
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped < IUnitOfWork, Sheard.EntityFramework.UnitOfWork <CuraContext>>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<ITokenHelper, TokenHelper>();
           
          
        }
       
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMailRepository, MailRepository>();
            services.AddTransient<IMailAttachmentRepository, MailAttachmentRepository>();
        }
        private static void DatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, CuraContext>();
            services.AddDbContext<CuraContext>(opts => opts. UseSqlServer(configuration["ConnectionStrings:CuraDatabase"]));
            services.AddScoped(typeof(CuraContext));
        }
    }
}
