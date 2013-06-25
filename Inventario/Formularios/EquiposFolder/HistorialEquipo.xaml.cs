using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Dao;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for HistorialEquipo.xaml
    /// </summary>
    public partial class HistorialEquipo
    {
        private readonly List<HistorialPc> historial;

        public HistorialEquipo(List<HistorialPc> historial)
        {
            InitializeComponent();
            this.historial = historial;
        }

        private void RadWindow_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RgridHistorial.DataContext = historial;
        }




    }
}
