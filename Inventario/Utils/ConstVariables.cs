using System;
using System.Collections.ObjectModel;
using System.Linq;
using Inventario.Dao;

namespace Inventario.Utils
{
    public class ConstVariables
    {
        public static ObservableCollection<Equipos> ListaSubEquipos = new ObservableCollection<Equipos>();


        /// <summary>
        /// Caracteres no permitidos para agregar datos.
        /// </summary>
        public static String[] NoPermitidos = new String[] { "+", "=", "'", "&", "^", "$", "#", "@","-","\\",
                                 "!","¡","¿","?","<",">","~","¬","|","°",",",";",":","%","\n",
                                 "(",")","[","]","{","}","´","¨","_","`","¥","€"};


        /// <summary>
        /// Las palabras que no se incluyen en busquedas
        /// </summary>
        public static String[] Stoppers = new String[]{"el","la","las", "le","lo", "los", "no", ".", 
            "pero", "puede","se", "sus", "y", "o", "n","a", "al", "aquel", "aun", "cada", "como", "con", "cual", 
            "de", "debe", "deben", "del", "el", "en", "este", "esta", "la", "las", "le", "lo", "los", 
            "para", "pero", "por", "puede", "que", "se", "sin", "sus", "un", "una"};

        /// <summary>
        /// Encabezados de las columnas cuando se exporta el mobiliario a un documento de Excel
        /// </summary>
        public static String[] ExcelMobiliarioHeader = new String[] { "Número inventario","Denominación del activo", "Expediente", "Nombre del usuario",
            "Ubicación", "Cencos" , "Observaciones"};


        public static String[] ExcelEquipoHeader = new String[] { "Número inventario","Denominación del activo", "Expediente", "Nombre del usuario",
            "Ubicación", "Cencos" , "Extensión", "Puerta", "Marca", "Modelo", "No. de serie", "Observaciones"};


    }
}

