using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for BuscarEquipo.xaml
    /// </summary>
    public partial class BuscarEquipo
    {
        /// <summary>
        /// Almacena el listado de equipos que tienen el SC solicitado
        /// </summary>
        ObservableCollection<Equipos> resultadosObtenidos;

        /// <summary>
        /// Equipo sobre el cual se realizará alguna acción
        /// </summary>
        private Equipos equipo;
        ServidoresPublicos servidor;

        public BuscarEquipo()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.DataContext = TiposEquiposSingleton.TiposComputo;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            equipo = null;
            if (String.IsNullOrEmpty(TxtScEquipo.Text) || String.IsNullOrWhiteSpace(TxtScEquipo.Text))
            {
                return;
            }

            resultadosObtenidos = new EquiposModel().GetEquiposPorParametro("SC_Equipo", TxtScEquipo.Text);

            if (resultadosObtenidos.Count() == 0)
            {
                EquipoAlta.DataContext = new Equipos();
                Usuario.DataContext = new ServidoresPublicos();
                ActionButtons.Visibility = Visibility.Collapsed;
                Usuario.Visibility = Visibility.Collapsed;
                MessageBox.Show("El número de inventario ingresado No existe. Favor de verificar");
            }else if (resultadosObtenidos.Count() == 1)
            {
                equipo = resultadosObtenidos[0];
                this.SetEquipoTrabajar();
            }
            else
            {
                RLstCoinciden.Visibility = Visibility.Visible;
                RLstCoinciden.DataContext = resultadosObtenidos;
            }
        }

        private void SetEquipoTrabajar()
        {
            EquipoAlta.DataContext = equipo;
            servidor = new ServidoresModel().GetUsuarioPorExpediente(equipo.Expediente);
            Usuario.DataContext = servidor;
            RcbTipoEquipo.SelectedValue = equipo.IdTipo;
            ActionButtons.Visibility = Visibility.Visible;
            Usuario.Visibility = Visibility.Visible;
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
            update.Owner = this;
            update.Show();
            this.Close();

        }

        private void RLstCoinciden_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            equipo = RLstCoinciden.SelectedItem as Equipos;
            this.SetEquipoTrabajar();
            

            RLstCoinciden.Visibility = Visibility.Collapsed;
        }
    }
}
