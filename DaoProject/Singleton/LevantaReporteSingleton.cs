using System;
using System.Collections.ObjectModel;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
{
    public class LevantaReporteSingleton
    {

         private static ObservableCollection<LevantaReporte> reportes;

         private LevantaReporteSingleton()
        {
        }

        public static ObservableCollection<LevantaReporte> Reportes
        {
            get
            {
                if (reportes == null)
                    reportes = new LevantaReporteModel().GetReportes();

                return reportes;
            }
        }
    }
}
