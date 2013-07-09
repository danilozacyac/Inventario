using System;
using System.Collections.Generic;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
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