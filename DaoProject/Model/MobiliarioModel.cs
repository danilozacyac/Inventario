using System;
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
    public class MobiliarioModel
    {
        private readonly Mobiliario mobiliario;

        public MobiliarioModel() { }
        public MobiliarioModel(Mobiliario mobiliario)
        {
            this.mobiliario = mobiliario;
        }




        /// <summary>
        /// Devuelve la lista de mobiliario que tiene cada servidor público bajo su resguardo
        /// </summary>
        /// <param name="expediente"></param>
        /// <returns></returns>
        //public List<Mobiliario> GetMobiliarioPorServidorPublico(int expediente)
        //{
        //    SqlConnection sqlConne = Conexion.GetConexion();
        //    SqlDataReader dataReader;

        //    List<Mobiliario> listaMobiliario = new List<Mobiliario>();

        //    try
        //    {
        //        sqlConne.Open();

        //        string selstr = "SELECT * FROM Mobiliario WHERE Expediente = @Expediente";
        //        SqlCommand cmd = new SqlCommand(selstr, sqlConne);
        //        SqlParameter usuario = cmd.Parameters.Add("@Expediente", SqlDbType.NVarChar, 0);
        //        usuario.Value = expediente;

        //        dataReader = cmd.ExecuteReader();

        //        if (dataReader.HasRows)
        //        {
        //            while (dataReader.Read())
        //            {

        //                Mobiliario mobiliarioL = new Mobiliario();
        //                mobiliarioL.IdMobiliario = Convert.ToInt32(dataReader["idMobiliario"]);
        //                mobiliarioL.IdTipoMobiliario = Convert.ToInt32(dataReader["idTipo"]);
        //                mobiliarioL.Inventario = Convert.ToInt32(dataReader["NoInventario"]);
        //                mobiliarioL.Expediente = expediente;
        //                mobiliarioL.Observaciones = dataReader["Observaciones"].ToString();
        //                mobiliarioL.FechaAlta = MiscFunt.ConvertReaderToDateTime(dataReader, "FechaAlta");
        //                mobiliarioL.FechaModificacion = MiscFunt.ConvertReaderToDateTime(dataReader, "FechaModificacion");

        //                listaMobiliario.Add(mobiliarioL);
        //            }
        //        }
        //    }
        //    catch (SqlException sql)
        //    {
        //        MessageBox.Show("Error ({0}) : {1}" + sql.Source + sql.Message, "Error Interno");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, "Error Interno");
        //    }
        //    finally
        //    {
        //        sqlConne.Close();
        //    }
        //    return listaMobiliario;
        //}


        /// <summary>
        /// Devuelve la lista de mobiliario de acuerdo al parametro solicitado
        /// </summary>
        /// <param name="parametro">Columna de la base de datos contra la cual se buscara</param>
        /// <param name="valor">Valor Buscado</param>
        /// <returns></returns>
        public ObservableCollection<Mobiliario> GetMobiliarioPorParametro(String parametro, String valor)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;
            ObservableCollection<Mobiliario> listaMobiliario = new ObservableCollection<Mobiliario>();

            int number = 0;
            int.TryParse(valor.ToString(), out number);

            try
            {
                sqlConne.Open();

                string selstr = "SELECT M.*,T.Descripcion FROM Mobiliario M INNER JOIN TiposEquipos T ON T.idTipo = M.idTipo WHERE "
                    + parametro + " = @Parametro AND T.idInventario = 2";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter param = cmd.Parameters.Add("@Parametro", (number > 0) ? SqlDbType.Int : SqlDbType.NVarChar, 0);
                param.Value = valor;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        Mobiliario mobiliarioL = new Mobiliario();
                        mobiliarioL.IdMobiliario = Convert.ToInt32(dataReader["idMobiliario"]);
                        mobiliarioL.IdTipoMobiliario = Convert.ToInt32(dataReader["idTipo"]);
                        mobiliarioL.TipoMobiliario = dataReader["Descripcion"].ToString();
                        mobiliarioL.Inventario = Convert.ToInt32(dataReader["NoInventario"]);
                        mobiliarioL.Expediente = Convert.ToInt32(dataReader["Expediente"]);
                        mobiliarioL.Observaciones = dataReader["Observaciones"].ToString();
                        mobiliarioL.FechaAlta = MisFunt.ConvertReaderToDateTime(dataReader, "FechaAlta");
                        mobiliarioL.FechaModificacion = MisFunt.ConvertReaderToDateTime(dataReader, "FechaModificacion");

                        listaMobiliario.Add(mobiliarioL);
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
            return listaMobiliario;
        }

        /// <summary>
        /// Agrega un nuevo bien (mobiliario) al catálogo de la coordinación
        /// </summary>
        public void SetNewMobiliario()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            try
            {
                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlCadena = "SELECT * FROM Mobiliario WHERE expediente = 0";

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, sqlConne);

                dataAdapter.Fill(dataSet, "Mobiliario");

                dr = dataSet.Tables["Mobiliario"].NewRow();
                dr["idTipo"] = mobiliario.IdTipoMobiliario;
                dr["NoInventario"] = mobiliario.Inventario;
                dr["Expediente"] = mobiliario.Expediente;
                dr["Observaciones"] = mobiliario.Observaciones;

                dataSet.Tables["Mobiliario"].Rows.Add(dr);

                dataAdapter.InsertCommand = sqlConne.CreateCommand();
                dataAdapter.InsertCommand.CommandText = "INSERT INTO Mobiliario(idTipo,NoInventario,Expediente,Observaciones,FechaAlta,FechaModificacion)" +
                                                        " VALUES(@idTipo,@NoInventario,@Expediente,@Observaciones,SysDateTime(),SysDateTime())";

                dataAdapter.InsertCommand.Parameters.Add("@idTipo", SqlDbType.Int, 0, "idTipo");
                dataAdapter.InsertCommand.Parameters.Add("@NoInventario", SqlDbType.NVarChar, 0, "NoInventario");
                dataAdapter.InsertCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.InsertCommand.Parameters.Add("@Observaciones", SqlDbType.Text, 0, "Observaciones");

                dataAdapter.Update(dataSet, "Mobiliario");

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
        /// Actualiza los datos del mobiliario en cuestión
        /// </summary>
        public void UpdateMobiliario()
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Mobiliario WHERE idMobiliario = " + mobiliario.IdMobiliario;

            try
            {

                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Mobiliario");

                dr = dataSet.Tables["Mobiliario"].Rows[0];
                dr.BeginEdit();
                dr["idTipo"] = mobiliario.IdTipoMobiliario;
                dr["NoInventario"] = mobiliario.Inventario;
                dr["Observaciones"] = mobiliario.Observaciones;
                dr["FechaModificacion"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr.EndEdit();


                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Mobiliario SET idTipo = @idTipo, NoInventario = @NoInventario," +
                                                        "Observaciones = @Observaciones, FechaModificacion = @FechaModificacion " +
                                                        "WHERE idMobiliario = @idMobiliario";

                dataAdapter.UpdateCommand.Parameters.Add("@idTipo", SqlDbType.Int, 0, "idTipo");
                dataAdapter.UpdateCommand.Parameters.Add("@NoInventario", SqlDbType.VarChar, 0, "NoInventario");
                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.Text, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaModificacion", SqlDbType.Date, 0, "FechaModificacion");
                dataAdapter.UpdateCommand.Parameters.Add("@idMobiliario", SqlDbType.Int, 0, "idMobiliario");

                dataAdapter.Update(dataSet, "Mobiliario");

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
        }


        public void UpdateMobiliario(HistorialMobiliario historial)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Mobiliario WHERE idMobiliario = " + mobiliario.IdMobiliario;

            try
            {

                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Mobiliario");

                dr = dataSet.Tables["Mobiliario"].Rows[0];
                dr.BeginEdit();
                dr["Expediente"] = historial.ExpActual;
                dr["Observaciones"] = historial.Observaciones;
                dr.EndEdit();

                //dataSet.Tables["Mobiliario"].Rows.Add(dr);

                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Mobiliario SET Expediente = @Expediente,Observaciones = @Observaciones,FechaModificacion = SysDateTime() " +
                                                        "WHERE idMobiliario = @idMobiliario";

                dataAdapter.UpdateCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.Text, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@idMobiliario", SqlDbType.Int, 0, "idMobiliario");

                dataAdapter.Update(dataSet, "Mobiliario");

                dataSet.Dispose();
                dataAdapter.Dispose();

                this.InsertaHistorialMobiliario(historial);
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
        }

        /// <summary>
        /// Inserta el registro del movimiento de reasignacion de mobiliario  dentro de la tabla historial
        /// </summary>
        /// <param name="servidorActual"></param>
        /// <param name="servidorNuevo"></param>
        private void InsertaHistorialMobiliario(HistorialMobiliario historial)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM HistorialMobiliario WHERE idMovimiento = 0";

            try
            {

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

                dataAdapter.Fill(dataSet, "Mobiliario");

                dr = dataSet.Tables["Mobiliario"].NewRow();
                dr["idMobiliario"] = historial.IdMobiliario;
                dr["expAnterior"] = historial.ExpAnterior;
                dr["expActual"] = historial.ExpActual;
                dr["Observaciones"] = historial.Observaciones;
                dr["usuarioModifica"] = AccesoUsuarioModel.Usuario;

                dataSet.Tables["Mobiliario"].Rows.Add(dr);

                dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
                dataAdapter.InsertCommand.CommandText = "INSERT INTO HistorialMobiliario(idMobiliario,expAnterior,expActual,Observaciones,FechaReasignacion,usuarioModifica)" +
                                                        " VALUES(@idMobiliario,@expAnterior,@expActual,@Observaciones,SysDateTime(),@usuarioModifica)";

                dataAdapter.InsertCommand.Parameters.Add("@idMobiliario", SqlDbType.Int, 0, "idMobiliario");
                dataAdapter.InsertCommand.Parameters.Add("@expAnterior", SqlDbType.Int, 0, "expAnterior");
                dataAdapter.InsertCommand.Parameters.Add("@expActual", SqlDbType.Int, 0, "expActual");
                dataAdapter.InsertCommand.Parameters.Add("@Observaciones", SqlDbType.Text, 0, "Observaciones");
                dataAdapter.InsertCommand.Parameters.Add("@usuarioModifica", SqlDbType.VarChar, 0, "usuarioModifica");

                dataAdapter.Update(dataSet, "Mobiliario");

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
        }

        public void BajaMobiliario( String observaciones)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlCommand cmd;

            cmd = sqlConne.CreateCommand();
            cmd.Connection = sqlConne;

            try
            {
                sqlConne.Open();

                    this.ActualizaObservacionesMobiliarioBaja(mobiliario, observaciones);
                    cmd.CommandText = "DELETE FROM Mobiliario WHERE NoInventario = '" + mobiliario.Inventario + "'";
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
        
        private void ActualizaObservacionesMobiliarioBaja(Mobiliario mobiliario, String observaciones)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Mobiliario WHERE NoInventario = '" + mobiliario.Inventario + "'";

            try
            {

                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Mobiliario");

                dr = dataSet.Tables["Mobiliario"].Rows[0];
                dr.BeginEdit();
                dr["Observaciones"] = observaciones;
                dr.EndEdit();


                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Mobiliario SET  Observaciones = @Observaciones " +
                                                        "WHERE NoInventario = @NoInventario ";

                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@NoInventario", SqlDbType.VarChar, 0, "NoInventario");

                dataAdapter.Update(dataSet, "Mobiliario");

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
        }


        #region Historial


        /// <summary>
        /// Devuelve el listado de movimientos que ha registrado un equipo
        /// </summary>
        /// <param name="equipo"></param>
        /// <returns></returns>
        public ObservableCollection<HistorialMobiliario> GetHistorial(Equipos equipo)
        {
            ObservableCollection<HistorialMobiliario> historiales = new ObservableCollection<HistorialMobiliario>();

            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM HistorialMobiliario  WHERE NoInventario = @Inventario ORDER BY fechaReasignacion ";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter inventario = cmd.Parameters.Add("@Inventario", SqlDbType.Int, 0);
                inventario.Value = equipo.ScEquipo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        HistorialMobiliario historial = new HistorialMobiliario();
                        historial.IdMovimiento = Convert.ToInt32(dataReader["IdMovimiento"]);
                        historial.ExpAnterior = Convert.ToInt32(dataReader["ExpAnterior"]);
                        historial.ExpActual = Convert.ToInt32(dataReader["ExpActual"]);
                        historial.Observaciones = dataReader["Observaciones"].ToString();
                        historial.FechaReasignacion = MisFunt.ConvertReaderToDateTime(dataReader, "FechaReasignacion"); 

                        historiales.Add(historial);
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
            return historiales;
        }

        #endregion

    }
}
