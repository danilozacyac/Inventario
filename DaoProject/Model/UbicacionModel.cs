using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;

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

                string selstr = "SELECT * FROM Ubicaciones";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        CommonProperties ubicacion = new CommonProperties();
                        ubicacion.IdElemento = Convert.ToInt32(dataReader["idUbicacion"]);
                        ubicacion.Descripcion = dataReader["Ubicacion"].ToString();

                        ubicaciones.Add(ubicacion);
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
            return ubicaciones;
        }
    }
}
