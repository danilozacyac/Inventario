using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Inventario.Dao
{
    public class Mobiliario
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
    }
}
