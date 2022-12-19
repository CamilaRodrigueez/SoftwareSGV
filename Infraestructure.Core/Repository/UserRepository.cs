using Common.Utils.Enums;
using Infraestructure.Core.Data;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructure.Core.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        #region Attributes
        private readonly DataContext _context;
        #endregion Attributes

        #region Builder
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public List<UserEntity> InstructoresFicha(int idFicha)
        {
            var result = (from utu in _context.UserTypeUserEntity
                          join u in _context.UserEntity on new
                          {
                              utu.IdUser,
                              utu.IdTypeUser
                          } equals new
                          {
                              u.IdUser,
                              IdTypeUser = (int)Enums.TypeUser.Instructor
                          }
                          join fu in _context.FichaUserEntity on u.IdUser equals fu.IdUser
                          where fu.IdFicha == idFicha
                          select new UserEntity
                          {
                              Email = u.Email,
                              Identification = u.Identification,
                              IdUser = u.IdUser,
                              LastName = u.LastName,
                              Name = u.Name,
                              Telefono = u.Telefono,
                          }).ToList();



            //var resultInasistencias = (from p in _context.PresenceEntity
            //                           join u in _context.UserEntity on p.IdUser equals u.IdUser
            //                           join fu in _context.FichaUserEntity on u.IdUser equals fu.IdUser
            //                           where fu.IdFicha == idFicha
            //                           select new UserEntity
            //                           {
            //                               Email = u.Email,
            //                               Identification = u.Identification,
            //                               IdUser = u.IdUser,
            //                               LastName = u.LastName,
            //                               Name = u.Name,
            //                               Telefono = u.Telefono,
            //                           }).ToList();

            return result;
        }

        #endregion
    }
}
