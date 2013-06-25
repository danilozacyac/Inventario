using System;
using System.Linq;
using Inventario.Converters;
using Inventario.Dao;
using Inventario.Singleton;
using Inventario.Utils;
using Microsoft.Office.Interop.Excel;

//using ExcelDoc = Microsoft.Office.Interop.Excel;

namespace Inventario.Reportes.Exporta
{
    public class ExportaMobiliario
    {
        private readonly Application app = new Application();
        private Worksheet hoja;

        public void GeneraDocumento()
        {
            app.Visible = true;
            Workbook libro = app.Workbooks.Add();

            hoja = libro.Worksheets[1];
            hoja.Name = "Inventario";

            int columna = 1;
            int fila = 1;
            foreach (String encabezado in ConstVariables.ExcelMobiliarioHeader)
            {
                hoja.Cells[fila, columna] = encabezado;
                columna++;
            }


            foreach (ServidoresPublicos servidor in ServidoresSingleton.Servidores)
            {

                foreach (Mobiliario mobil in servidor.Mobiliario)
                {
                    fila++;
                    hoja.Cells[fila, 1] = mobil.Inventario;
                    hoja.Cells[fila, 2] = new MobDescripcionConverter().Convert(mobil.IdTipoMobiliario, null, null, null);
                    hoja.Cells[fila, 3] = mobil.Expediente;
                    hoja.Cells[fila, 4] = servidor.Nombre;
                    hoja.Cells[fila, 5] = new UbicacionToStringConverter().Convert(servidor.IdUbicacion, null, null, null);
                    hoja.Cells[fila, 6] = new AreaToStringConverter().Convert(servidor.IdArea, null, null, null);
                    hoja.Cells[fila, 7] = mobil.Observaciones;

                }
            }

            hoja.Columns.AutoFit();

        }


    }
}
