using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using Telerik.Windows.Controls;
using DaoProject.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for MobiliarioAsignado.xaml
    /// </summary>
    public partial class MobiliarioAsignado
    {
        private Mobiliario mobiliario;
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

        private void RbtnEditaEquipo_Click(object sender, RoutedEventArgs e)
        {
            if (mobiliario == null)
            {
                MessageBox.Show("Selecciona el artículo que deseas actualizar");
                return;
            }

            AddUpdateMobiliario add = new AddUpdateMobiliario(mobiliario);
            add.ShowDialog();
        }

        private void RBtnHistMobiliario_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<HistorialMobiliario> historial = new MobiliarioModel().GetHistorial(mobiliario);
            HistorialMobiliarioWin showHistorial = new HistorialMobiliarioWin(historial);
            showHistorial.Owner = this;
            showHistorial.ShowDialog();
        }
    }
}
