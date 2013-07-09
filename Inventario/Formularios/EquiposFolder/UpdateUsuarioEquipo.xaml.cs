using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;

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