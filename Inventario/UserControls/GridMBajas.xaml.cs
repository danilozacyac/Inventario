using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DaoProject.Model;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridMBajas.xaml
    /// </summary>
    public partial class GridMBajas : UserControl
    {
        public GridMBajas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GMBajas.DataContext = new MobiliarioModel().GetBajas().Tables[0].DefaultView;
        }
    }
}
