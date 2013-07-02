using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Inventario.Dao;
using Inventario.Singleton;
using Telerik.Windows.Controls;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para PresentaUsuarios.xaml
    /// </summary>
    public partial class PresentaUsuarios : UserControl
    {
        public ServidoresPublicos ServidorSeleccionado = null;
        public Equipos EquipoSeleccionado = null;
        public Mobiliario MobilSeleccionado = null;

        private List<ServidoresPublicos> listaServidores;

        public PresentaUsuarios()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listaServidores = ServidoresSingleton.Servidores;

            this.DataContext = listaServidores;
        }

        private void TileServidores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServidorSeleccionado = TileServidores.SelectedItem as ServidoresPublicos;
        }

        private void TileServidores_TilesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TileServidores_TileSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TileServidores_TileStateChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
        }

        private void RbtnMobiliario_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            //if (tempString.Length > 3 && tempString.Length % 2 == 0)
            //{
            ObservableCollection<ServidoresPublicos> temp = (ObservableCollection<ServidoresPublicos>)(from n in listaServidores
                                                                                                       where n.Nombre.ToUpper().Contains(tempString)
                                                                                                       select n);

            TileServidores.DataContext = temp;
            // }
        }

        private void RbtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void GridComputo_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            EquipoSeleccionado = ((RadGridView)sender).SelectedItem as Equipos;
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            //if (tempString.Length > 3 && tempString.Length % 2 == 0)
            //{
            List<ServidoresPublicos> temp = (from n in listaServidores
                                             where n.Nombre.ToUpper().Contains(tempString)
                                             select n).ToList();

            TileServidores.DataContext = temp;
            // }
        }

        private void GridMobiliario_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            MobilSeleccionado = ((RadGridView)sender).SelectedItem as Mobiliario;
        }
    }
}
