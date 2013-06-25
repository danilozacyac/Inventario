using System;
using System.Linq;

namespace Inventario.Dao
{
    public class CommonProperties
    {
        private int idElemento;
        private String descripcion;
        private String abreviatura;
        private String corto;


        public int IdElemento
        {
            get
            {
                return this.idElemento;
            }
            set
            {
                this.idElemento = value;
            }
        }

        public String Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
            }
        }

        public String Abreviatura
        {
            get
            {
                return this.abreviatura;
            }
            set
            {
                this.abreviatura = value;
            }
        }

        public String Corto
        {
            get
            {
                return this.corto;
            }
            set
            {
                this.corto = value;
            }
        }
    }
}
