using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using Inventario.Formularios.EquiposFolder;
using Inventario.Formularios.MobiliarioFolder;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridUsuarios.xaml
    /// </summary>
    public partial class GridUsuarios : UserControl
    {
        public ServidoresPublicos ServidorSeleccionado = null;
        //public Equipos EquipoSeleccionado = null;
        //public Mobiliario MobilSeleccionado = null;

        //private ObservableCollection<ServidoresPublicos> listaServidores;

        public GridUsuarios()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //if (listaServidores == null)
            //{
            //    listaServidores = ServidoresSingleton.Servidores;

                this.DataContext = ServidoresSingleton.Servidores;
            //}
        }

        private void RbtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void RGridUsers_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            ServidorSeleccionado = RGridUsers.SelectedItem as ServidoresPublicos;
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            List<ServidoresPublicos> temp = (from n in ServidoresSingleton.Servidores
                                             where n.Nombre.ToUpper().Contains(tempString)
                                             select n).ToList();

            RGridUsers.DataContext = temp;
        }

        private void RGridUsers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AccesoUsuarioModel.Grupo == 1)
            {
                EquiposAsignados asignados = new EquiposAsignados(ServidorSeleccionado);
                asignados.ShowDialog();
            }
            else if (AccesoUsuarioModel.Grupo == 2)
            {
                MobiliarioAsignado asignado = new MobiliarioAsignado(ServidorSeleccionado);
                asignado.ShowDialog();
            }
            else if (AccesoUsuarioModel.Grupo == 3)
            {
                EquiposAsignados asignados = new EquiposAsignados(ServidorSeleccionado);
                asignados.Show();

                MobiliarioAsignado asignado = new MobiliarioAsignado(ServidorSeleccionado);
                asignado.Show();
            }
        }
    }
}
