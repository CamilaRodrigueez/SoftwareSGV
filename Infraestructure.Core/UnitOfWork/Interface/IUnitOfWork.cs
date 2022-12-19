using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Infraestructure.Core.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {

        IUserRepository UserRepository { get; }

        IRepository<RolEntity> RolRepository { get; }
        IRepository<UserEntity> UsersRepository { get; }
        IRepository<RolUserEntity> RolUserRepository { get; }

        IRepository<PermissionEntity> PermissionRepository { get; }

        IRepository<TypePermissionEntity> TypePermissionRepository { get; }

        IRepository<RolPermissionEntity> RolesPermissionRepository { get; }
        IRepository<TypeUserEntity> TypeUserRepository { get; }
        IRepository<UserTypeUserEntity> UserTypeUserRepository { get; }
        IRepository<ClassEntity> ClassRepository { get; }
        IRepository<ExcuseEntity> ExcuseRepository { get; }
        IRepository<FichaClassEntity> FichaClassRepository { get; }
        IRepository<FichaEntity> FichaRepository { get; }
        IRepository<FichaUserEntity> FichaUserRepository { get; }
        IRepository<PresenceEntity> PresenceRepository { get; }
        IRepository<NotificationEntity> NotificationRepository { get; }


        void Dispose();

        Task<int> Save();

        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
