using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DaoProject.Dao;
using DaoProject.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reporting
{
    public class REquipoComputo : IReportePdf
    {
        private iTextSharp.text.Document myDocument;
        //private Paragraph para;

        private ObservableCollection<ServidoresPublicos> servidores;
        private readonly ServidoresPublicos servidor;
        private readonly int idAreaReporte;

        public REquipoComputo(ObservableCollection<ServidoresPublicos> servidores, int idAreaReporte)
        {
            this.servidores = servidores;
            this.idAreaReporte = idAreaReporte;
        }

        public REquipoComputo(ServidoresPublicos servidor)
        {
            this.servidor = servidor;
        }

        /// <summary>
        /// Devuelve el reporte del equipo que esta bajo resguardo de  un servidor público en específico
        /// </summary>
        public void ReportePersonal()
        {
            myDocument = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            string documento = Path.GetTempFileName() + ".pdf";

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(myDocument, new FileStream(documento, FileMode.Create));

                myDocument.Open();

                if (servidor.Equipos.Count > 0)
                {
                    myDocument.NewPage();

                    myDocument = RElementosComunes.SetPageHeader(myDocument);

                    myDocument = RElementosComunes.SetSpaces(myDocument, 1);

                    myDocument = RElementosComunes.SetUserInfo(myDocument, servidor);

                    myDocument = RElementosComunes.SetSpaces(myDocument, 1);

                    this.SetEquiposInfo(servidor.Equipos);

                    myDocument = RElementosComunes.SetSpaces(myDocument, 3);

                    myDocument = RElementosComunes.SetPageFooter(myDocument, servidor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, "Error Interno");
            }
            finally
            {
                if (servidor.Equipos.Count > 0)
                {
                    myDocument.Close();
                    System.Diagnostics.Process.Start(documento);
                }
            }
        }

        /// <summary>
        /// Devuelve el reporte del resguardo de equipo de computo de un área en particular o de 
        /// toda la Coordinación
        /// </summary>
        public void ReportePorAreas()
        {
            myDocument = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            string documento = Path.GetTempFileName() + ".pdf";

            try
            {
                if (idAreaReporte != 0)
                    servidores = ((from n in servidores
                                   where n.IdArea == idAreaReporte
                                   select n).ToList()).ToObservableCollection();

                PdfWriter writer = PdfWriter.GetInstance(myDocument, new FileStream(documento, FileMode.Create));

                myDocument.Open();

                foreach (ServidoresPublicos usuario in servidores)
                {
                    if (usuario.Equipos.Count > 0)
                    {
                        myDocument.NewPage();

                        myDocument = RElementosComunes.SetPageHeader(myDocument);

                        myDocument = RElementosComunes.SetSpaces(myDocument, 1);

                        myDocument = RElementosComunes.SetUserInfo(myDocument, usuario);

                        myDocument = RElementosComunes.SetSpaces(myDocument, 1);

                        this.SetEquiposInfo(usuario.Equipos);

                        myDocument = RElementosComunes.SetSpaces(myDocument, 3);

                        myDocument = RElementosComunes.SetPageFooter(myDocument, usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, "Error Interno");
            }
            finally
            {
                myDocument.Close();
                System.Diagnostics.Process.Start(documento);
            }
        }

        private void SetEquiposInfo(ObservableCollection<Equipos> equipos)
        {
            PdfPTable table = new PdfPTable(5);
            //table.TotalWidth = 400;
            table.WidthPercentage = 100;

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            string[] encabezado = { "Equipo", "SC Equipo", "Marca", "No. Serie", "Observaciones" };
            PdfPCell cell;

            foreach (string cabeza in encabezado)
            {
                cell = new PdfPCell(new Phrase(cabeza, Fuentes.EncabezadoColumna));
                cell.Colspan = 0;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
            }

            foreach (Equipos equipo in equipos)
            {
                string[] descs = { equipo.TipoEquipo, equipo.ScEquipo, equipo.Marca, equipo.NoSerie, equipo.Observaciones };

                foreach (string desc in descs)
                {
                    cell = new PdfPCell(new Phrase(desc, Fuentes.ContenidoCelda));
                    cell.Colspan = 0;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);
                }
            }

            myDocument.Add(table);
        }
    }
}