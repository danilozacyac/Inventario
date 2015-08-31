using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Model;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridBajas.xaml
    /// </summary>
    public partial class GridBajas : UserControl
    {
        public GridBajas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GBajas.DataContext = new EquiposModel().GetBajas().Tables["vBajas"].DefaultView;
        }
    }
}
