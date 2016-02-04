using DaoProject.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for HistorialMobiliario.xaml
    /// </summary>
    public partial class HistorialMobiliarioWin 
    {

        private ObservableCollection<HistorialMobiliario> historial;

        public HistorialMobiliarioWin(ObservableCollection<HistorialMobiliario> historial)
        {
            InitializeComponent();
            this.historial = historial;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RgridHistorial.DataContext = historial;
        }
    }
}
