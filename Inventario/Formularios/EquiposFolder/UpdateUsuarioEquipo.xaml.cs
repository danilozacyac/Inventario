using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for UpdateUsuarioEquipo.xaml
    /// </summary>
    public partial class UpdateUsuarioEquipo
    {
        private readonly ServidoresPublicos usuarioActual;
        private ServidoresPublicos nuevoUsuario;
        private readonly Equipos equipo;

        public UpdateUsuarioEquipo(ServidoresPublicos usuarioActual, Equipos equipo)
        {
            InitializeComponent();
            this.usuarioActual = usuarioActual;
            this.equipo = equipo;
        }

        private void WinUpdateUserEquipo_Loaded(object sender, RoutedEventArgs e)
        {
            GridActual.DataContext = usuarioActual;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            nuevoUsuario = new ServidoresModel().GetUsuarioPorExpediente(Convert.ToInt32(txtAExpediente.Text));

            GridNuevo.DataContext = nuevoUsuario;
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            equipo.Observaciones = txtAObservaciones.Text;

            EquiposModel update = new EquiposModel(equipo);
            update.UpdateEquipo(usuarioActual, nuevoUsuario);

            usuarioActual.Equipos.Remove(equipo);

            equipo.FechaModificacion = DateTime.Now;

            ServidoresPublicos addToNuevo = (from n in ServidoresSingleton.Servidores
                                             where n.Expediente == nuevoUsuario.Expediente
                                             select n).ToList()[0];

            addToNuevo.Equipos.Add(equipo);

            DialogResult = true;

            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }


    }
}