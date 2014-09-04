using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for EquiposAsignados.xaml
    /// </summary>
    public partial class EquiposAsignados
    {
        Equipos equipo;
        private readonly ServidoresPublicos servidor;

        public EquiposAsignados(ServidoresPublicos servidor)
        {
            InitializeComponent();
            this.servidor = servidor;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.GridComputo.DataContext = servidor.Equipos;
            this.Header = "Equipos asignados a: " + servidor.Nombre;
        }

        private void RbtnBajaEquipo_Click(object sender, RoutedEventArgs e)
        {

            DeleteEquipo delete = new DeleteEquipo(equipo);
            delete.ShowDialog();
        }

        private void RbtnEditaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateEquipo update = new UpdateEquipo(equipo);
            update.ShowDialog();
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            
            UpdateUsuarioEquipo update = new UpdateUsuarioEquipo(servidor, equipo);
            update.Owner = this.Owner;
            update.ShowDialog();
        }


        private void GridComputo_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            equipo = GridComputo.SelectedItem as Equipos;
            ActionButtons.Visibility = Visibility.Visible;
        }
    }
}
