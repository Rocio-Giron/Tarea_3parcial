
using Dapper;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private string CadenaConexion;

        public LoginRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        public  async Task<bool> ValidarUsuario(Login login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE codigo = @codigo AND clave=@clave;";
                valido = await conexion.ExecuteScalarAsync<bool>(sql, new {login.codigo, login.clave});
                
            }
            catch (Exception ex)
            {

                //throw;
            }
            return valido;
        }
    }
}
