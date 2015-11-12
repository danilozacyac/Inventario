using System;
using System.Linq;

namespace DaoProject.Dao
{
    public class LevantaReporte : Equipos
    {
        private int idReporte;
        private string nombre;
        private int numReporte;
        private DateTime? fechaReporte;
        private int fechaReporteInt;
        private string problema;
        private int reporto;
        private string reportoStr;
        private string atendio;
        private DateTime? fechaCierre;
        private int fechaCierreInt;

        
        public int IdReporte
        {
            get
            {
                return this.idReporte;
            }
            set
            {
                this.idReporte = value;
            }
        }

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        public int NumReporte
        {
            get
            {
                return this.numReporte;
            }
            set
            {
                this.numReporte = value;
            }
        }

        public DateTime? FechaReporte
        {
            get
            {
                return this.fechaReporte;
            }
            set
            {
                this.fechaReporte = value;
            }
        }

        public int FechaReporteInt
        {
            get
            {
                return this.fechaReporteInt;
            }
            set
            {
                this.fechaReporteInt = value;
            }
        }

        public string Problema
        {
            get
            {
                return this.problema;
            }
            set
            {
                this.problema = value;
                this.OnPropertyChanged("Problema");
            }
        }



        public int Reporto
        {
            get
            {
                return this.reporto;
            }
            set
            {
                this.reporto = value;
            }
        }

        public string ReportoStr
        {
            get
            {
                return this.reportoStr;
            }
            set
            {
                this.reportoStr = value;
            }
        }

        public string Atendio
        {
            get
            {
                return this.atendio;
            }
            set
            {
                this.atendio = value;
                this.OnPropertyChanged("Atendio");
            }
        }

        public DateTime? FechaCierre
        {
            get
            {
                return this.fechaCierre;
            }
            set
            {
                this.fechaCierre = value;
                this.OnPropertyChanged("FechaCierre");
            }
        }

        public int FechaCierreInt
        {
            get
            {
                return this.fechaCierreInt;
            }
            set
            {
                this.fechaCierreInt = value;
            }
        }

       
    }
}
