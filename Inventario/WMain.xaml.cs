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
using Inventario.UserControls;
using Reporting;
using Reporting.Exporta;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Inventario.Formularios.LevReportes;

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

            if (AccesoUsuarioModel.Grupo == 1 || AccesoUsuarioModel.Grupo == 3)
                RbtnBuscar.IsEnabled = true;

            if (AccesoUsuarioModel.Grupo == 2 || AccesoUsuarioModel.Grupo == 3)
                RbtnBuscarMob.IsEnabled = true;

          
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

        GridUsuarios grUsuarios;
        private void RbtnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            grUsuarios = new GridUsuarios();

            this.AddPane(1, "Lista de Usuarios", grUsuarios);

        }

        private RadPane GetExistingPane(int tag)
        {
            RadPane pane = null;

            foreach (RadPane panes in Docking.Panes)
            {
                if (Convert.ToInt32(panes.Tag) == tag)
                {
                    pane = panes;
                    break;
                }
            }
            return pane;
        }

        private void AddPane(int tag, string tabTitle, object organoControl)
        {

            RadPane existingPane = this.GetExistingPane(tag);

            if (existingPane == null)
            {
                RadPane pane = new RadPane();
                pane.Tag = tag;
                pane.Header = tabTitle;
                pane.Content = organoControl;

                PanelCentral.Items.Add(pane);
                Docking.ActivePane = pane;
            }
            else
            {
                existingPane.IsHidden = false;
                Docking.ActivePane = existingPane;
            }
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
            if (grUsuarios.ServidorSeleccionado != null)
            {
                ServidoresPublicos servidor = grUsuarios.ServidorSeleccionado;

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
            else
            {
                MessageBox.Show("Seleccione el usuario del cual quiere generar el resguardo");
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
            //UpdateEquipo update = new UpdateEquipo(this.GrUsuarios.EquipoSeleccionado);
            //update.Owner = this;
            //update.Show();
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            //UpdateUsuarioEquipo update = new UpdateUsuarioEquipo(GrUsuarios.ServidorSeleccionado, GrUsuarios.EquipoSeleccionado);
            //update.Show();
        }

        ListaAreas ucAreas;
        private void RbtnListaAreas_Click(object sender, RoutedEventArgs e)
        {
            ucAreas = new ListaAreas();

            RadPane pane = new RadPane();
            pane.Header = "Tesis turnadas";
            pane.Content = ucAreas;

            PanelCentral.AddItem(pane, DockPosition.Center);

        }

        private void RbtnEliminarArea_Click(object sender, RoutedEventArgs e)
        {
            if (ucAreas.AreaSeleccionada == null)
            {
                MessageBox.Show("Seleccione el área que desea eliminar");
                return;
            }

            MessageBoxResult result = MessageBox.Show("¿Esta seguro de eliminar esta área?", "Atención:", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                AddUpdateArea delete = new AddUpdateArea(ucAreas.AreaSeleccionada, 2);
                delete.Owner = this;
                delete.Show();
            }
        }

        private void RbtnUpdateArea_Click(object sender, RoutedEventArgs e)
        {
            if (ucAreas.AreaSeleccionada == null)
            {
                MessageBox.Show("Seleccione el área que desea editar");
                return;
            }

            AddUpdateArea update = new AddUpdateArea(ucAreas.AreaSeleccionada, 1);
            update.Owner = this;
            update.Show();
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
                BuscarEquipo busca = new BuscarEquipo();
                busca.Owner = this;
                busca.Show();
        }

        private void RbtnBuscarMob_Click(object sender, RoutedEventArgs e)
        {
            BuscarMobiliario busca = new BuscarMobiliario();
            busca.Owner = this;
            busca.Show();
        }

        private void RbtnConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigOptions config = new ConfigOptions();
            config.ShowDialog();
        }

        private void RbtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (grUsuarios.ServidorSeleccionado != null)
            {
                AddUpdateUsuarios update = new AddUpdateUsuarios(grUsuarios.ServidorSeleccionado);
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

        GridHistorial gHistoria;
        private void RbtnHistorial_Click(object sender, RoutedEventArgs e)
        {
            gHistoria = new GridHistorial();

            RadPane pane = new RadPane();
            pane.Header = "Historial";
            pane.Content = gHistoria;

            PanelCentral.AddItem(pane, DockPosition.Center);

            
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
            //DeleteMobiliario delete = new DeleteMobiliario(GrUsuarios.MobilSeleccionado);
            //delete.Show();
        }

        private void RbtnReasigMobiliario_Click(object sender, RoutedEventArgs e)
        {
            //if (GrUsuarios.MobilSeleccionado == null)
            //    return;

            //UpdateMobiliarioUsuario update = new UpdateMobiliarioUsuario(GrUsuarios.ServidorSeleccionado, GrUsuarios.MobilSeleccionado);
            //update.Owner = this;
            //update.Show();
        }

        private void RbtnEditMobiliario_Click(object sender, RoutedEventArgs e)
        {
            //if (GrUsuarios.MobilSeleccionado == null)
            //{
            //    MessageBox.Show("Selecciona el artículo que deseas actualizar");
            //    return;
            //}

            //AddUpdateMobiliario add = new AddUpdateMobiliario(GrUsuarios.MobilSeleccionado);
            //add.ShowDialog();
        }

        private void ListaMobiliario_Click(object sender, RoutedEventArgs e)
        {
            CatalogoTipos catalog = new CatalogoTipos(2);
            catalog.ShowDialog();
        }

        GridBajas gBajas;
        private void RBtnBajas_Click(object sender, RoutedEventArgs e)
        {
            gBajas = new GridBajas();

            RadPane pane = new RadPane();
            pane.Header = "Bajas";
            pane.Content = gBajas;

            PanelCentral.AddItem(pane, DockPosition.Center);
            
        }

        GridHMobiliario grhMob;
        private void RBtnHMobiliario_Click(object sender, RoutedEventArgs e)
        {
            grhMob = new GridHMobiliario();

            RadPane pane = new RadPane();
            pane.Header = "Historial";
            pane.Content = grhMob;

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        GridMBajas gmBajas;
        private void RBtnBMobiliario_Click(object sender, RoutedEventArgs e)
        {
            gmBajas = new GridMBajas();
            RadPane pane = new RadPane();
            pane.Header = "Bajas Mobiliario";
            pane.Content = gmBajas;

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        private void RbtnListaEquipos_Click(object sender, RoutedEventArgs e)
        {
            CatalogoTipos catalogo = new CatalogoTipos(1);
            catalogo.Owner = this;
            catalogo.ShowDialog();
        }

        private void RBtnLevantaR_Click(object sender, RoutedEventArgs e)
        {
            LevantarReporte reporte = new LevantarReporte();
            reporte.Owner = this;
            reporte.ShowDialog();
        }

        private void RBtnEditaR_Click(object sender, RoutedEventArgs e)
        {
            if (gReportes == null || gReportes.selectedReporte == null)
            {
                MessageBox.Show("Selecciona el reporte que quieres modificar");
                return;
            }

            UpdateReporte reporte = new UpdateReporte(gReportes.selectedReporte);
            reporte.Owner = this;
            reporte.ShowDialog();
        }


        GridReportes gReportes;
        private void RBtnListadoR_Click(object sender, RoutedEventArgs e)
        {
            gReportes = new GridReportes();

            RadPane pane = new RadPane();
            pane.Header = "Reportes Informática";
            pane.Content = gReportes;

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

       
    }
}
