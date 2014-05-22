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
    public class EquiposModel
    {
        private readonly Equipos equipo;

        public EquiposModel()
        {
        }

        public EquiposModel(Equipos equipo)
        {
            this.equipo = equipo;
        }

        /// <summary>
        /// Devuelve una lista de equipos en base al parámetro de busqueda deseado
        /// </summary>
        /// <param name="parametroBusqueda">Nombre de la columna contra la cual compararemos</param>
        /// <param name="valorParametro">Valor que estamos buscando</param>
        /// <returns></returns>
        public ObservableCollection<Equipos> GetEquiposPorParametro(String parametroBusqueda, String valorParametro)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            ObservableCollection<Equipos> listaEquipos = new ObservableCollection<Equipos>();

            try
            {
                sqlConne.Open();

                string selstr = "select E.*,T.Descripcion from Equipos E INNER JOIN TiposEquipos T ON T.idTipo = E.idTipo WHERE E." +
                                parametroBusqueda + " = @valorParametro AND T.idInventario = 1";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

                SqlParameter param = cmd.Parameters.Add("@valorParametro", SqlDbType.NVarChar, 0);
                param.Value = valorParametro;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Equipos myEquipo = new Equipos();
                        myEquipo.Expediente = Convert.ToInt32(dataReader["Expediente"]);
                        myEquipo.IdEquipo = Convert.ToInt32(dataReader["idEquipo"]);
                        myEquipo.ScEquipo = dataReader["SC_Equipo"].ToString();
                        myEquipo.ScPrincipal = MisFunt.VerifyDbNullForStrings(dataReader, "SC_Principal");
                        myEquipo.IdTipo = Convert.ToInt32(dataReader["idTipo"]);
                        myEquipo.TipoEquipo = dataReader["Descripcion"].ToString();
                        myEquipo.Marca = MisFunt.VerifyDbNullForStrings(dataReader, "Marca");
                        myEquipo.Modelo = MisFunt.VerifyDbNullForStrings(dataReader, "Modelo");
                        myEquipo.NoSerie = MisFunt.VerifyDbNullForStrings(dataReader, "NoSerie");
                        myEquipo.Observaciones = MisFunt.VerifyDbNullForStrings(dataReader, "Observaciones");
                        myEquipo.Estado = MisFunt.VerifyDbNullForStrings(dataReader, "Estado");
                        myEquipo.FechaAlta = MisFunt.ConvertReaderToDateTime(dataReader, "Alta");
                        myEquipo.FechaModificacion = MisFunt.ConvertReaderToDateTime(dataReader, "Modificacion");

                        listaEquipos.Add(myEquipo);
                    }
                }

                dataReader.Close();
                selstr = null;
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
            return listaEquipos;
        }

        /// <summary>
        /// Devuelve en forma de lista el equipo que se esta buscando por número de inventario y el tipo de equipo
        /// En general solo devuelve un resultado
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        /// <param name="valorParametro"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public Equipos GetEquiposPorParametro(String parametroBusqueda, String valorParametro, int tipo)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            Equipos myEquipo = null;

            try
            {
                sqlConne.Open();

                string selstr = "select E.*,T.Descripcion from Equipos E INNER JOIN TiposEquipos T ON T.idTipo = E.idTipo WHERE E." +
                                parametroBusqueda + " = @valorParametro AND E.idTipo = @tipo AND T.idInventario = 1";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

                SqlParameter param = cmd.Parameters.Add("@valorParametro", SqlDbType.NVarChar, 0);
                param.Value = valorParametro;
                SqlParameter tipoP = cmd.Parameters.Add("@tipo", SqlDbType.Int, 0);
                tipoP.Value = tipo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        myEquipo = new Equipos();
                        myEquipo.Expediente = Convert.ToInt32(dataReader["Expediente"]);
                        myEquipo.IdEquipo = Convert.ToInt32(dataReader["idEquipo"]);
                        myEquipo.ScEquipo = dataReader["SC_Equipo"].ToString();
                        myEquipo.ScPrincipal = MisFunt.VerifyDbNullForStrings(dataReader, "SC_Principal");
                        myEquipo.IdTipo = Convert.ToInt32(dataReader["idTipo"]);
                        myEquipo.TipoEquipo = dataReader["Descripcion"].ToString();
                        myEquipo.Marca = MisFunt.VerifyDbNullForStrings(dataReader, "Marca");
                        myEquipo.Modelo = MisFunt.VerifyDbNullForStrings(dataReader, "Modelo");
                        myEquipo.NoSerie = MisFunt.VerifyDbNullForStrings(dataReader, "NoSerie");
                        myEquipo.Observaciones = MisFunt.VerifyDbNullForStrings(dataReader, "Observaciones");
                        myEquipo.Estado = MisFunt.VerifyDbNullForStrings(dataReader, "Estado");
                        myEquipo.FechaAlta = MisFunt.ConvertReaderToDateTime(dataReader, "Alta");
                        myEquipo.FechaModificacion = MisFunt.ConvertReaderToDateTime(dataReader, "Modificacion");
                    }
                }

                dataReader.Close();
                selstr = null;
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
            return myEquipo;
        }

        /// <summary>
        /// Agrega los datos de un equipo a la base de datos
        /// </summary>
        //public void SetNewEquipo()
        //{
        //    SqlConnection connectionEpsSql = Conexion.GetConexion();
        //    SqlDataAdapter dataAdapter;

        //    DataSet dataSet = new DataSet();
        //    DataRow dr;

        //    string sqlCadena = "SELECT * FROM Equipos WHERE expediente = 0";

        //    try
        //    {
        //        dataAdapter = new SqlDataAdapter();
        //        dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

        //        dataAdapter.Fill(dataSet, "Equipos");

        //        dr = dataSet.Tables["Equipos"].NewRow();
        //        dr["SC_Equipo"] = equipo.ScEquipo;
        //        dr["Expediente"] = equipo.Expediente;
        //        dr["idTipo"] = equipo.IdTipo;
        //        dr["Marca"] = equipo.Marca;
        //        dr["Modelo"] = equipo.Modelo;
        //        dr["NoSerie"] = equipo.NoSerie;
        //        dr["Observaciones"] = equipo.Observaciones;
        //        dr["Estado"] = "A";
        //        dr["Alta"] = DateTime.Now.ToString("yyyy/MM/dd");
        //        dr["Modificacion"] = DateTime.Now.ToString("yyyy/MM/dd");

        //        dataSet.Tables["Equipos"].Rows.Add(dr);

        //        dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
        //        dataAdapter.InsertCommand.CommandText = "INSERT INTO Equipos(SC_Equipo,Expediente,idTipo,Marca,Modelo,NoSerie,Observaciones,Estado,Alta,Modificacion)" +
        //                                                " VALUES(@SC_Equipo,@Expediente,@idTipo,@Marca,@Modelo,@NoSerie,@Observaciones,@Estado,@Alta,@Modificacion)";

        //        dataAdapter.InsertCommand.Parameters.Add("@SC_Equipo", SqlDbType.VarChar, 0, "SC_Equipo");
        //        dataAdapter.InsertCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
        //        dataAdapter.InsertCommand.Parameters.Add("@idTipo", SqlDbType.Int, 0, "idTipo");
        //        dataAdapter.InsertCommand.Parameters.Add("@Marca", SqlDbType.VarChar, 0, "Marca");
        //        dataAdapter.InsertCommand.Parameters.Add("@Modelo", SqlDbType.VarChar, 0, "Modelo");
        //        dataAdapter.InsertCommand.Parameters.Add("@NoSerie", SqlDbType.VarChar, 0, "NoSerie");
        //        dataAdapter.InsertCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");
        //        dataAdapter.InsertCommand.Parameters.Add("@Estado", SqlDbType.VarChar, 0, "Estado");
        //        dataAdapter.InsertCommand.Parameters.Add("@Alta", SqlDbType.Date, 0, "Alta");
        //        dataAdapter.InsertCommand.Parameters.Add("@Modificacion", SqlDbType.Date, 0, "Modificacion");

        //        dataAdapter.Update(dataSet, "Equipos");

        //        dataSet.Dispose();
        //        dataAdapter.Dispose();
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
        //        connectionEpsSql.Close();
        //    }
        //}

        public int SetNewEquipo()
        {
            //string message = "";
            int userId = 0;

            try
            {
                equipo.ScPrincipal = "";
                //string constr = Conexion.GetConexion();
                using (SqlConnection con = Conexion.GetConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("Inserta_Equipo"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SC_Equipo", equipo.ScEquipo);
                            cmd.Parameters.AddWithValue("@SC_Principal", equipo.ScPrincipal);
                            cmd.Parameters.AddWithValue("@Expediente", equipo.Expediente);
                            cmd.Parameters.AddWithValue("@IdTipo", equipo.IdTipo);
                            cmd.Parameters.AddWithValue("@Marca", equipo.Marca);
                            cmd.Parameters.AddWithValue("@Modelo", equipo.Modelo);
                            cmd.Parameters.AddWithValue("@NoSerie", equipo.NoSerie);
                            cmd.Parameters.AddWithValue("@Observaciones", "");
                            cmd.Parameters.AddWithValue("@Estado", "A");
                            cmd.Parameters.AddWithValue("@Alta", DateTime.Now.ToString("yyyy/MM/dd"));
                            cmd.Parameters.AddWithValue("@Modificacion", DateTime.Now.ToString("yyyy/MM/dd"));
                            cmd.Connection = con;
                            con.Open();
                            userId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
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
                //connectionEpsSql.Close();
            }
            return userId;
        }


        /// <summary>
        /// Actualiza los datos generales del equipo
        /// </summary>
        /// <param name="nuevoSc">nuevoSc del equipo</param>
        /// <param name="nuevoTipo">nuevo tipo de equipo</param>
        public void UpdateEquipo(String nuevoSc, int nuevoTipo)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Equipos WHERE SC_Equipo = '" + equipo.ScEquipo + "' AND idTipo = " + equipo.IdTipo;

            try
            {
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Equipos");

                dr = dataSet.Tables["Equipos"].Rows[0];
                dr.BeginEdit();
                dr["SC_Equipo"] = (String.IsNullOrEmpty(nuevoSc)) ? equipo.ScEquipo : nuevoSc;
                dr["Expediente"] = equipo.Expediente;
                dr["idTipo"] = (equipo.IdEquipo != nuevoTipo) ? nuevoTipo : equipo.IdTipo;
                dr["Marca"] = equipo.Marca;
                dr["Modelo"] = equipo.Modelo;
                dr["NoSerie"] = equipo.NoSerie;
                dr["Observaciones"] = equipo.Observaciones;
                dr["Estado"] = "S";
                dr["Modificacion"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr.EndEdit();

                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Equipos SET SC_Equipo = @SC_Equipo, Expediente = @Expediente, idTipo = @idTipo, Marca = @Marca," +
                                                        "Modelo = @Modelo, NoSerie = @NoSerie, Observaciones = @Observaciones, Estado = @Estado, Modificacion = @Modificacion " +
                                                        "WHERE idEquipo = @idEquipo";

                dataAdapter.UpdateCommand.Parameters.Add("@SC_Equipo", SqlDbType.VarChar, 0, "SC_Equipo");
                dataAdapter.UpdateCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.UpdateCommand.Parameters.Add("@idTipo", SqlDbType.Int, 0, "idTipo");
                dataAdapter.UpdateCommand.Parameters.Add("@Marca", SqlDbType.VarChar, 0, "Marca");
                dataAdapter.UpdateCommand.Parameters.Add("@Modelo", SqlDbType.VarChar, 0, "Modelo");
                dataAdapter.UpdateCommand.Parameters.Add("@NoSerie", SqlDbType.VarChar, 0, "NoSerie");
                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@Estado", SqlDbType.VarChar, 0, "Estado");
                dataAdapter.UpdateCommand.Parameters.Add("@Modificacion", SqlDbType.Date, 0, "Modificacion");
                dataAdapter.UpdateCommand.Parameters.Add("@idEquipo", SqlDbType.Int, 0, "idEquipo");

                dataAdapter.Update(dataSet, "Equipos");

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

        /// <summary>
        /// Actualiza el número de expediente de la persona a la que se le esta asignando el equipo
        /// </summary>
        /// <param name="servidorActual">Servidor que tenía el resguardo del equipo</param>
        /// <param name="servidorNuevo">Servidor que pasa a tener el resguardo del equipo</param>
        public void UpdateEquipo(ServidoresPublicos servidorActual, ServidoresPublicos servidorNuevo)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Equipos WHERE expediente = " + servidorActual.Expediente + " AND SC_Equipo = '" + equipo.ScEquipo + "'";

            try
            {
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Equipos");

                dr = dataSet.Tables["Equipos"].Rows[0];
                dr.BeginEdit();
                dr["Expediente"] = servidorNuevo.Expediente;
                dr["Observaciones"] = equipo.Observaciones;
                dr["Estado"] = "R";
                dr["Modificacion"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr.EndEdit();

                //dataSet.Tables["Equipos"].Rows.Add(dr);

                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Equipos SET Expediente = @Expediente, Observaciones = @Observaciones, Estado = @Estado, Modificacion = @Modificacion " +
                                                        "WHERE SC_Equipo = @SC_Equipo";

                dataAdapter.UpdateCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@Estado", SqlDbType.VarChar, 0, "Estado");
                dataAdapter.UpdateCommand.Parameters.Add("@Modificacion", SqlDbType.Date, 0, "Modificacion");
                dataAdapter.UpdateCommand.Parameters.Add("@SC_Equipo", SqlDbType.VarChar, 0, "SC_Equipo");

                dataAdapter.Update(dataSet, "Equipos");

                dataSet.Dispose();
                dataAdapter.Dispose();
                this.InsertaHistorial(servidorActual, servidorNuevo);
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
        /// Da de baja un equipo, con lo cual este no puede volver a ser asignado
        /// </summary>
        /// <param name="equiposEliminar">Lista de los equipos que se van a eliminar</param>
        /// <param name="observaciones">Observaciones que se hacen respecto a la baja del equipo</param>
        public void BajaEquipo(ObservableCollection<Equipos> equiposEliminar, String observaciones)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlCommand cmd;

            cmd = sqlConne.CreateCommand();
            cmd.Connection = sqlConne;

            try
            {
                sqlConne.Open();

                foreach (Equipos equipoElim in equiposEliminar)
                {
                    this.ActualizaObservacionesEquiposBaja(equipoElim, observaciones);
                    cmd.CommandText = "DELETE FROM Equipos WHERE SC_Equipo = '" + equipoElim.ScEquipo + "' AND idTipo = " + equipoElim.IdTipo;
                    cmd.ExecuteNonQuery();
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
        }

        private void ActualizaObservacionesEquiposBaja(Equipos equipoEl, String observaciones)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Equipos WHERE SC_Equipo = '" + equipoEl.ScEquipo + "' AND idTipo = " + equipoEl.IdTipo;

            try
            {
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);
                dataAdapter.Fill(dataSet, "Equipos");

                dr = dataSet.Tables["Equipos"].Rows[0];
                dr.BeginEdit();
                dr["Observaciones"] = observaciones;
                dr.EndEdit();

                dataAdapter.UpdateCommand = connectionEpsSql.CreateCommand();
                dataAdapter.UpdateCommand.CommandText = "UPDATE Equipos SET  Observaciones = @Observaciones " +
                                                        "WHERE SC_Equipo = @SC_Equipo AND idTipo = @idTipo ";

                dataAdapter.UpdateCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");
                dataAdapter.UpdateCommand.Parameters.Add("@SC_Equipo", SqlDbType.VarChar, 0, "SC_Equipo");
                dataAdapter.UpdateCommand.Parameters.Add("@idTipo", SqlDbType.Int, 0, "idTipo");

                dataAdapter.Update(dataSet, "Equipos");

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
        /// Registra los cambios de usuario del equipo en cuestión
        /// </summary>
        /// <param name="servidorActual">Servidor que tenía el resguardo del equipo</param>
        /// <param name="servidorNuevo">Servidor que pasa a tener el resguardo del equipo</param>
        private void InsertaHistorial(ServidoresPublicos servidorActual, ServidoresPublicos servidorNuevo)
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            string sqlCadena = "SELECT * FROM Historial WHERE movimiento = 0";

            try
            {
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

                dataAdapter.Fill(dataSet, "Historial");

                dr = dataSet.Tables["Historial"].NewRow();
                dr["SC_Equipo"] = equipo.ScEquipo;
                dr["SC_Principal"] = "";
                dr["isTipo"] = equipo.IdTipo;
                dr["LastUser"] = servidorActual.Expediente;
                dr["NewUser"] = servidorNuevo.Expediente;
                dr["Observaciones"] = equipo.Observaciones;

                dataSet.Tables["Historial"].Rows.Add(dr);

                dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
                dataAdapter.InsertCommand.CommandText = "INSERT INTO Historial(SC_Equipo,SC_Principal,isTipo,LastUser,NewUser,Observaciones,Modificacion)" +
                                                        " VALUES(@SC_Equipo,@SC_Principal,@isTipo,@LastUser,@NewUser,@Observaciones,SysDateTime())";

                dataAdapter.InsertCommand.Parameters.Add("@SC_Equipo", SqlDbType.VarChar, 0, "SC_Equipo");
                dataAdapter.InsertCommand.Parameters.Add("@SC_Principal", SqlDbType.VarChar, 0, "SC_Principal");
                dataAdapter.InsertCommand.Parameters.Add("@isTipo", SqlDbType.Int, 0, "isTipo");
                dataAdapter.InsertCommand.Parameters.Add("@LastUser", SqlDbType.BigInt, 0, "LastUser");
                dataAdapter.InsertCommand.Parameters.Add("@NewUser", SqlDbType.BigInt, 0, "NewUser");
                dataAdapter.InsertCommand.Parameters.Add("@Observaciones", SqlDbType.VarChar, 0, "Observaciones");

                dataAdapter.Update(dataSet, "Historial");

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

        /// <summary>
        /// Devuelve el listado de movimientos que ha registrado un equipo
        /// </summary>
        /// <param name="equipo"></param>
        /// <returns></returns>
        public List<HistorialPc> GetHistorial(Equipos equipo)
        {
            List<HistorialPc> historiales = new List<HistorialPc>();

            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM Historial  WHERE SC_Equipo = @sc AND isTipo = @tipo ORDER BY Modificacion ";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter inventario = cmd.Parameters.Add("@sc", SqlDbType.VarChar, 0);
                inventario.Value = equipo.ScEquipo;
                SqlParameter tipo = cmd.Parameters.Add("@tipo", SqlDbType.Int, 0);
                tipo.Value = equipo.IdTipo;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        HistorialPc historial = new HistorialPc();
                        historial.IdMovimiento = Convert.ToInt32(dataReader["Movimiento"]);
                        historial.ScEquipo = dataReader["SC_Equipo"].ToString();
                        historial.IdTipo = Convert.ToInt32(dataReader["isTipo"]);
                        historial.ExpAnterior = Convert.ToInt32(dataReader["LastUser"]);
                        historial.ExpActual = Convert.ToInt32(dataReader["NewUser"]);
                        historial.Observaciones = dataReader["Observaciones"].ToString();
                        historial.FechaModificacion = dataReader["Modificacion"].ToString();

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