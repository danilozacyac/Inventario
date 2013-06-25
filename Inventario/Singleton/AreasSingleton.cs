using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Singleton
{
    public class AreasSingleton
    {
        private static List<CommonProperties> areas;

        private AreasSingleton()
        {
        }

        public static List<CommonProperties> Areas
        {
            get
            {
                if (areas == null)
                    areas = new AreasModel().GetAreas();

                return areas;
            }
        }

        private static List<CommonProperties> adscripciones;

        public static List<CommonProperties> Adscripciones
        {
            get
            {
                if (adscripciones == null)
                    adscripciones = new AreasModel().GetAdscripciones();

                return adscripciones;
            }
        }
    }
}