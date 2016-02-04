using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DaoProject.Dao;
using DaoProject.DbAccess;
using ScjnUtilities;

namespace DaoProject.Model
{
    public class AreasModel
    {
        private readonly CommonProperties area;

        public AreasModel() { }

        public AreasModel(CommonProperties area)
        {
            this.area = area;
        }

        /// <summary>
        /// DElvuelve la lista de las áreas pertenecientes a la coordinación
        /// </summary>
        /// <returns></returns>
        public List<CommonProperties> GetAreas()
        {
            SqlConnection connection = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> areas = new List<CommonProperties>();

            try
            {
                connection.Open();

                string selstr = "SELECT * FROM Areas";
                SqlCommand cmd = new SqlCommand(selstr, connection);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        CommonProperties area = new CommonProperties();
                        area.IdElemento = Convert.ToInt32(dataReader["idArea"]);
                        area.Descripcion = dataReader["Area"].ToString();
                        area.Corto = dataReader["Corto"].ToString();
                        area.Abreviatura = dataReader["Abreviatura"].ToString();

                        areas.Add(area);
                    }
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            finally
            {
                connection.Close();
            }
            return areas;
        }

        /// <summary>
        /// Devuelve la lista de adscripciones de la SCJN
        /// </summary>
        /// <returns></returns>
        public List<CommonProperties> GetAdscripciones()
        {
            SqlConnection connection = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> adscripciones = new List<CommonProperties>();

            try
            {
                connection.Open();

                string selstr = "SELECT * FROM Adscripciones";
                SqlCommand cmd = new SqlCommand(selstr, connection);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        CommonProperties adscripcion = new CommonProperties();
                        adscripcion.IdElemento = Convert.ToInt32(dataReader["idAdscipcion"]);
                        adscripcion.Descripcion = dataReader["Adscripcion"].ToString();

                        adscripciones.Add(adscripcion);
                    }
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            finally
            {
                connection.Close();
            }
            return adscripciones;
        }

        /// <summary>
        /// Agrega un área nueva al directorio de la Coordinación
        /// </summary>
        public void SetNewArea()
        {
            SqlConnection connection = Conexion.GetConexion();

            try
            {
                string queryString = "INSERT INTO Areas(Area,Corto,Abreviatura)" +
                                                        " VALUES(@Area,@Corto,@Abreviatura)";
                connection.Open();
                SqlCommand cmd = new SqlCommand(queryString, connection);

                cmd.Parameters.AddWithValue("@Area", area.Descripcion);
                cmd.Parameters.AddWithValue("@Corto", area.Corto);
                cmd.Parameters.AddWithValue("@Abreviatura", area.Abreviatura);
                cmd.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            finally
            {
                connection.Close();
            }
        }



        /// <summary>
        /// Actualiza la información de alguna de las áreas existente en el catálogo
        /// </summary>
        public void UpdateArea()
        {
            SqlConnection connection = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;
            SqlCommand cmd;
            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            try
            {
                connection.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlCadena = "SELECT * FROM Areas WHERE idArea =" + area.IdElemento;

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Areas");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["Area"] = area.Descripcion;
                dr["Corto"] = area.Corto;
                dr["Abreviatura"] = area.Abreviatura;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                string sSql = "UPDATE Areas SET Area = @Area, Corto = @Corto, Abreviatura = @Abreviatura WHERE IdArea = @IdArea";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@Area", SqlDbType.VarChar, 0, "Area");
                dataAdapter.UpdateCommand.Parameters.Add("@Corto", SqlDbType.VarChar, 0, "Corto");
                dataAdapter.UpdateCommand.Parameters.Add("@Abreviatura", SqlDbType.VarChar, 0, "Abreviatura");
                dataAdapter.UpdateCommand.Parameters.Add("@IdArea", SqlDbType.Int, 0, "IdArea");

                dataAdapter.Update(dataSet, "Areas");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Elimina el área seleccionada
        /// </summary>
        public void DeleteArea()
        {
            SqlConnection connection = Conexion.GetConexion();
            SqlCommand cmd;

            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            String sqlCadena = "DELETE FROM Areas WHERE idArea = " + area.IdElemento;

            try
            {
                connection.Open();
                cmd.CommandText = sqlCadena;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AreasModel", "Inventario");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
