using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Singleton
{
    public class TitulosSingleton
    {
        private static List<CommonProperties> titulos;

        private TitulosSingleton()
        {
        }

        public static List<CommonProperties> Titulos
        {
            get
            {
                if (titulos == null)
                    titulos = new TitulosModel().GetTitulos();

                return titulos;
            }
        }

    }
}
