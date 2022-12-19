using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Infraestructure.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        #region Attributes

        private readonly DataContext _context;
        private bool disposed = false;

        #endregion Attributes

        #region builder

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }


        #endregion builder

        #region Properties
        private IUserRepository userRepository;
        private IRepository<UserEntity> usersRepository;
        private IRepository<RolEntity> rolRepository;
        private IRepository<RolUserEntity> rolUserRepository;
        private IRepository<PermissionEntity> permissionRepository;
        private IRepository<TypePermissionEntity> typePermissionRepository;
        private IRepository<RolPermissionEntity> rolPermissionRepository;

        private IRepository<TypeUserEntity> typeUserRepository;
        private IRepository<UserTypeUserEntity> userTypeUserRepository;

        private IRepository<ClassEntity> classRepository;
        private IRepository<ExcuseEntity> excuseRepository;
        private IRepository<FichaClassEntity> fichaClassRepository;
        private IRepository<FichaEntity> fichaRepository;
        private IRepository<FichaUserEntity> fichaUserRepository;
        private IRepository<PresenceEntity> presenceRepository;
        private IRepository<NotificationEntity> notificationRepository;
        #endregion


        #region Members
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new UserRepository(_context);

                return userRepository;
            }
        }

        public IRepository<RolEntity> RolRepository
        {
            get
            {
                if (this.rolRepository == null)
                    this.rolRepository = new Repository<RolEntity>(_context);

                return rolRepository;
            }
        }
        public IRepository<UserEntity> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                    this.usersRepository = new Repository<UserEntity>(_context);

                return usersRepository;
            }
        }
        public IRepository<RolUserEntity> RolUserRepository
        {
            get
            {
                if (this.rolUserRepository == null)
                    this.rolUserRepository = new Repository<RolUserEntity>(_context);

                return rolUserRepository;
            }
        }
      

        public IRepository<PermissionEntity> PermissionRepository
        {
            get
            {
                if (this.permissionRepository == null)
                    this.permissionRepository = new Repository<PermissionEntity>(_context);

                return permissionRepository;
            }
        }

        public IRepository<TypePermissionEntity> TypePermissionRepository
        {
            get
            {
                if (this.typePermissionRepository == null)
                    this.typePermissionRepository = new Repository<TypePermissionEntity>(_context);

                return typePermissionRepository;
            }
        }

        public IRepository<RolPermissionEntity> RolesPermissionRepository
        {
            get
            {
                if (this.rolPermissionRepository == null)
                    this.rolPermissionRepository = new Repository<RolPermissionEntity>(_context);

                return rolPermissionRepository;
            }
        }

        public IRepository<TypeUserEntity> TypeUserRepository
        {
            get
            {
                if (this.typeUserRepository == null)
                    this.typeUserRepository = new Repository<TypeUserEntity>(_context);

                return typeUserRepository;
            }
        }
        public IRepository<UserTypeUserEntity> UserTypeUserRepository
        {
            get
            {
                if (this.userTypeUserRepository == null)
                    this.userTypeUserRepository = new Repository<UserTypeUserEntity>(_context);

                return userTypeUserRepository;
            }
        }
        public IRepository<ClassEntity> ClassRepository
        {
            get
            {
                if (this.classRepository == null)
                    this.classRepository = new Repository<ClassEntity>(_context);

                return classRepository;
            }
        }

        public IRepository<ExcuseEntity> ExcuseRepository
        {
            get
            {
                if (this.excuseRepository == null)
                    this.excuseRepository = new Repository<ExcuseEntity>(_context);

                return excuseRepository;
            }
        }

        public IRepository<FichaClassEntity> FichaClassRepository
        {
            get
            {
                if (this.fichaClassRepository == null)
                    this.fichaClassRepository = new Repository<FichaClassEntity>(_context);

                return fichaClassRepository;
            }
        }
        public IRepository<FichaEntity> FichaRepository
        {
            get
            {
                if (this.fichaRepository == null)
                    this.fichaRepository = new Repository<FichaEntity>(_context);

                return fichaRepository;
            }
        }

        public IRepository<FichaUserEntity> FichaUserRepository
        {
            get
            {
                if (this.fichaUserRepository == null)
                    this.fichaUserRepository = new Repository<FichaUserEntity>(_context);

                return fichaUserRepository;
            }
        }

        public IRepository<PresenceEntity> PresenceRepository
        {
            get
            {
                if (this.presenceRepository == null)
                    this.presenceRepository = new Repository<PresenceEntity>(_context);

                return presenceRepository;
            }
        }

        
        public IRepository<NotificationEntity> NotificationRepository
        {
            get
            {
                if (this.notificationRepository == null)
                    this.notificationRepository = new Repository<NotificationEntity>(_context);

                return notificationRepository;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        #endregion


        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
    }
}
