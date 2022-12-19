using Common.Utils.Enums;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.Core.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        #region Builder
        public SeedDb(DataContext context)
        {
            _context = context;
        }
        #endregion


        public async Task ExecSeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
    
           
            await CheckTypePermissionAsync();
            await CheckPermissionAsync();
            await CheckRolAsync();
            await CheckRolPermissonAsync();
            await CheckTypeUserAsync();
            await CheckUserAsync();
            await CheckRolUserAsync();
            await CheckUserTypeUser();

        }

      
        private async Task CheckTypeUserAsync()
        {
            if (!_context.TypeUserEntity.Any())
            {
                _context.TypeUserEntity.AddRange(new List<TypeUserEntity>
                {
                    new TypeUserEntity
                    {
                        IdTypeUser = (int)Enums.TypeUser.Instructor,
                        TypeUser = "Instructor"
                    },
                    new TypeUserEntity
                    {
                       IdTypeUser = (int)Enums.TypeUser.Aprendiz,
                        TypeUser = "Aprendiz"
                    },
                     new TypeUserEntity
                    {
                        IdTypeUser = (int)Enums.TypeUser.Coordinador,
                        TypeUser = "Coordinador"
                    },
                      new TypeUserEntity
                    {
                        IdTypeUser = (int)Enums.TypeUser.BienestarAprendiz,
                        TypeUser = "Bienestar al Aprendiz"
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTypePermissionAsync()
        {
            if (!_context.TypePermissionEntity.Any())
            {
                _context.TypePermissionEntity.AddRange(new List<TypePermissionEntity>
                {
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Usuarios,
                        TypePermission="Usuarios"
                    },
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        TypePermission="Roles"
                    },
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        TypePermission="Permisos"
                    },
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Excusa,
                        TypePermission="Excusa"
                    },
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Asistencia,
                        TypePermission="Asistencia"
                    },
                     new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Ficha,
                        TypePermission="Ficha"
                    },
                       new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Notificaciones,
                        TypePermission="Notificaciones"
                    },
                        new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        TypePermission="Estados"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPermissionAsync()
        {
            if (!_context.PermissionEntity.Any())
            {
                _context.PermissionEntity.AddRange(new List<PermissionEntity>
                {
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuarios,
                        Permission="Crear Usuarios",
                        Description="Crear usuarios en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuarios,
                        Permission="Actualizar Usuarios",
                        Description="Actualizar datos de un usuario en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuarios,
                        Permission="Eliminar Usuarios",
                        Description="Eliminar un usuairo del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuarios,
                        Permission="Consultar Usuarios",
                        Description="Consulta todos los usuarios"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarRoles,
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        Permission="Actualizar Roles",
                        Description="Actualizar datos de un Roles en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarRoles,
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        Permission="Consultar Roles",
                        Description="Consultar Roles del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Actualizar Permisos",
                        Description="Actualizar datos de un Permiso en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Consultar Permisos",
                        Description="Consultar Permisos del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.DenegarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Denegar Permisos Rol",
                        Description="Denegar permisos a un rol del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarEstados,
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        Permission="Consultar Estado",
                        Description="Consultar los estados del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarEstado,
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        Permission="Actualizar Estados",
                        Description="Actualizar los estados del sistema"
                    },
                     new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearExcusa,
                        IdTypePermission=(int)Enums.TypePermission.Excusa,
                        Permission="Crear Excusa",
                        Description="Crear Excusa en el Sistema"
                    },
                      new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarExcusa,
                        IdTypePermission=(int)Enums.TypePermission.Excusa,
                        Permission="Actualizar Excusas",
                        Description="Actualizar Excusa en el sistema"
                    },
                         new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarExcusa,
                        IdTypePermission=(int)Enums.TypePermission.Excusa,
                        Permission="Eliminar Excusa",
                        Description="Eliminar Excusa del Sistema"
                    },
                        new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarExcusa,
                        IdTypePermission=(int)Enums.TypePermission.Excusa,
                        Permission="Consultar Excusas",
                        Description="Consultar Excusas en el Sistema"
                    },
                         new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearAsistencia,
                        IdTypePermission=(int)Enums.TypePermission.Asistencia,
                        Permission="Crear Asistencia",
                        Description="Crear Asistencia en el Sistema"
                    },
                           new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarAsistencia,
                        IdTypePermission=(int)Enums.TypePermission.Asistencia,
                        Permission="Actualizar Asistencia",
                        Description="Actualizar Asistencia en el sistema"
                    },
                         new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarAsistencia,
                        IdTypePermission=(int)Enums.TypePermission.Asistencia,
                        Permission="Consultar Asistencia",
                        Description="Consultar Asistencia en el Sistema"
                    },
                        new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.DescargarInformeAsistencia,
                        IdTypePermission=(int)Enums.TypePermission.Asistencia,
                        Permission="Descargar informe de Asistencia",
                        Description="Descargar informe de Asistencia del Sistema"
                    },
                        new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearFicha,
                        IdTypePermission=(int)Enums.TypePermission.Ficha,
                        Permission="Crear Ficha",
                        Description="Crear Ficha en el Sistema"
                    },
                        new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarFicha,
                        IdTypePermission=(int)Enums.TypePermission.Ficha,
                        Permission="Actualizar Ficha",
                        Description="Actualizar Ficha en el Sistema"
                    },
                        new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarFicha,
                        IdTypePermission=(int)Enums.TypePermission.Ficha,
                        Permission="Consultar Ficha",
                        Description="Consultar Ficha en el Sistema"
                    },
                             new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarNotificaciones,
                        IdTypePermission=(int)Enums.TypePermission.Notificaciones,
                        Permission="Consultar Notificaciones",
                        Description="Consultar Notificaciones en el Sistema"
                    },


                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolAsync()
        {
            if (!_context.RolEntity.Any())
            {
                _context.RolEntity.AddRange(new List<RolEntity>
                {
                    new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Administrador,
                        Rol="Administrador"
                    },
                     new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Estandar,
                        Rol="Estandar"
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolPermissonAsync()
        {
            if (!_context.RolPermissionEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                var rolesPermisosAdmin = _context.PermissionEntity.Select(x => new RolPermissionEntity
                {
                    IdRol = (int)Enums.RolUser.Administrador,
                    IdPermission = x.IdPermission
                }).ToList();

                _context.RolPermissionEntity.AddRange(rolesPermisosAdmin);


                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUserAsync()
        {
            if (!_context.UserEntity.Any())
            {
                _context.UserEntity.AddRange(new List<UserEntity>()
                {
                    new UserEntity()
                    {
                        Password = "123456",
                        Name = "Pepito",
                        LastName = "Perez",
                        Identification = "1040854658",
                        Email="pepito@gmail.com",
                        Telefono="3004101236",
                    },
                    new UserEntity()
                    {
                        Password = "123456",
                        Name = "John",
                        LastName = "Doe",
                        Identification = "100200300",
                        Email="doe@gmail.com",
                        Telefono="3004101236",
                    },

                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolUserAsync()
        {
            if (!_context.RolUserEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                var users = _context.UserEntity;

                foreach (var item in users)
                {
                    if (item.Name == "Pepito")
                    {
                        _context.RolUserEntity.Add(new RolUserEntity
                        {
                            IdRol = (int)Enums.RolUser.Administrador,
                            IdUser = item.IdUser,
                        });
                    }
                    else
                    {
                        _context.RolUserEntity.Add(new RolUserEntity
                        {
                            IdRol = (int)Enums.RolUser.Estandar,
                            IdUser = item.IdUser,
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUserTypeUser()
        {
            if (!_context.UserTypeUserEntity.Where(x => x.IdTypeUser == (int)Enums.TypeUser.Instructor).Any())
            {
                var users = _context.UserEntity;

                foreach (var item in users)
                {
                    if (item.Name == "Pepito")
                    {
                        _context.UserTypeUserEntity.Add(new UserTypeUserEntity
                        {
                            IdTypeUser = (int)Enums.TypeUser.Instructor,
                            IdUser = item.IdUser,
                            Selected=true
                        });

                        _context.UserTypeUserEntity.Add(new UserTypeUserEntity
                        {
                            IdTypeUser = (int)Enums.TypeUser.Aprendiz,
                            IdUser = item.IdUser,
                            Selected = false
                        });
                    }
                    else
                    {
                        _context.UserTypeUserEntity.Add(new UserTypeUserEntity
                        {
                            IdTypeUser = (int)Enums.TypeUser.Aprendiz,
                            IdUser = item.IdUser,
                            Selected = true
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}