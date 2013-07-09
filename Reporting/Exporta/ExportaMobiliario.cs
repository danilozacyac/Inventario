using System;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Singleton;
using DaoProject.Utilities;
using Microsoft.Office.Interop.Excel;

//using ExcelDoc = Microsoft.Office.Interop.Excel;

namespace Reporting.Exporta
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
                    hoja.Cells[fila, 2] = (from n in TiposEquiposSingleton.Tipos
                                           where n.IdElemento == mobil.IdTipoMobiliario && n.Corto == "2"
                                           select n.Descripcion).ToList()[0];
                        
                    hoja.Cells[fila, 3] = mobil.Expediente;
                    hoja.Cells[fila, 4] = servidor.Nombre;
                    hoja.Cells[fila, 5] = (from n in UbicacionesSingleton.Ubicaciones
                                           where n.IdElemento == servidor.IdUbicacion
                                           select n.Descripcion).ToList()[0];
                        
                    hoja.Cells[fila, 6] = (from n in AreasSingleton.Areas
                                           where n.IdElemento == servidor.IdArea
                                           select n.Descripcion).ToList()[0];
                        
                    hoja.Cells[fila, 7] = mobil.Observaciones;

                }
            }

            hoja.Columns.AutoFit();

        }


    }
}
