using System;
using System.Linq;

namespace DaoProject.Dao
{
    public class HistorialPc
    {
        private int idMovimiento;
        private String scEquipo;
        private String scPrincipal;
        private int idTipo;
        private int expAnterior;
        private int expActual;
        private String observaciones;
        private String fechaModificacion;
        private String usuarioModifica;
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

        public String FechaModificacion
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
