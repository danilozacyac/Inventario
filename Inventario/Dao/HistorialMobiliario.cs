using System;
using System.Linq;

namespace Inventario.Dao
{
    public class HistorialMobiliario
    {
        private int idMovimiento;
        private int idMobiliario;
        private int expAnterior;
        private int expActual;
        private String observaciones;
        private DateTime? fechaReasignacion;
        private String usuarioModifica;

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

        public int IdMovimiento
        {
            get
            {
                return this.idMovimiento;
            }
            set
            {
                this.idMovimiento = value;
            }
        }

        public int ExpAnterior
        {
            get
            {
                return this.expAnterior;
            }
            set
            {
                this.expAnterior = value;
            }
        }

        public int ExpActual
        {
            get
            {
                return this.expActual;
            }
            set
            {
                this.expActual = value;
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

        public DateTime? FechaReasignacion
        {
            get
            {
                return this.fechaReasignacion;
            }
            set
            {
                this.fechaReasignacion = value;
            }
        }

        public String UsuarioModifica
        {
            get
            {
                return this.usuarioModifica;
            }
            set
            {
                this.usuarioModifica = value;
            }
        }
    }
}
