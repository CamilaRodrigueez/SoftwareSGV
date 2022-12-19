using GA.Domain.Services;
using GA.Domain.Services.Interface;
using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace GestionAsistencia.Handlers
{
    public class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Repository await UnitofWork parameter ctor explicit
            services.AddScoped<UnitOfWork, UnitOfWork>();


            // Infrastructure
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            //Domain
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IFichaServices, FichaServices>();
            services.AddTransient<IClassServices, ClassServices>();
            services.AddTransient<IExcuseServices, ExcuseServices>();
            services.AddTransient<IPresenceServices, PresenceServices>();
            services.AddTransient<INotificationServices, NotificationServices>();

            #region Inyección de DaataSemilla
            services.AddTransient<SeedDb>();
            #endregion

        }
    }
}