using System;
using System.Linq;
using System.Windows;
using Inventario.Dao;
using Inventario.Model;
using Inventario.Singleton;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for BuscarEquipo.xaml
    /// </summary>
    public partial class BuscarEquipo
    {
        private Equipos equipo;
        ServidoresPublicos servidor;

        public BuscarEquipo()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.DataContext = TiposEquiposSingleton.Tipos;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            equipo = null;
            if (String.IsNullOrEmpty(TxtScEquipo.Text) || String.IsNullOrWhiteSpace(TxtScEquipo.Text))
            {
                return;
            }

            if (RcbTipoEquipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione la categoría a la que pertenece el equipo buscado");
                return;
            }

            equipo = new EquiposModel().GetEquiposPorParametro("SC_Equipo", TxtScEquipo.Text, Convert.ToInt32(RcbTipoEquipo.SelectedValue));

            if (equipo != null)
            {
                EquipoAlta.DataContext = equipo;
                servidor = new ServidoresModel().GetUsuarioPorExpediente(equipo.Expediente);
                Usuario.DataContext = servidor;
                //RcbUbicacion.SelectedValue = servidor.IdUbicacion;
                //RcbTitulos.SelectedValue = servidor.IdTitulo;
                //RcbAreas.SelectedValue = servidor.IdArea;
                ActionButtons.Visibility = Visibility.Visible;
                Usuario.Visibility = Visibility.Visible;
            }
            else
            {
                EquipoAlta.DataContext = new Equipos();
                Usuario.DataContext = new ServidoresPublicos();
                ActionButtons.Visibility = Visibility.Collapsed;
                Usuario.Visibility = Visibility.Collapsed;
                MessageBox.Show("El número de inventario ingresado No existe. Favor de verificar");
            }
        }

        private void RbtnBajaEquipo_Click(object sender, RoutedEventArgs e)
        {
            DeleteEquipo delete = new DeleteEquipo(equipo);
            delete.Show();
            this.Close();
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateUsuarioEquipo update = new UpdateUsuarioEquipo(servidor, equipo);
            update.Owner = this.Owner;
            update.Show();
            this.Close();
        }

        private void RbtnEditaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateEquipo update = new UpdateEquipo(equipo);
            update.Show();
            this.Close();

        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
