using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.DbAccess;
using ScjnUtilities;

namespace DaoProject.Model
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
            SqlDataReader dataReader = null;

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
                dataReader.Close();
                selstr = null;

            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "DaoProject");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "DaoProject");
            }
            finally
            {
                sqlConne.Close();
            }
            return state;
        }
    }
}