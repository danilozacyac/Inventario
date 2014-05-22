using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using Telerik.Windows.Controls;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for MobiliarioAsignado.xaml
    /// </summary>
    public partial class MobiliarioAsignado
    {
        Mobiliario mobiliario;
        private readonly ServidoresPublicos servidor;

        public MobiliarioAsignado(ServidoresPublicos servidor)
        {
            InitializeComponent();
            this.servidor = servidor;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.GridMobiliario.DataContext = servidor.Mobiliario;
            this.Header = "Mobiliario asignado a: " + servidor.Nombre;

        }

        private void RbtnBajaEquipo_Click(object sender, RoutedEventArgs e)
        {
            DeleteMobiliario delete = new DeleteMobiliario(mobiliario);
            delete.Owner = this.Owner;
            delete.ShowDialog();
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateMobiliarioUsuario update = new UpdateMobiliarioUsuario(servidor, mobiliario);
            update.Owner = this.Owner;
            update.ShowDialog();
        }

        private void GridMobiliario_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            mobiliario = GridMobiliario.SelectedItem as Mobiliario;
            ActionButtons.Visibility = Visibility.Visible;
        }
    }
}
