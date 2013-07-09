using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;

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
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> areas = new List<CommonProperties>();

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM Areas";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

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
            return areas;
        }

        /// <summary>
        /// Devuelve la lista de adscripciones de la SCJN
        /// </summary>
        /// <returns></returns>
        public List<CommonProperties> GetAdscripciones()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> adscripciones = new List<CommonProperties>();

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM Adscripciones";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

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

        /// <summary>
        /// Agrega un área nueva al directorio de la Coordinación
        /// </summary>
        public void SetNewArea()
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Areas WHERE idArea = 0";

            dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

            dataAdapter.Fill(dataSet, "Areas");

            dr = dataSet.Tables["Areas"].NewRow();
            dr["Area"] = area.Descripcion;
            dr["Corto"] = area.Corto;
            dr["Abreviatura"] = area.Abreviatura;

            dataSet.Tables["Areas"].Rows.Add(dr);

            dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
            dataAdapter.InsertCommand.CommandText = "INSERT INTO Areas(Area,Corto,Abreviatura)" +
                                                    " VALUES(@Area,@Corto,@Abreviatura)";

            dataAdapter.InsertCommand.Parameters.Add("@Area", SqlDbType.VarChar, 0, "Area");
            dataAdapter.InsertCommand.Parameters.Add("@Corto", SqlDbType.VarChar, 0, "Corto");
            dataAdapter.InsertCommand.Parameters.Add("@Abreviatura", SqlDbType.VarChar, 0, "Abreviatura");

            dataAdapter.Update(dataSet, "Areas");

            dataSet.Dispose();
            dataAdapter.Dispose();
        }

        /// <summary>
        /// Actualiza la información de alguna de las áreas existente en el catálogo
        /// </summary>
        public void UpdateArea()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;
            SqlCommand cmd;
            cmd = sqlConne.CreateCommand();
            cmd.Connection = sqlConne;

            try
            {
                sqlConne.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlCadena = "SELECT * FROM Areas WHERE idArea =" + area.IdElemento;

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, sqlConne);

                dataAdapter.Fill(dataSet, "Areas");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["Area"] = area.Descripcion;
                dr["Corto"] = area.Corto;
                dr["Abreviatura"] = area.Abreviatura;

                dr.EndEdit();

                dataAdapter.UpdateCommand = sqlConne.CreateCommand();

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
        }

        /// <summary>
        /// Elimina el área seleccionada
        /// </summary>
        public void DeleteArea()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlCommand cmd;

            cmd = sqlConne.CreateCommand();
            cmd.Connection = sqlConne;

            String sqlCadena = "DELETE FROM Areas WHERE idArea = " + area.IdElemento;

            try
            {
                sqlConne.Open();
                cmd.CommandText = sqlCadena;
                cmd.ExecuteNonQuery();
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
        }
    }
}
