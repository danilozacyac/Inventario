using System;
using System.Configuration;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reporting
{
    public class RElementosComunes
    {
        /// <summary>
        /// Agrega el encabezado a cada uno de los resguardos generados, incluye el logo de la SCJN
        /// </summary>
        /// <param name="myDocument"></param>
        /// <returns></returns>
        public static iTextSharp.text.Document SetPageHeader(iTextSharp.text.Document myDocument)
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;

            table.SetWidths(new Single[] { 35, 150 });

            table.SpacingBefore = 20f;
            table.SpacingAfter = 10f;

            Image gif = Image.GetInstance(ConfigurationManager.AppSettings.Get("ImagenReportes"));
            gif.ScalePercent(24f);

            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.AddElement(gif);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            Paragraph para = new Paragraph(ConfigurationManager.AppSettings.Get("AreaCorteL1"), Fuentes.EncabezadoReportes);
            para.Add(Environment.NewLine);
            para.Add(ConfigurationManager.AppSettings.Get("AreaCorteL2"));
            para.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(para);

            if (AccesoUsuarioModel.Grupo == 1)
                para = new Paragraph(ConfigurationManager.AppSettings.Get("TituloComputo"), Fuentes.EncabezadoReportesTitulo);
            else
                para = new Paragraph(ConfigurationManager.AppSettings.Get("TituloMobiliario"), Fuentes.EncabezadoReportesTitulo);
            para.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(para);
            table.AddCell(cell);

            myDocument.Add(table);

            return myDocument;
        }

        /// <summary>
        /// Agrega los espacios en blanco necesarios despues de cada párrafo
        /// </summary>
        /// <param name="myDocument"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static iTextSharp.text.Document SetSpaces(iTextSharp.text.Document myDocument, int number)
        {
            for (int x = 0; x < number; x++)
            {
                iTextSharp.text.Paragraph white = new iTextSharp.text.Paragraph(" ");
                myDocument.Add(white);
            }
            return myDocument;
        }

        /// <summary>
        /// Agrega la información del usuario en cuestión al su resguardo
        /// </summary>
        /// <param name="myDocument"></param>
        /// <param name="serv"></param>
        /// <returns></returns>
        public static iTextSharp.text.Document SetUserInfo(iTextSharp.text.Document myDocument, ServidoresPublicos serv)
        {
            Phrase phrase = new Phrase("Nombre: ", Fuentes.NomExpd);
            Paragraph para = new Paragraph(phrase);
            phrase = new Phrase(serv.Nombre, Fuentes.NomExpdUnder);
            para.Add(phrase);
            phrase = new Phrase("                           ", Fuentes.NomExpd);
            para.Add(phrase);
            phrase = new Phrase("Expediente: ", Fuentes.NomExpd);
            para.Add(phrase);
            phrase = new Phrase(serv.Expediente.ToString(), Fuentes.NomExpdUnder);
            para.Add(phrase);
            myDocument.Add(para);

            phrase = new Phrase("Área:      ", Fuentes.NomExpd);
            para = new Paragraph(phrase);
            phrase = new Phrase(MisFunt.GetAreasDescrip(serv.IdArea), Fuentes.NomExpdUnder);
            para.Add(phrase);
            myDocument.Add(para);

            phrase = new Phrase("Adscripción: ", Fuentes.NomExpd);
            para = new Paragraph(phrase);
            phrase = new Phrase(MisFunt.GetAdscripcionDescrip(serv.IdAdscripcion), Fuentes.NomExpdUnder);
            para.Add(phrase);
            myDocument.Add(para);

            phrase = new Phrase("Fecha:   ", Fuentes.NomExpd);
            para = new Paragraph(phrase);
            phrase = new Phrase(DateTime.Now.ToString("dd/MM/yyyy"), Fuentes.NomExpdUnder);
            para.Add(phrase);
            phrase = new Phrase("                                                             ", Fuentes.NomExpd);
            para.Add(phrase);
            phrase = new Phrase("Puerta: ", Fuentes.NomExpd);
            para.Add(phrase);
            phrase = new Phrase(serv.Puerta.ToString(), Fuentes.NomExpdUnder);
            para.Add(phrase);
            myDocument.Add(para);

            return myDocument;
        }

        /// <summary>
        /// Agrega la zona de firmas al resguardo
        /// </summary>
        /// <param name="myDocument"></param>
        /// <param name="serv"></param>
        /// <returns></returns>
        public static iTextSharp.text.Document SetPageFooter(iTextSharp.text.Document myDocument, ServidoresPublicos serv)
        {
            PdfPTable firma = new PdfPTable(3);
            //table.TotalWidth = 400;
            firma.WidthPercentage = 100;

            //Evitan que la tabla se separe si quedca entre dos paginas
            firma.KeepTogether = true;
            firma.SplitLate = true;
            firma.SplitRows = false;

            //firma.SpacingBefore = 20f;
            firma.SpacingAfter = 30f;

            float[] widths = new float[] { 2f, 1f, 2f };
            firma.SetWidths(widths);

            PdfPCell cell = new PdfPCell(new Phrase(" Entrega:" , Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);

            cell = new PdfPCell(new Phrase("Recibe:", Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);

            cell = new PdfPCell(new Phrase("   ", Fuentes.ContenidoCelda));
            cell.Border = 0;
            for(int cellX = 0;cellX < 6 ;cellX++)
                firma.AddCell(cell);
            
            

            cell = new PdfPCell(new Phrase("        " + ConfigurationManager.AppSettings.Get("ResponsableComputo"), Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = Rectangle.TOP_BORDER;
            cell.BorderWidthTop = 1f;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);

            cell = new PdfPCell(new Phrase(MisFunt.GetTituloDescrip(serv.IdTitulo) + " " + serv.Nombre, Fuentes.ContenidoCelda));
            cell.Colspan = 0;
            cell.Border = Rectangle.TOP_BORDER;
            cell.BorderWidthTop = 1f;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            firma.AddCell(cell);
            myDocument.Add(firma);

            return myDocument;
        }
    }
}