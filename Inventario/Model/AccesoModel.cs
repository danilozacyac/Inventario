using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Inventario.DbAccess;

namespace Inventario.Model
{
    public class AccesoModel
    {
        /// <summary>
        /// Verifica las credenciales que ingreso el usuario y permite, o no, el acceso
        /// </summary>
        /// <returns></returns>
        public static Boolean IsLogginCorrect()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            bool state = false;

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM UserAccess WHERE Usuario = @Usuario AND Pass = @Pass";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter usuario = cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 0);
                usuario.Value = AccesoUsuarioModel.Usuario;
                SqlParameter pass = cmd.Parameters.Add("@Pass", SqlDbType.NVarChar, 0);
                pass.Value = AccesoUsuarioModel.Pwd;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    state = true;
                    dataReader.Read();
                    AccesoUsuarioModel.Grupo = Convert.ToInt32(dataReader["idInventario"]);
                    if (AccesoUsuarioModel.Grupo == 3)
                        AccesoUsuarioModel.IsSuper = true;
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("Error ({0}) : {1}" + sql.Source + sql.Message, "Error Interno");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, "Error Interno");
            }
            finally
            {
                sqlConne.Close();
            }
            return state;
        }
    }
}