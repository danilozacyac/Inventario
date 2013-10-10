using System;
using System.Configuration;
using System.Linq;
using iTextSharp.text;

namespace Reporting
{
    public class Fuentes
    {
        private static readonly BaseColor black = new BaseColor(0, 0, 0);

        public static Font Encabezados
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 18, Font.BOLD, black);
                return font;
            }
        }

        public static Font NomExpd
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 12, Font.BOLD, black);
                return font;
            }
        }

        public static Font NomExpdUnder
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 12, Font.UNDERLINE, black);
                return font;
            }
        }

        public static Font EncabezadoColumna
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 12, Font.BOLD, black);
                return font;
            }
        }



        #region Reportes


        public static Font EncabezadoReportes
        {
            get
            {
                Font font = FontFactory.GetFont(ConfigurationManager.AppSettings.Get("EncabezadoReportesFont"),
                                                Convert.ToInt32(ConfigurationManager.AppSettings.Get("EncabezadoReportesSize")), Font.BOLD, black);
                return font;
            }
        }

        public static Font EncabezadoReportesTitulo
        {
            get
            {
                Font font = FontFactory.GetFont(ConfigurationManager.AppSettings.Get("EncabezadoReportesFont"),
                                                Convert.ToInt32(ConfigurationManager.AppSettings.Get("EncabezadoReportesSize")) - 1, Font.NORMAL, black);
                return font;
            }
        }

        public static Font ContenidoCelda
        {
            get
            {
                Font font = FontFactory.GetFont(ConfigurationManager.AppSettings.Get("EncabezadoReportesFont"),
                                                Convert.ToInt32(ConfigurationManager.AppSettings.Get("ContenidoReportesSize")) , Font.NORMAL, black);
                return font;
            }
        }

        #endregion
        /// <summary>
        /// Fuentes IUS
        /// </summary>


        public static Font TemaIus
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 14, Font.BOLD, black);
                return font;
            }
        }

        public static Font SubtemasIus
        {
            get
            {
                BaseColor grey = new BaseColor(0, 0, 0);
                Font font = FontFactory.GetFont("Arial", 11, Font.NORMAL, grey);
                return font;
            }
        }

        public static Font RelacionIus
        {
            get
            {
                BaseColor grey = new BaseColor(0, 0, 0);
                Font font = FontFactory.GetFont("Arial", 10, Font.NORMAL, grey);
                return font;
            }
        }

        /// <summary>
        /// Fuentes Comisión de Venecia
        /// </summary>

        public static Font VeneciaTema
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 14, Font.NORMAL, black);
                return font;
            }
        }

        public static Font VeneciaSubTema
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 11, Font.NORMAL, black);
                return font;
            }
        }


        public static Font Venecia
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 10, Font.NORMAL, black);
                return font;
            }
        }

        /// <summary>
        /// Fuentes CIDH
        /// </summary>

        public static Font CidhTema
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 14, Font.BOLD, black);
                return font;
            }
        }

        public static Font CidhSubTema
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 11, Font.BOLD, black);
                return font;
            }
        }

        public static Font Cidh
        {
            get
            {
                BaseColor green = new BaseColor(0, 200, 0);
                Font font = FontFactory.GetFont("Arial", 10, Font.NORMAL, green);
                return font;
            }
        }

        /// <summary>
        /// Fuente Resultado Busqueda
        /// </summary>

        public static Font PdfEncuentraTema
        {
            get
            {
                BaseColor red = new BaseColor(200, 0, 0);
                Font font = FontFactory.GetFont("Arial", 14, Font.NORMAL, red);
                return font;
            }
        }

        public static Font PdfEncuentraSubTema
        {
            get
            {
                Font font = FontFactory.GetFont("Arial", 11, Font.NORMAL, black);
                return font;
            }
        }

        /// <summary>
        /// Fuentes encabezado y Pie de Pagina
        /// </summary>

        public static Font Footer
        {
            get
            {
                // create a basecolor to use for the footer font, if needed.
                BaseColor grey = new BaseColor(128, 128, 128);
                Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);
                return font;
            }
        }

        public static Font Header
        {
            get
            {
                BaseColor grey = new BaseColor(255, 0, 0);
                Font font = FontFactory.GetFont("Arial", 16, Font.NORMAL, grey);
                return font;
            }
        }

        /// <summary>
        /// Los siguientes metodos devuelven los colores para las relaciones que se 
        /// imprimiran en el reporte
        /// </summary>
        /// <param name="tesauroRelacion"></param>
        /// <returns></returns>




    }
}