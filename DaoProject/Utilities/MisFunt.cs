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
        /// Verifica que el reader en la columna seleccionada no tenga null como valor
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string VerifyDbNullForStrings(SqlDataReader reader, string property)
        {
            if (reader[property] == DBNull.Value)
                return string.Empty;
            else
                return reader[property].ToString();
        }


        public static DateTime? ConvertReaderToDateTime(SqlDataReader reader, string property)
        {

            if (reader[property] == DBNull.Value)
                return null;
            else
                return DateTime.Parse(reader[property].ToString());//, System.Globalization.DateTimeStyles.AssumeLocal);

        }

        /// <summary>
        /// Verifica si el caracter introducido es un digito
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsADigit(string text)
        {
            // Regex NumEx = new Regex(@"^\d+(?:.\d{0,2})?$"); 
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text 
            return regex.IsMatch(text);
        }

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

        /// <summary>
        /// Pone en mayúsculas la primer letra de cada palabra en una cadena de caracteres
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

       

    }
}
