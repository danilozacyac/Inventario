using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;
using ScjnUtilities;

namespace DaoProject.Model
{
    public class TitulosModel
    {
        /// <summary>
        /// Devuelve una lista con los títulos que se pueden otorgar a una persona
        /// </summary>
        /// <returns></returns>
        public List<CommonProperties> GetTitulos()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> adscripciones = new List<CommonProperties>();

            try
            {
                sqlConne.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Titulos", sqlConne);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        CommonProperties adscripcion = new CommonProperties()
                        {
                            IdElemento = Convert.ToInt32(dataReader["idTitulo"]),
                            Descripcion = dataReader["Titulo"].ToString(),
                            Abreviatura = dataReader["abrev"].ToString()
                        };

                        adscripciones.Add(adscripcion);
                    }
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TitulosModel", "DaoProject");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TitulosModel", "DaoProject");
            }
            finally
            {
                sqlConne.Close();
            }
            return adscripciones;
        }
    }
}
