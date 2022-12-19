using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Core.Repository.Interface
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        List<UserEntity> InstructoresFicha(int idFicha);

    }
}
