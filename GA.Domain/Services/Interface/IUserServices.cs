using GA.Domain.DTO;
using GA.Domain.DTO.User;
using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface IUserServices
    {
        #region Autentication
        ResponseDto Login(UserDto user);
        #endregion

        #region Methods 
        List<UserDto> GetAllUsers(bool isInstructor);
        Task<ResponseDto> InserUserAsync(UserDto userInsert, bool isInstructor);
        Task<ResponseDto> UpdateUserAsync(UserDto updateUser);
        Task<ResponseDto> DeleteUserAsync(int idUser);
        List<UserDto> GetAllStudentByFicha(int idFicha);
        UserEntity GetUser(int idUser);
        Task<ResponseDto> UpdatePersonalInformationAsync(UserDto updateUser);

        Task<ResponseDto> InserUserMasivoAsync(List<UserExcelDto> listStudents, int idFicha);
        #endregion
    }
}
