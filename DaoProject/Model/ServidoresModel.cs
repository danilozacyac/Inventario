using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.DbAccess;
using DaoProject.Singleton;
using DaoProject.Utilities;

namespace DaoProject.Model
{
    public class ServidoresModel
    {
        private readonly ServidoresPublicos servidor;

        public ServidoresModel()
        {
        }

        public ServidoresModel(ServidoresPublicos servidor)
        {
            this.servidor = servidor;
        }

        /// <summary>
        /// Devuelve los datos del usuario a partir de su número de expediente, se utiliza principalmente para
        /// actualizar la información del mismo o para asignar un equipo
        /// </summary>
        /// <param name="expediente"></param>
        /// <returns></returns>
        public ServidoresPublicos GetUsuarioPorExpediente(int expediente)
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            ServidoresPublicos servidorP = null;

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM Usuarios WHERE Expediente = @Expediente";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);
                SqlParameter usuario = cmd.Parameters.Add("@Expediente", SqlDbType.NVarChar, 0);
                usuario.Value = expediente;

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    servidorP = new ServidoresPublicos();
                    servidorP.Expediente = expediente;
                    servidorP.IdTitulo = Convert.ToInt32(dataReader["idTitulo"].ToString());
                    servidorP.IdArea = Convert.ToInt32(dataReader["idArea"].ToString());
                    servidorP.IdUbicacion = Convert.ToInt32(dataReader["IdUbicacion"].ToString());
                    servidorP.Nombre = dataReader["nombre"].ToString();
                    servidorP.Puerta = dataReader["puerta"].ToString();
                    servidorP.Extension = dataReader["Extension"] as int?;
                    servidorP.IdAdscripcion = Convert.ToInt32(dataReader["idadscripcion"].ToString());
                    servidorP.Equipos = new EquiposModel().GetEquiposPorParametro("Expediente", servidorP.Expediente.ToString());
                    servidorP.Mobiliario = new MobiliarioModel().GetMobiliarioPorParametro("Expediente", servidorP.Expediente.ToString());
                }
                else
                {
                    MessageBox.Show("El número de expediente ingreado no existe. Favor de verificar");
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
            return servidorP;
        }

        /// <summary>
        /// Obtiene la lista de todos los servidores públicos adscritos a la coordinación
        /// junto con su respectivo mobiliario y equipo de computo
        /// </summary>
        /// <returns></returns>
        public List<ServidoresPublicos> GetUsuarios()
        {
            SqlConnection sqlConne = Conexion.GetConexion();
            SqlDataReader dataReader;

            List<ServidoresPublicos> servidores = new List<ServidoresPublicos>();

            try
            {
                sqlConne.Open();

                string selstr = "SELECT * FROM Usuarios WHERE userStatus = 1 ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(selstr, sqlConne);

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        ServidoresPublicos servidorP = new ServidoresPublicos();
                        servidorP.Expediente = Convert.ToInt32(dataReader["expediente"]);
                        servidorP.IdTitulo = Convert.ToInt32(dataReader["idTitulo"]);
                        servidorP.IdArea = Convert.ToInt32(dataReader["idArea"]);
                        servidorP.IdUbicacion = Convert.ToInt32(dataReader["IdUbicacion"]);
                        servidorP.Nombre = MisFunt.UppercaseWords(dataReader["nombre"].ToString().ToLower());
                        servidorP.Puerta = dataReader["puerta"].ToString();
                        servidorP.Extension = dataReader["Extension"] as int?;
                        servidorP.IdAdscripcion = Convert.ToInt32(dataReader["idadscripcion"]);
                        servidorP.Equipos = (AccesoUsuarioModel.Grupo == 1 || AccesoUsuarioModel.IsSuper) ? new EquiposModel().GetEquiposPorParametro("Expediente", servidorP.Expediente.ToString()) :
                                            new ObservableCollection<Equipos>();

                        servidorP.Mobiliario = (AccesoUsuarioModel.Grupo == 1 || AccesoUsuarioModel.IsSuper) ? new MobiliarioModel().GetMobiliarioPorParametro("Expediente", servidorP.Expediente.ToString()) :
                                               new ObservableCollection<Mobiliario>();

                        servidores.Add(servidorP);
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
            return servidores;
        }

        /// <summary>
        /// Agrega el registro de un servidor público de nuevo ingreso
        /// </summary>
        /// <param name="servidor"></param>
        public void SetNewUser()
        {
            SqlConnection connectionEpsSql = Conexion.GetConexion();
            SqlDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Usuarios WHERE expediente = 0";

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, connectionEpsSql);

                dataAdapter.Fill(dataSet, "Usuarios");

                dr = dataSet.Tables["Usuarios"].NewRow();
                dr["Expediente"] = servidor.Expediente;
                dr["idTitulo"] = servidor.IdTitulo;
                dr["Nombre"] = servidor.Nombre;
                dr["IdUbicacion"] = servidor.IdUbicacion;
                dr["Puerta"] = servidor.Puerta;
                dr["Extension"] = (servidor.Extension == null) ? 0 : servidor.Extension;
                dr["idArea"] = servidor.IdArea;
                dr["idAdscripcion"] = 1;

                dataSet.Tables["Usuarios"].Rows.Add(dr);

                dataAdapter.InsertCommand = connectionEpsSql.CreateCommand();
                dataAdapter.InsertCommand.CommandText = "INSERT INTO Usuarios(Expediente,idTitulo,Nombre,IdUbicacion,Puerta,Extension,idArea,idAdscripcion,UserStatus)" +
                                                        " VALUES(@Expediente,@idTitulo,@Nombre,@IdUbicacion,@Puerta,@Extension,@idArea,@idAdscripcion,1)";

                dataAdapter.InsertCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.InsertCommand.Parameters.Add("@idTitulo", SqlDbType.Int, 0, "idTitulo");
                dataAdapter.InsertCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 0, "Nombre");
                dataAdapter.InsertCommand.Parameters.Add("@IdUbicacion", SqlDbType.Int, 0, "IdUbicacion");
                dataAdapter.InsertCommand.Parameters.Add("@Puerta", SqlDbType.VarChar, 0, "Puerta");
                dataAdapter.InsertCommand.Parameters.Add("@Extension", SqlDbType.Int, 0, "Extension");
                dataAdapter.InsertCommand.Parameters.Add("@idArea", SqlDbType.Int, 0, "idArea");
                dataAdapter.InsertCommand.Parameters.Add("@idAdscripcion", SqlDbType.Int, 0, "idAdscripcion");

                dataAdapter.Update(dataSet, "Usuarios");

                dataSet.Dispose();
                dataAdapter.Dispose();

                servidor.Equipos = new ObservableCollection<Equipos>();
                servidor.Mobiliario = new ObservableCollection<Mobiliario>();
                ServidoresSingleton.AddUsuario(servidor);
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
        }// fin InsertarRegistro

        /// <summary>
        /// Actualiza los datos del servidor público en cuestion
        /// </summary>
        public void ActualizaInfoServidores()
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

                string sqlCadena = "SELECT * FROM Usuarios WHERE Expediente =" + servidor.Expediente;

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, sqlConne);

                dataAdapter.Fill(dataSet, "Usuarios");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["Expediente"] = servidor.Expediente;
                dr["idTitulo"] = servidor.IdTitulo;
                dr["Nombre"] = servidor.Nombre;
                dr["IdUbicacion"] = servidor.IdUbicacion;
                dr["Puerta"] = servidor.Puerta;
                dr["Extension"] = servidor.Extension;
                dr["idArea"] = servidor.IdArea;
                dr["idAdscripcion"] = servidor.IdAdscripcion;

                dr.EndEdit();

                dataAdapter.UpdateCommand = sqlConne.CreateCommand();

                string sSql = "UPDATE Usuarios SET idTitulo = @idTitulo, Nombre = @Nombre, IdUbicacion = @IdUbicacion, Puerta = @Puerta, Extension = @Extension," +
                              " idArea = @idArea, idAdscripcion = @idAdscripcion  WHERE Expediente = @Expediente";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");
                dataAdapter.UpdateCommand.Parameters.Add("@idTitulo", SqlDbType.Int, 0, "idTitulo");
                dataAdapter.UpdateCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 0, "Nombre");
                dataAdapter.UpdateCommand.Parameters.Add("@IdUbicacion", SqlDbType.Int, 0, "IdUbicacion");
                dataAdapter.UpdateCommand.Parameters.Add("@Puerta", SqlDbType.VarChar, 0, "Puerta");
                dataAdapter.UpdateCommand.Parameters.Add("@Extension", SqlDbType.Int, 0, "Extension");
                dataAdapter.UpdateCommand.Parameters.Add("@idArea", SqlDbType.Int, 0, "idArea");
                dataAdapter.UpdateCommand.Parameters.Add("@idAdscripcion", SqlDbType.Int, 0, "idAdscripcion");

                dataAdapter.Update(dataSet, "Usuarios");
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
        /// Pone al usuario en cuestión en un estado desactivado, es decir no es elegible para que se le asigne
        /// equipo ni para que se generen reportes a su nombre, el historial de este usuario permanece intacto
        /// </summary>
        public void DesactivarUsuario()
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

                string sqlCadena = "SELECT * FROM Usuarios WHERE Expediente = " + servidor.Expediente;

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(sqlCadena, sqlConne);

                dataAdapter.Fill(dataSet, "Usuarios");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["Expediente"] = servidor.Expediente;
                dr["userStatus"] = 0;

                dr.EndEdit();

                dataAdapter.UpdateCommand = sqlConne.CreateCommand();

                string sSql = "UPDATE Usuarios SET userStatus = @userStatus  WHERE Expediente = @Expediente";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@userStatus", SqlDbType.Int, 0, "userStatus");
                dataAdapter.UpdateCommand.Parameters.Add("@Expediente", SqlDbType.Int, 0, "Expediente");

                dataAdapter.Update(dataSet, "Usuarios");
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
    }
}