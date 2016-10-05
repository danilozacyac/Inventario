using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DaoProject.Dao;

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
