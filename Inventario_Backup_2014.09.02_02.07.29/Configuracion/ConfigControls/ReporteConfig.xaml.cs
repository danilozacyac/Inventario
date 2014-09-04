using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Inventario.Configuracion.ConfigControls
{
    /// <summary>
    /// Lógica de interacción para ReporteConfig.xaml
    /// </summary>
    public partial class ReporteConfig : UserControl
    {
        private ConfigClass configOriginal, configNueva;

        public ReporteConfig()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            configOriginal = new ConfigClass();
            configNueva = new ConfigClass();


            this.DataContext = configNueva;
            //this.RcbFonts.DataContext = Fonts.SystemFontFamilies;
            //this.RcbFonts.Text = configNueva.TipoLetraReporte;

            //Font font = new Font(configNueva.TipoLetraReporte, 1);
            FontFamily family = new FontFamily(configNueva.TipoLetraReporte);
            FontCombo.SelectedValue = family;

        }

        private void RbtnSaveReportConf_Click(object sender, RoutedEventArgs e)
        {
            configNueva.TipoLetraReporte = FontCombo.Text;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //make changes
            config.AppSettings.Settings["ResponsableComputo"].Value = configNueva.ResponsableComputo;
            config.AppSettings.Settings["ResponsableMobiliario"].Value = configNueva.ResponsableMobiliario;
            config.AppSettings.Settings["EncabezadoReportesSize"].Value = configNueva.TamEncabezadoRep;
            config.AppSettings.Settings["ContenidoReportesSize"].Value = configNueva.TamContenidoRep;
            config.AppSettings.Settings["TituloComputo"].Value = configNueva.TituloComputo;
            config.AppSettings.Settings["TituloMobiliario"].Value = configNueva.TituloMobiliario;
            config.AppSettings.Settings["EncabezadoReportesSize"].Value = configNueva.TamEncabezadoRep;
            config.AppSettings.Settings["EncabezadoReportesFont"].Value = configNueva.TipoLetraReporte;
            config.AppSettings.Settings["ContenidoReportesSize"].Value = configNueva.TamContenidoRep;

            //save to apply changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");


           

        }
    }
}

