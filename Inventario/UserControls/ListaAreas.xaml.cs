using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Inventario.Dao;
using Inventario.Singleton;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para ListaAreas.xaml
    /// </summary>
    public partial class ListaAreas : UserControl
    {
        public CommonProperties AreaSeleccionada;

        public ListaAreas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.GAreas.DataContext = AreasSingleton.Areas;
        }

        private void GAreas_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            AreaSeleccionada = GAreas.SelectedItem as CommonProperties;
        }

        private void RbtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
