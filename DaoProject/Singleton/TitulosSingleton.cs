using System;
using System.Collections.Generic;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
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
