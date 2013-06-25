using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Inventario.Converters;
using Inventario.Dao;
using Inventario.Singleton;
using Inventario.Utils;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Inventario.Reportes.Exporta
{
    public class ExportaEquipo
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
                    hoja.Cells[fila, 5] = new UbicacionToStringConverter().Convert(servidor.IdUbicacion, null, null, null);
                    hoja.Cells[fila, 6] = new AreaToStringConverter().Convert(servidor.IdArea, null, null, null);
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
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xml";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = true;
            saveDialog.Title = "Guardar en";
            saveDialog.InitialDirectory = @"C:\DOCS\";

            if (saveDialog.ShowDialog() == true)
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
