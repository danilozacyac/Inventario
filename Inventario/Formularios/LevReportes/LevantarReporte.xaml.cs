using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Dao;
using DaoProject.Model;
using ScjnUtilities;

namespace Inventario.Formularios.LevReportes
{
    /// <summary>
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte 
    {

        /// <summary>
        /// Almacena el listado de equipos que tienen el SC solicitado
        /// </summary>
        ObservableCollection<Equipos> resultadosObtenidos;

        /// <summary>
        /// Equipo sobre el cual se realizará alguna acción
        /// </summary>
        private Equipos equipo;

        /// <summary>
        /// Servidor Público que tiene asignado el equipo
        /// </summary>
        ServidoresPublicos servidor;

      

        public LevantarReporte()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxReporto.DataContext = new ServidoresModel().GetUsuariosReporte();

                DpFechaInicio.SelectableDateStart = DateTime.Now;

        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Reporte.IsEnabled = false;
            equipo = null;
            if (String.IsNullOrEmpty(TxtScEquipo.Text) || String.IsNullOrWhiteSpace(TxtScEquipo.Text))
            {
                return;
            }

            resultadosObtenidos = new EquiposModel().GetEquiposPorParametro("SC_Equipo", TxtScEquipo.Text);

            if (resultadosObtenidos.Count() == 0)
            {
                MessageBox.Show("El número de inventario ingresado No existe. Favor de verificar");
                EquipoAlta.DataContext = new Equipos();
                servidor = new ServidoresPublicos();
                TxtUsuario.Text = String.Empty;
                return;
            }
            else if (resultadosObtenidos.Count() == 1)
            {
                CbxTipoEquipo.DataContext = resultadosObtenidos;
                CbxTipoEquipo.SelectedIndex = 0;
            }
            else
            {
                CbxTipoEquipo.DataContext = resultadosObtenidos;
            }
            Reporte.IsEnabled = true;
        }

        private void CbxTipoEquipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            equipo = CbxTipoEquipo.SelectedItem as Equipos;

            EquipoAlta.DataContext = equipo;
            servidor = new ServidoresModel().GetUsuarioPorExpediente(equipo.Expediente);

            TxtUsuario.Text = servidor.Nombre;
        }

        private void DpFechaInicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (DpFechaInicio.SelectedDate == null)
            {
                MessageBox.Show("Debes seleccionar la fecha en que se esta levantando el reporte");
                return;
            }

            if (CbxReporto.SelectedIndex == -1)
            {
                MessageBox.Show("Debes indicar quien fue el encargado de levantar el reporte");
                return;
            }

            if (String.IsNullOrEmpty(TxtProblema.Text) || String.IsNullOrWhiteSpace(TxtProblema.Text))
            {
                MessageBox.Show("Debes ingresar la razón  por la cual se está levantando el reporte dentro del campo PROBLEMA");
                return;
            }

            LevantaReporte reporte = new LevantaReporte();
            reporte.FechaReporte = DpFechaInicio.SelectedValue;
            reporte.NumReporte = Convert.ToInt32(TxtNumReporte.Text);
            reporte.IdEquipo = equipo.IdEquipo;
            reporte.Expediente = servidor.Expediente;
            reporte.Reporto = (CbxReporto.SelectedItem as ServidoresPublicos).Expediente;
            reporte.Problema = TxtProblema.Text;
            reporte.ScEquipo = TxtScEquipo.Text;
            reporte.TipoEquipo = CbxTipoEquipo.Text;
            reporte.Nombre = TxtUsuario.Text;
            reporte.ReportoStr = CbxReporto.Text;

            new LevantaReporteModel().SetNewReporte(reporte);

            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtNumReporte_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        
        
    }
}
