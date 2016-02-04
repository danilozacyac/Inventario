using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DaoProject.Dao
{
    public class Equipos : INotifyPropertyChanged
    {
        private int idEquipo;
        private String scEquipo;
        private String scPrincipal;
        private int expediente;
        private int idTipo;
        private String tipoEquipo;
        private String marca;
        private String modelo;
        private String noSerie;
        private String observaciones;
        private String estado;
        private DateTime? fechaAlta;
        private DateTime? fechaModificacion;
        private List<HistorialPc> historial = null;

        public String TipoEquipo
        {
            get
            {
                return this.tipoEquipo;
            }
            set
            {
                this.tipoEquipo = value;
            }
        }

        public int IdEquipo
        {
            get
            {
                return this.idEquipo;
            }
            set
            {
                this.idEquipo = value;
            }
        }

        public String ScEquipo
        {
            get
            {
                return this.scEquipo;
            }
            set
            {
                this.scEquipo = value;
            }
        }

        public String ScPrincipal
        {
            get
            {
                return this.scPrincipal;
            }
            set
            {
                this.scPrincipal = value;
            }
        }

        public int Expediente
        {
            get
            {
                return this.expediente;
            }
            set
            {
                this.expediente = value;
            }
        }

        public int IdTipo
        {
            get
            {
                return this.idTipo;
            }
            set
            {
                this.idTipo = value;
            }
        }

        public String Marca
        {
            get
            {
                return this.marca;
            }
            set
            {
                this.marca = value;
            }
        }

        public String Modelo
        {
            get
            {
                return this.modelo;
            }
            set
            {
                this.modelo = value;
            }
        }

        public String NoSerie
        {
            get
            {
                return this.noSerie;
            }
            set
            {
                this.noSerie = value;
            }
        }

        public String Observaciones
        {
            get
            {
                return this.observaciones;
            }
            set
            {
                this.observaciones = value;
                this.OnPropertyChanged("Observaciones");
            }
        }

        public String Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = value;
            }
        }

        public DateTime? FechaAlta
        {
            get
            {
                return this.fechaAlta;
            }
            set
            {
                this.fechaAlta = value;
            }
        }

        public DateTime? FechaModificacion
        {
            get
            {
                return this.fechaModificacion;
            }
            set
            {
                this.fechaModificacion = value;
                this.OnPropertyChanged("FechaModificacion");
            }
        }

        public List<HistorialPc> Historial
        {
            get
            {
                return this.historial;
            }
            set
            {
                this.historial = value;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}

