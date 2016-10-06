using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DaoProject.Dao;
using DaoProject.DbAccess;
using ScjnUtilities;

namespace DaoProject.Model
{
    public class UbicacionModel
    {
        /// <summary>
        /// Devuelve un listado de los edificios en los cuales tiene inventario la coordinación
        /// </summary>
        /// <returns></returns>
        public List<CommonProperties> GetUbicaciones()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> ubicaciones = new List<CommonProperties>();

            try
            {
                sqlConne.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Ubicaciones", sqlConne);
                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        CommonProperties ubicacion = new CommonProperties()
                        {
                            IdElemento = Convert.ToInt32(dataReader["idUbicacion"]),
                            Descripcion = dataReader["Ubicacion"].ToString()
                        };

                        ubicaciones.Add(ubicacion);
                    }
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UbicacionModel", "DaoProject");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UbicacionModel", "DaoProject");
            }
            finally
            {
                sqlConne.Close();
            }
            return ubicaciones;
        }
    }
}
