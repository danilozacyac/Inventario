using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
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
            if (listaServidores == null)
            {
                listaServidores = ServidoresSingleton.Servidores;

                this.DataContext = listaServidores;
            }
        }

        private void TileServidores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServidorSeleccionado = TileServidores.SelectedItem as ServidoresPublicos;
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

            List<ServidoresPublicos> temp = (from n in listaServidores
                                             where n.Nombre.ToUpper().Contains(tempString)
                                             select n).ToList();

            TileServidores.DataContext = temp;
        }

        private void GridMobiliario_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            MobilSeleccionado = ((RadGridView)sender).SelectedItem as Mobiliario;
        }

        private void Rpanels_Loaded(object sender, RoutedEventArgs e)
        {
            RadPanelBarItem item = sender as RadPanelBarItem;

            if (item.Name.Equals("RpanelComputo"))
            {
                if (AccesoUsuarioModel.Grupo == 1 || AccesoUsuarioModel.IsSuper)
                {
                    item.Visibility = Visibility.Visible;
                    item.IsExpanded = true;
                }
            }
            else if (item.Name.Equals("RpanelMobiliario"))
            {
                if (AccesoUsuarioModel.Grupo == 2 || AccesoUsuarioModel.IsSuper)
                {
                    item.Visibility = Visibility.Visible;
                    item.IsExpanded = (AccesoUsuarioModel.IsSuper) ? false : true;
                }
            }
        }

        private void UserControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (this.IsVisible == true)
            //{
            //    listaServidores = ServidoresSingleton.Servidores;

            //    this.DataContext = listaServidores;
            //}
        }
    }
}