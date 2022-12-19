using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utils.Enums
{
    public class Enums
    {
        public enum TypeState
        {
            //Usuario
            EstadoUsuario = 1,
            EstadoFicha = 2,
            EstadoAsistencia = 3,
        }
        public enum TypeUser
        {
            //Usuario
            Instructor = 1,
            Aprendiz = 2,
            Coordinador = 3,
            BienestarAprendiz = 4,
        }

        public enum State
        {
            //Usuario
            UsuarioActivo = 1,
            UsuarioInactivo = 2,
            UsuarioSuspendido = 3,

            //Ficha
            FichaActiva = 4,
            FichaCancelada = 5,
            FichaFinalizada = 6,

            //Asistencia
            AsistenciaActiva = 7,
            AsistenciaFinalizada = 8,
            AsistenciaCancelada = 9,


        }

        public enum TypePermission
        {
            Usuarios = 1,
            Roles = 2,
            Permisos = 3,
            Excusa = 4,
            Asistencia = 5,
            Ficha = 6,
            Notificaciones = 7,
            Estados = 8,
        }

        public enum Permission
        {
            //Usuarios
            CrearUsuarios = 1,
            ActualizarUsuarios = 2,
            EliminarUsuarios = 3,
            ConsultarUsuarios = 4,

            //Roles
            ActualizarRoles = 5,
            ConsultarRoles = 6,

            //Permisos
            ActualizarPermisos = 7,
            ConsultarPermisos = 8,
            DenegarPermisos = 9,

            //Excusa
            CrearExcusa = 10,
            ActualizarExcusa = 11,
            EliminarExcusa = 12,
            ConsultarExcusa = 13,

            //Asistencia
            CrearAsistencia = 14,
            ActualizarAsistencia = 15,
            DescargarInformeAsistencia = 16,
            ConsultarAsistencia = 17,

            //Ficha
            CrearFicha = 18,
            ActualizarFicha = 19,   
            ConsultarFicha = 20,

            //Notificaciones
            ConsultarNotificaciones = 21,

            //Estados
            ConsultarEstados = 22,
            ActualizarEstado = 23,
        }
       
        public enum RolUser
        {
            Administrador = 1,
            Estandar= 2
        }

    }
}
