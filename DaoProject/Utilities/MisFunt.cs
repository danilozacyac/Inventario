using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using DaoProject.Singleton;

namespace DaoProject.Utilities
{
    public class MisFunt
    {

        /// <summary>
        /// Devuelve el "título" que ostenta cada persona
        /// </summary>
        /// <param name="idTitulo"></param>
        /// <returns></returns>
        public static String GetTituloDescrip(int idTitulo)
        {
            return (from n in TitulosSingleton.Titulos
                    where n.IdElemento == idTitulo
                    select n.Abreviatura).ToList()[0];
        }

        /// <summary>
        /// Devuelve el texto de la adscripción de acuerdo al identificador
        /// </summary>
        /// <param name="idAdscripcion"></param>
        /// <returns></returns>
        public static String GetAdscripcionDescrip(int idAdscripcion)
        {
            return (from n in AreasSingleton.Adscripciones
                    where n.IdElemento == idAdscripcion
                    select n.Descripcion).ToList()[0];
        }

        /// <summary>
        /// Devuelve el nombre del área a la que pertence el servidor
        /// </summary>
        /// <param name="idArea"></param>
        /// <returns></returns>
        public static String GetAreasDescrip(int idArea)
        {
            return (from n in AreasSingleton.Areas
                    where n.IdElemento == idArea
                    select n.Descripcion).ToList()[0];
        }

        public static String GetTipoMobilDescrip(int idTipoEquipo)
        {
            return (from n in TiposEquiposSingleton.MySingletonInstance.Tipos
                    where n.IdElemento == idTipoEquipo
                    select n.Descripcion).ToList()[0];
        }

        /// <summary>
        /// Devuelve una lista de palabras de acuerdo a la cadena recibida, omitiendo aquellas palabras
        /// incluidas en el arreglo Stoppers de la clase ConstVariables
        /// </summary>
        /// <param name="descripcion">Cadena a ser analizada</param>
        /// <returns></returns>
        public static List<String> SplitStringWithoutStoppers(String descripcion)
        {
            List<String> palabras = new List<string>();

            foreach (String palabra in descripcion.Split(' '))
            {
                if (!ConstVariables.Stoppers.Contains(palabra))
                    palabras.Add(palabra);
            }

            return palabras;
        }


       

    }
}
