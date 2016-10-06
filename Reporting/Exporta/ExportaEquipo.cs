using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using DaoProject.Dao;
using DaoProject.Singleton;
using DaoProject.Utilities;
using Microsoft.Office.Interop.Excel;

namespace Reporting.Exporta
{
    public class ExportaEquipo
    {
        private readonly Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        private Worksheet hoja;

        public void GeneraDocumento()
        {
            app.Visible = true;
            Workbook libro = app.Workbooks.Add();

            hoja = libro.Worksheets[1];
            hoja.Name = "Inventario";

            int columna = 1;
            int fila = 1;
            foreach (String encabezado in ConstVariables.ExcelEquipoHeader)
            {
                hoja.Cells[fila, columna] = encabezado;
                columna++;
            }


            foreach (ServidoresPublicos servidor in ServidoresSingleton.Servidores)
            {

                foreach (Equipos equipo in servidor.Equipos)
                {
                    fila++;
                    hoja.Cells[fila, 1] = equipo.ScEquipo;
                    hoja.Cells[fila, 2] = equipo.TipoEquipo;
                    hoja.Cells[fila, 3] = equipo.Expediente;
                    hoja.Cells[fila, 4] = servidor.Nombre;
                    hoja.Cells[fila, 5] = (from n in UbicacionesSingleton.Ubicaciones
                                           where n.IdElemento == servidor.IdUbicacion
                                           select n.Descripcion).ToList()[0];

                    hoja.Cells[fila, 6] = (from n in AreasSingleton.Areas
                                           where n.IdElemento == servidor.IdArea
                                           select n.Descripcion).ToList()[0];
                        
                    hoja.Cells[fila, 7] = servidor.Extension;
                    hoja.Cells[fila, 8] = servidor.Puerta;
                    hoja.Cells[fila, 9] = equipo.Marca;
                    hoja.Cells[fila, 10] = equipo.Modelo;
                    hoja.Cells[fila, 11] = equipo.NoSerie;
                    hoja.Cells[fila, 12] = equipo.Observaciones;

                }
            }

            hoja.Columns.AutoFit();

        }

        public void GeneraXml()
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                DefaultExt = "xml",
                AddExtension = true,
                RestoreDirectory = true,
                Title = "Guardar en",
                InitialDirectory = @"C:\DOCS\"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {

                StreamWriter myWriter = new StreamWriter(saveDialog.FileName);
                saveDialog = null;

                // Insert code to set properties and fields of the object.
                XmlSerializer mySerializer = new
                XmlSerializer(typeof(List<ServidoresPublicos>));
                // To write to a file, create a StreamWriter object.
                mySerializer.Serialize(myWriter, ServidoresSingleton.Servidores);


            }
        }
    }
}
