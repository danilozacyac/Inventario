using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using Inventario.Configuracion;
using Inventario.Formularios.Areas;
using Inventario.Formularios.EquiposFolder;
using Inventario.Formularios.MobiliarioFolder;
using Inventario.Formularios.ServidoresFolder;
using Reporting;
using Reporting.Exporta;
using Telerik.Windows.Controls;

namespace Inventario
{
    /// <summary>
    /// Interaction logic for WMain.xaml
    /// </summary>
    public partial class WMain 
    {
        private IReportePdf reporte;

        public WMain()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            this.ShowInTaskbar(this, "Inventario CCST");
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.RcbAreasReporte.ItemsSource = AreasSingleton.Areas;

            if (AccesoUsuarioModel.Grupo == 1)
                TabMobiliario.IsEnabled = false;

            if (AccesoUsuarioModel.Grupo == 2)
                TabComputo.IsEnabled = false;
        }

        public void ShowInTaskbar(RadWindow control, string title)
        {
            control.Show();
            var window = control.ParentOfType<Window>();
            window.ShowInTaskbar = true;
            window.Title = title;
            var uri = new Uri("pack://application:,,,/Inventario;component/Resources/icon.png");
            window.Icon = BitmapFrame.Create(uri);
        }

        private void AgregaUsuario_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateUsuarios add = new AddUpdateUsuarios();
            add.Owner = this;
            add.Show();
        }

        private void RbtnAddArea_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateArea add = new AddUpdateArea();
            add.Owner = this;
            add.Show();
        }

        private void RbtnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            

            this.UcUsuarios.Visibility = System.Windows.Visibility.Visible;
        }

        private void RbtnAddMobiliario_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateMobiliario add = new AddUpdateMobiliario();
            add.Owner = this;
            add.Show();
        }

        private void RribReporteGral_Click(object sender, RoutedEventArgs e)
        {
            this.ReportePorArea(0);
        }

        private void RbtnReportePersonal_Click(object sender, RoutedEventArgs e)
        {
            ServidoresPublicos servidor = UcUsuarios.ServidorSeleccionado;

            if (servidor == null)
                MessageBox.Show("Seleccione el usuario del cual quiere generar el resguardo");
            else
            {
                if (AccesoUsuarioModel.Grupo == 1)
                {
                    reporte = new REquipoComputo(servidor);
                    reporte.ReportePersonal();
                }
                else if (AccesoUsuarioModel.Grupo == 2)
                {
                    reporte = new RMobiliario(servidor);
                    reporte.ReportePersonal();
                }
            }
        }

        private void RbtnReporteAreas_Click(object sender, RoutedEventArgs e)
        {
            int area = Convert.ToInt32(RcbAreasReporte.SelectedValue);

            if (RcbAreasReporte.SelectedIndex == -1 || area == 17)
                MessageBox.Show("Seleccione el área de la cual quiere generar los resguardos");
            else
            {
                this.ReportePorArea(area);
            }
        }

        private void ReportePorArea(int area)
        {
            if (AccesoUsuarioModel.Grupo == 1)
            {
                reporte = new REquipoComputo(ServidoresSingleton.Servidores, area);
                reporte.ReportePorAreas();
            }
            else if (AccesoUsuarioModel.Grupo == 2)
            {
                reporte = new RMobiliario(ServidoresSingleton.Servidores, area);
                reporte.ReportePorAreas();
            }
        }

        private void RbtnAgregaEquipo_Click(object sender, RoutedEventArgs e)
        {
            AddEquipos add = new AddEquipos();
            add.Owner = this;
            add.Show();
        }

        private void RbtnBajaEquipo_Click(object sender, RoutedEventArgs e)
        {
            DeleteEquipo del = new DeleteEquipo();
            del.Owner = this;
            del.Show();
        }

        private void RbtnEditaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateEquipo update = new UpdateEquipo(this.UcUsuarios.EquipoSeleccionado);
            update.Owner = this;
            update.Show();
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateUsuarioEquipo update = new UpdateUsuarioEquipo(UcUsuarios.ServidorSeleccionado, UcUsuarios.EquipoSeleccionado);
            update.Show();
        }

        private void RbtnListaAreas_Click(object sender, RoutedEventArgs e)
        {
            this.UcAreas.Visibility = Visibility.Visible;
        }

        private void RbtnEliminarArea_Click(object sender, RoutedEventArgs e)
        {
            if (UcAreas.AreaSeleccionada == null)
            {
                MessageBox.Show("Seleccione el área que desea eliminar");
                return;
            }

            MessageBoxResult result = MessageBox.Show("¿Esta seguro de eliminar esta área?", "Atención:", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                AddUpdateArea delete = new AddUpdateArea(UcAreas.AreaSeleccionada, 2);
                delete.Owner = this;
                delete.Show();
            }
        }

        private void RbtnUpdateArea_Click(object sender, RoutedEventArgs e)
        {
            if (UcAreas.AreaSeleccionada == null)
            {
                MessageBox.Show("Seleccione el área que desea editar");
                return;
            }

            AddUpdateArea update = new AddUpdateArea(UcAreas.AreaSeleccionada, 1);
            update.Owner = this;
            update.Show();
        }

        private void RbtnCatalogoTipos_Click(object sender, RoutedEventArgs e)
        {
            CatalogoTipos catalogo = new CatalogoTipos();
            catalogo.Owner = this;
            catalogo.Show();
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoUsuarioModel.Grupo == 1)
            {
                BuscarEquipo busca = new BuscarEquipo();
                busca.Owner = this;
                busca.Show();
            }
            else
            {
                BuscarMobiliario busca = new BuscarMobiliario();
                busca.Owner = this;
                busca.Show();
            }
        }

        private void RbtnConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigOptions config = new ConfigOptions();
            config.ShowDialog();
        }

        private void RbtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (UcUsuarios.ServidorSeleccionado != null)
            {
                AddUpdateUsuarios update = new AddUpdateUsuarios(UcUsuarios.ServidorSeleccionado);
                update.Owner = this;
                update.Show();
            }
        }

        private void Ribbon_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AccesoUsuarioModel.IsSuper)
            {
                if (Ribbon.SelectedIndex == 1)
                    AccesoUsuarioModel.Grupo = 1;
                else if (Ribbon.SelectedIndex == 2)
                    AccesoUsuarioModel.Grupo = 2;
            }
        }

        private void RbtnHistorial_Click(object sender, RoutedEventArgs e)
        {
            if (UcUsuarios.EquipoSeleccionado == null)
                return;

            if (UcUsuarios.EquipoSeleccionado.Historial == null)
            {
                UcUsuarios.EquipoSeleccionado.Historial = new EquiposModel().GetHistorial(UcUsuarios.EquipoSeleccionado);
            }

            HistorialEquipo historial = new HistorialEquipo(UcUsuarios.EquipoSeleccionado.Historial);
            historial.ShowDialog();
        }

        private void ExportaDocs_Click(object sender, RoutedEventArgs e)
        {
            RadRibbonButton clickImage = (RadRibbonButton)sender;

            if (AccesoUsuarioModel.Grupo == 1)
            {
                ExportaEquipo exporta = new ExportaEquipo();
                switch (clickImage.Tag.ToString())
                {
                    case "Excel":
                        exporta.GeneraDocumento();
                        break;
                    case "XML":
                        exporta.GeneraXml();
                        break;
                }
            }
            else
            {
                switch (clickImage.Tag.ToString())
                {
                    case "Excel":
                        ExportaMobiliario exporta = new ExportaMobiliario();
                        exporta.GeneraDocumento();
                        break;
                }
            }

            MessageBox.Show("Exportación finalizada");
        }

        private void RbtnDelMobiliario_Click(object sender, RoutedEventArgs e)
        {
            DeleteMobiliario delete = new DeleteMobiliario(UcUsuarios.MobilSeleccionado);
            delete.Show();
        }

        private void RbtnReasigMobiliario_Click(object sender, RoutedEventArgs e)
        {
            if (UcUsuarios.MobilSeleccionado == null)
                return;

            UpdateMobiliarioUsuario update = new UpdateMobiliarioUsuario(UcUsuarios.ServidorSeleccionado,UcUsuarios.MobilSeleccionado);
            update.Owner = this;
            update.Show();
        }
    }
}
