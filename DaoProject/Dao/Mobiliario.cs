using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DaoProject.Dao
{
    public class Mobiliario : INotifyPropertyChanged
    {
        private int idMobiliario;
        private int idTipoMobiliario;
        private String tipoMobiliario;
        private long inventario;
        private int expediente;
        private String observaciones;
        private DateTime? fechaAlta;
        private DateTime? fechaModificacion;
        private ObservableCollection<HistorialMobiliario> historial;

        public int IdMobiliario
        {
            get
            {
                return this.idMobiliario;
            }
            set
            {
                this.idMobiliario = value;
            }
        }

        public int IdTipoMobiliario
        {
            get
            {
                return this.idTipoMobiliario;
            }
            set
            {
                this.idTipoMobiliario = value;
            }
        }

        public long Inventario
        {
            get
            {
                return this.inventario;
            }
            set
            {
                this.inventario = value;
                this.OnPropertyChanged("Inventario");
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

        public String TipoMobiliario
        {
            get
            {
                return this.tipoMobiliario;
            }
            set
            {
                this.tipoMobiliario = value;
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

        public DateTime? FechaAlta
        {
            get
            {
                return this.fechaAlta;
            }
            set
            {
                this.fechaAlta = value;
                this.OnPropertyChanged("FechaAlta");
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

        public ObservableCollection<HistorialMobiliario> Historial
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
