using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Model;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridHMobiliario.xaml
    /// </summary>
    public partial class GridHMobiliario : UserControl
    {
        public GridHMobiliario()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GHMobiliario.DataContext = new MobiliarioModel().GetHistorial().Tables["HMobiliario"].DefaultView;
        }
    }
}
