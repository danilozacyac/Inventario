using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;
using DaoProject.Utilities;

namespace DaoProject.Model
{
    public class TiposEquiposModel
    {
        private readonly CommonProperties tipo;

        public TiposEquiposModel()
        {
        }

        public TiposEquiposModel(CommonProperties tipo)
        {
            this.tipo = tipo;
        }

        /// <summary>
        /// Devuelve una lista con el tipo de equipos registrados de acuerdo al inventario de que se trate
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CommonProperties> GetTiposEquipos()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            ObservableCollection<CommonProperties> tipos = new ObservableCollection<CommonProperties>();

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM TiposEquipos";// WHERE idInventario = @idInventario ORDER BY Descripcion";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                //SqlParameter tipoInventario = cmd.Parameters.Add("@idInventario", SqlDbType.Int, 0);
                //tipoInventario.Value = AccesoUsuarioModel.Grupo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        CommonProperties descEquipo = new CommonProperties();
                        descEquipo.IdElemento = Convert.ToInt32(dataReader["idTipo"]);
                        descEquipo.Descripcion = dataReader["Descripcion"].ToString();
                        descEquipo.Corto = dataReader["IdInventario"].ToString();

                        tipos.Add(descEquipo);
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
            return tipos;
        }

        /// <summary>
        /// Agrega un elemento al catálogo de tipo de equipos
        /// </summary>
        public CommonProperties SetNewTipoEquipo()
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM TiposEquipos WHERE idTipo = 0";

            try
            {
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

                dataAdapter.Fill(dataSet, "TiposEquipos");

                tipo.IdElemento = this.GetLastId() + 1;
                
                dr = dataSet.Tables["TiposEquipos"].NewRow();
                dr["idTipo"] = tipo.IdElemento;
                dr["Descripcion"] = tipo.Descripcion;
                dr["IdInventario"] = AccesoUsuarioModel.Grupo;

                dataSet.Tables["TiposEquipos"].Rows.Add(dr);

                dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
                dataAdapter.InsertCommand.CommandText = "INSERT INTO TiposEquipos(IdTipo,Descripcion,IdInventario)" +
                                                        " VALUES(@IdTipo,@Descripcion,@IdInventario)";

                dataAdapter.InsertCommand.Parameters.Add("@IdTipo", SqlDbType.Int, 0, "IdTipo");
                dataAdapter.InsertCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar, 0, "Descripcion");
                dataAdapter.InsertCommand.Parameters.Add("@IdInventario", SqlDbType.Int, 0, "IdInventario");

                dataAdapter.Update(dataSet, "TiposEquipos");

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
                connectionEpsSql.Close();
            }

            return tipo;
        }

        /// <summary>
        /// Actualiza la descripcion de los tipos de equipo
        /// </summary>
        /// <returns></returns>
        public CommonProperties UpdateTipoEquipo()
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM TiposEquipos WHERE idTipo = " + tipo.IdElemento + " AND idInventario = " + AccesoUsuarioModel.Grupo;

            try
            {
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

                dataAdapter.Fill(dataSet, "TiposEquipos");

                dr = dataSet.Tables["TiposEquipos"].Rows[0];
                dr.BeginEdit();
                dr["Descripcion"] = tipo.Descripcion.ToUpper();
                dr.EndEdit();

                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE TiposEquipos SET Descripcion = @Descripcion" +
                                                        " WHERE IdTipo = @IdTipo and IdInventario = @IdInventario";

                dataAdapter.UpdateCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar, 0, "Descripcion");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTipo", SqlDbType.Int, 0, "IdTipo");
                dataAdapter.UpdateCommand.Parameters.Add("@IdInventario", SqlDbType.Int, 0, "IdInventario");

                dataAdapter.Update(dataSet, "TiposEquipos");

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
                connectionEpsSql.Close();
            }
            return tipo;
        }

        /// <summary>
        /// Elimina un elemento del catalogo de tipos de equipos
        /// </summary>
        public void DeleteTipoEquipo()
        {
            SqlConnection sqlConne = Conexion.GetConexion();

            try
            {
                sqlConne.Open();

                string selstr = "Delete FROM TiposEquipos WHERE idTipo = @idTipo AND idInventario = " + AccesoUsuarioModel.Grupo;
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter idTipo = cmd.Parameters.Add("@idTipo", SqlDbType.Int, 0);
                idTipo.Value = tipo.IdElemento;

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

        private int GetLastId()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            int maxId = 0;

            try
            {
                sqlConne.Open();

                string selstr = "SELECT MAX(idTipo) as idTipo FROM TiposEquipos WHERE idInventario = @idInventario";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter tipoInventario = cmd.Parameters.Add("@idInventario", SqlDbType.Int, 0);
                tipoInventario.Value = AccesoUsuarioModel.Grupo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    maxId = dataReader.GetInt32(0);
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
            return maxId;
        }

        public List<CommonProperties> GetTiposEquiposForInsert(String descripcion)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<CommonProperties> tipos = new List<CommonProperties>();

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM TiposEquipos WHERE idInventario = @idInventario AND ( " + this.ArmaCadena(descripcion) + ") ORDER BY Descripcion";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter tipoInventario = cmd.Parameters.Add("@idInventario", SqlDbType.Int, 0);
                tipoInventario.Value = AccesoUsuarioModel.Grupo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        CommonProperties descEquipo = new CommonProperties();
                        descEquipo.IdElemento = Convert.ToInt32(dataReader["idTipo"]);
                        descEquipo.Descripcion = dataReader["Descripcion"].ToString();

                        tipos.Add(descEquipo);
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
            return tipos;
        }

        private String ArmaCadena(String descripcionAIngresar)
        {
            List<String> palabras = MisFunt.SplitStringWithoutStoppers(descripcionAIngresar);

            String sqlCadena = "";

            foreach (String palabra in palabras)
            {
                sqlCadena += " OR Descripcion LIKE '%" + palabra + "%'";
            }

            return sqlCadena = sqlCadena.Substring(3);
        }
    }
}