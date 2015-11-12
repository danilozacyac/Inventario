using DaoProject.Dao;
using DaoProject.Model;
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

namespace Inventario.UserControls
{
    /// <summary>
    /// Interaction logic for GridReportes.xaml
    /// </summary>
    public partial class GridReportes : UserControl
    {
        public LevantaReporte selectedReporte;

        public GridReportes()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GReporte.DataContext = new LevantaReporteModel().GetReportes();
        }

        private void GReporte_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            selectedReporte = GReporte.SelectedItem as LevantaReporte;
        }
    }
}
