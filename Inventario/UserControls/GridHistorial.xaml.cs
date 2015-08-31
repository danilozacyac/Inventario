using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Model;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridHistorial.xaml
    /// </summary>
    public partial class GridHistorial : UserControl
    {
        public GridHistorial()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GHistorial.DataContext = new EquiposModel().GetBajas().Tables[0].DefaultView;
        }
    }
}
