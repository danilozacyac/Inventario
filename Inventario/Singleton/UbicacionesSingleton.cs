using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Singleton
{
    public class UbicacionesSingleton
    {
        private static List<CommonProperties> ubicaciones;

        private UbicacionesSingleton()
        {
        }

        public static List<CommonProperties> Ubicaciones
        {
            get
            {
                if (ubicaciones == null)
                    ubicaciones = new UbicacionModel().GetUbicaciones();

                return ubicaciones;
            }
        }
    }
}