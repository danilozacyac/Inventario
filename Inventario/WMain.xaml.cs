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
using System.ComponentModel;

namespace Inventario
{
    /// <summary>
    /// Interaction logic for WMain.xaml
    /// </summary>
    public partial class WMain 
    {
        private IReportePdf reporte;
        int backgroundEvent = 0;


        public WMain()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            this.ShowInTaskbar(this, "Inventario CCST");
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
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

        private void RbtnConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigOptions config = new ConfigOptions();
            config.ShowDialog();
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

        #region Areas
        private void RbtnAddArea_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateArea add = new AddUpdateArea();
            add.Owner = this;
            add.Show();
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


        #endregion

        #region Usuarios


        private void AgregaUsuario_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateUsuarios add = new AddUpdateUsuarios();
            add.Owner = this;
            add.Show();
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


        #endregion

        #region Computo

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarEquipo busca = new BuscarEquipo();
            busca.Owner = this;
            busca.Show();
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

        /// <summary>
        /// Muestra el catálogo de los tipos de equipo de computo
        /// </summary>
        private void RbtnListaEquipos_Click(object sender, RoutedEventArgs e)
        {
            CatalogoTipos catalogo = new CatalogoTipos(1);
            catalogo.Owner = this;
            catalogo.ShowDialog();
        }

        #endregion

        #region Reportes Computo

        /// <summary>
        /// Permite agregar la información de el reporte que se esta levantando en informática
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnLevantaR_Click(object sender, RoutedEventArgs e)
        {
            LevantarReporte reporte = new LevantarReporte();
            reporte.Owner = this;
            reporte.ShowDialog();
        }

        /// <summary>
        /// Permite actualizar la información de los reportes que se levantan ante informática
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #endregion

        #region Mobiliario

        private void RbtnAddMobiliario_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateMobiliario add = new AddUpdateMobiliario();
            add.Owner = this;
            add.Show();
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

        private void RbtnBuscarMob_Click(object sender, RoutedEventArgs e)
        {
            BuscarMobiliario busca = new BuscarMobiliario();
            busca.Owner = this;
            busca.Show();
        }

        #endregion

        /// <summary>
        /// Esta región contiene los metodos que muestran listados. Dichos listados se encuentran dentro 
        /// de Controles de Usuario y son mostrados en elementos Pane del Control Docking
        /// </summary>
        #region Panes

        GridUsuarios grUsuarios; ListaAreas ucAreas; GridMBajas gmBajas; GridHMobiliario grhMob; GridBajas gBajas;
        GridHistorial gHistoria; GridReportes gReportes;

        /// <summary>
        /// Permite agregar elementos al panel central de la vista principal
        /// </summary>
        /// <param name="tag">Identificador que se le dará al elemento creado</param>
        /// <param name="tabTitle">Título que se mostrará en la pestaña del elemento</param>
        /// <param name="organoControl">Elemento a mostrar</param>
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

        /// <summary>
        /// Obtiene, si existe, el panel que se pretende crear
        /// </summary>
        /// <param name="tag">Identificador del panel</param>
        /// <returns></returns>
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


        private void RbtnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            grUsuarios = new GridUsuarios();
            this.AddPane(1, "Lista de Usuarios", grUsuarios);
        }

        
        private void RbtnListaAreas_Click(object sender, RoutedEventArgs e)
        {
            ucAreas = new ListaAreas();
            this.AddPane(2, "Listado de Áreas", ucAreas);
        }


        /// <summary>
        /// Muestra el equipo de computo dado de baja hasta la fecha
        /// </summary>
        private void RBtnBajas_Click(object sender, RoutedEventArgs e)
        {
            gBajas = new GridBajas();
            this.AddPane(3, "Bajas de Equipo de Computo", gBajas);
        }

        /// <summary>
        /// Muestra el mobiliario dado de baja hasta la fecha
        /// </summary>
        private void RBtnBMobiliario_Click(object sender, RoutedEventArgs e)
        {
            gmBajas = new GridMBajas();
            this.AddPane(4, "Bajas de Mobiliario", gmBajas);
        }


        /// <summary>
        /// Muestra la información de los reportes levantados ante el área de informática
        /// </summary>
        private void RBtnListadoR_Click(object sender, RoutedEventArgs e)
        {
            backgroundEvent = 5;
            gReportes = new GridReportes();
            this.LaunchBusyIndicator();
        }



        #endregion

        #region Imprime Reportes y exporta info

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

        #endregion


        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            object x;

            if(backgroundEvent ==5)
                x = LevantaReporteSingleton.Reportes;
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           if(backgroundEvent == 5)
               this.AddPane(5, "Reportes informática", gReportes);

            //Dispatcher.BeginInvoke(new Action<ObservableCollection<Organismos>>(this.UpdateGridDataSource), e.Result);
            this.BusyIndicator.IsBusy = false;
        }

        private void LaunchBusyIndicator()
        {
            if (!worker.IsBusy)
            {
                this.BusyIndicator.IsBusy = true;
                worker.RunWorkerAsync();

            }
        }

        #endregion
       
    }
}
