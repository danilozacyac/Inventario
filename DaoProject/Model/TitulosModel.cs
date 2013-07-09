using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;

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

                string selstr = "SELECT * FROM Titulos";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        CommonProperties adscripcion = new CommonProperties();
                        adscripcion.IdElemento = Convert.ToInt32(dataReader["idTitulo"]);
                        adscripcion.Descripcion = dataReader["Titulo"].ToString();
                        adscripcion.Abreviatura = dataReader["abrev"].ToString();

                        adscripciones.Add(adscripcion);
                    }
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
            return adscripciones;
        }
    }
}
