using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
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
using System.Windows.Shapes;

namespace Inventario.Formularios.LevReportes
{
    /// <summary>
    /// Interaction logic for UpdateReporte.xaml
    /// </summary>
    public partial class UpdateReporte 
    {
        private LevantaReporte reporte;

        public UpdateReporte(LevantaReporte reporte)
        {
            InitializeComponent();
            this.reporte = reporte;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = reporte;
            CbxReporto.DataContext = ServidoresSingleton.ServidoresReportan;
            CbxReporto.SelectedValue = reporte.Reporto;
            TxtProblema.Text = reporte.Problema;
            TxtAtendio.Text = reporte.Atendio;

            if (reporte.FechaCierre != null)
                DpCierre.SelectedDate = reporte.FechaCierre;

            TxtObservaciones.Text = reporte.Observaciones;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            reporte.Problema = TxtProblema.Text;
            reporte.Atendio = TxtAtendio.Text;
            reporte.Observaciones = TxtObservaciones.Text;
            reporte.FechaCierre = DpCierre.SelectedDate;

            new LevantaReporteModel().UpdateReporte(reporte);
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
