using System;
using System.Configuration;
using System.Linq;

namespace Inventario.Configuracion
{
    public class ConfigClass
    {


        #region Reportes
        private String tituloComputo = ConfigurationManager.AppSettings.Get("TituloComputo");
        private String tituloMobiliario = ConfigurationManager.AppSettings.Get("TituloMobiliario");
        private String responsableComputo = ConfigurationManager.AppSettings.Get("ResponsableComputo");
        private String responsableMobiliario = ConfigurationManager.AppSettings.Get("ResponsableMobiliario");
        private String rutaImagenEncabezado = ConfigurationManager.AppSettings.Get("ImagenReportes");
        private String tipoLetraReporte = ConfigurationManager.AppSettings.Get("EncabezadoReportesFont");
        private String tamEncabezadoRep = ConfigurationManager.AppSettings.Get("EncabezadoReportesSize");
        private String tamContenidoRep = ConfigurationManager.AppSettings.Get("ContenidoReportesSize");


        public String TituloComputo
        {
            get
            {
                return this.tituloComputo;
            }
            set
            {
                this.tituloComputo = value;
            }
        }

        public String TituloMobiliario
        {
            get
            {
                return this.tituloMobiliario;
            }
            set
            {
                this.tituloMobiliario = value;
            }
        }

        public String ResponsableComputo
        {
            get
            {
                return this.responsableComputo;
            }
            set
            {
                this.responsableComputo = value;
            }
        }

        public String ResponsableMobiliario
        {
            get
            {
                return this.responsableMobiliario;
            }
            set
            {
                this.responsableMobiliario = value;
            }
        }

        public String RutaImagenEncabezado
        {
            get
            {
                return this.rutaImagenEncabezado;
            }
            set
            {
                this.rutaImagenEncabezado = value;
            }
        }

        public String TipoLetraReporte
        {
            get
            {
                return this.tipoLetraReporte;
            }
            set
            {
                this.tipoLetraReporte = value;
            }
        }

        public String TamEncabezadoRep
        {
            get
            {
                return this.tamEncabezadoRep;
            }
            set
            {
                this.tamEncabezadoRep = value;
            }
        }

        public String TamContenidoRep
        {
            get
            {
                return this.tamContenidoRep;
            }
            set
            {
                this.tamContenidoRep = value;
            }
        }

        #endregion
    }
}
